﻿using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Designer.Metadata;
using EFSM.Designer.Model;
using EFSM.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineViewModel : DesignerViewModelBase, ITransitionCreator
    {
        private const double DefaultWidth = 800;
        private const double DefaultHeight = 800;

        private double _width = DefaultWidth;
        private double _height = DefaultHeight;

        private Point _newTransitionEnd;
        private bool _isBulkDeleteInProgress = false;
        private bool _isStateDeleteInProgress = false;

        private readonly ObservableCollection<TransitionViewModel> _transitions = new ObservableCollection<TransitionViewModel>();
        private readonly bool _isReadOnly;
        private bool _isCreatingTransition;
        private bool _isConnectorMode;

        private StateViewModel _sourceForNewTransition;
        private readonly StateMachine _model;
        private readonly IViewService _viewService;

        private readonly IUndoProvider _undoProvider;
        private readonly IDirtyService _dirtyService;
        private bool _isPointerMode = true;
        public override bool IsReadOnly => _isReadOnly;
        private Point _newTransitionStart;
        private bool _isInitialized = false;

        private ObservableCollection<StateMachineInputViewModel> _inputs = new ObservableCollection<StateMachineInputViewModel>();
        private ObservableCollection<StateMachineOutputActionViewModel> _outputs = new ObservableCollection<StateMachineOutputActionViewModel>();

        public StateMachineViewModel(
            StateMachine model,
            IViewService viewService,
            IUndoProvider undoProvider,
            IDirtyService dirtyService,
            IMessageBoxService messageBoxService,
            bool isReadOnly = false
            ) : base(messageBoxService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _undoProvider = undoProvider;
            _dirtyService = dirtyService;
            _isReadOnly = isReadOnly;

            InitiateModel();
        }

        public int NumberOfInstances
        {
            get => _model.NumberOfInstances;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                _model.NumberOfInstances = value;
                RaisePropertyChanged();
                SaveUndoState();
            }
        }

        public ObservableCollection<StateMachineInputViewModel> Inputs
        {
            get { return _inputs; }
            set
            {
                _inputs = value;
                RaisePropertyChanged();
                SaveUndoState();
            }
        }


        public ObservableCollection<StateMachineOutputActionViewModel> Outputs
        {
            get { return _outputs; }
            set
            {
                _outputs = value;
                RaisePropertyChanged();
                SaveUndoState();
            }
        }

        private ObservableCollection<StateViewModel> _states = new ObservableCollection<StateViewModel>();
        public ObservableCollection<StateViewModel> States
        {
            get { return _states; }
            set { _states = value; RaisePropertyChanged(); }
        }

        public StateMachineInputViewModel GetInputById(Guid id) => Inputs.FirstOrDefault(i => i.Id == id);

        private void InitiateModel()
        {
            AddStateViewModels();
            AddTransitionViewModel();
            InitializeInputViewModel();
            InitializeOutputViewModel();
            _isInitialized = true;

            States.CollectionChanged -= StatesCollectionChanged;
            States.CollectionChanged += StatesCollectionChanged;
            Transitions.CollectionChanged -= TransitionsCollectionChanged;
            Transitions.CollectionChanged += TransitionsCollectionChanged;
        }

        public void AddNewState(StateFactory factory, Point location)
        {
            var state = factory.Create();

            CreateNewState((StateType)state.StateType, location);
        }

        public void DeleteOfSelected()
        {
            _isBulkDeleteInProgress = true;
            var statesForDelete = States.Where(s => s.IsSelected).ToList();

            foreach (var stateForDelete in statesForDelete)
            {
                States.Remove(stateForDelete);
            }

            var transitionsForDelete = Transitions.Where(t => t.IsSelected).ToList();

            foreach (var transitionForDelete in transitionsForDelete)
            {
                Transitions.Remove(transitionForDelete);
            }

            _isBulkDeleteInProgress = false;
            SaveUndoState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateType"></param>
        /// <param name="location">If null, a new position will be chosen automatically.</param>
        private void CreateNewState(StateType stateType = StateType.Normal, Point? location = null)
        {
            if (location == null)
            {
                location = FindLocationForNewState();
            }

            //We can only have a single initial state. Oddly enough.
            if (stateType == StateType.Initial && States.Any(s => s.StateType == StateType.Initial))
            {
                MessageBox.Show("Only one initial state can be added to a state machine.");
                return;
            }

            //Come up with a name for this new state
            string name = stateType == StateType.Initial ? "Initial State" : States.CreateUniqueName("State {0}");

            //Create the new view model.
            var state = new StateViewModel(new State { Name = name, Id = Guid.NewGuid() }, this, _viewService, MessageBoxService)
            {
                Location = location.Value,
                StateType = stateType
            };

            States.Add(state);
            SaveUndoState();
        }

        private Point FindLocationForNewState()
        {
            Point point = new Point(100, 100);

            while (IsStatePositionInUse(point))
            {
                point += new Vector(10, 10);
            }

            return point;
        }

        private bool IsStatePositionInUse(Point point) => States.Any(s => Math.Abs(point.X - s.Location.X) < 10 && Math.Abs(point.Y - s.Location.Y) < 10);

        public override void OnDrop(DragEventArgs e)
        {
            var tool = e.Data.GetData(typeof(ITool)) as StateFactory;

            if (tool == null)
                return;

            var designCanvas = e.OriginalSource as Grid;

            if (designCanvas == null)
                return;

            var position = e.GetPosition(designCanvas);

            AddNewState(tool, position);

            e.Effects = DragDropEffects.Copy;

            e.Handled = true;
        }

        public override void OnPreviewMouseLeftButtonUp(MouseEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.LeftShift) && Keyboard.IsKeyUp(Key.LeftCtrl))
            {
                SelectionService.SelectNone();
            }

            SelectionService.Select(this);
        }

        private void AddTransitionViewModel()
        {
            Transitions.Clear();

            if (_model.Transitions != null)
            {
                foreach (var item in _model.Transitions)
                {
                    var source = States.First(s => s.Id == item.SourceStateId);
                    var target = States.First(s => s.Id == item.TargetStateId);
                    var transition = AddTransition(source, target, item.Name, item.Condition, item.TransitionActions, item.PullLength);
                }
            }
        }

        private void InitializeInputViewModel()
        {
            Inputs.Clear();

            if (_model.Inputs != null)
            {
                Inputs.AddRange(_model.Inputs.Select(i => new StateMachineInputViewModel(i, this)));
            }
        }

        private void InitializeOutputViewModel()
        {
            Outputs.Clear();

            if (_model.Actions != null)
            {
                Outputs.AddRange(_model.Actions.Select(i => new StateMachineOutputActionViewModel(i, this)));
            }
        }

        private void AddStateViewModels()
        {
            States.Clear();

            if (_model.States != null)
            {
                States.AddRange(_model.States.Select(st => new StateViewModel(st, this, _viewService, MessageBoxService)));
            }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                SaveUndoState();
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    RaisePropertyChanged();
                    SaveUndoState();
                }
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    RaisePropertyChanged();
                    SaveUndoState();
                }
            }
        }

        public bool IsPointerMode
        {
            get { return _isPointerMode; }
            set
            {
                _isPointerMode = value;
                RaisePropertyChanged();
            }
        }

        public Point NewTransitionStart
        {
            get { return _newTransitionStart; }
            private set
            {
                _newTransitionStart = value;
                RaisePropertyChanged();
            }
        }


        public Point NewTransitionEnd
        {
            get { return _newTransitionEnd; }
            private set
            {
                _newTransitionEnd = value;
                RaisePropertyChanged();
            }
        }


        public bool IsCreatingTransition
        {
            get { return _isCreatingTransition; }
            private set
            {
                _isCreatingTransition = value;
                RaisePropertyChanged();
            }
        }


        public bool IsConnectorMode
        {
            get { return _isConnectorMode; }
            set
            {
                _isConnectorMode = value;
                RaisePropertyChanged();
            }
        }

        public void StartCreatingTransition(StateViewModel source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IsCreatingTransition = true;
            NewTransitionStart = source.Location;
            NewTransitionEnd = source.Location;
            _sourceForNewTransition = source;
        }

        public void ContinueCreatingTransition(Point point)
        {
            NewTransitionEnd = point;
        }

        public ObservableCollection<TransitionViewModel> Transitions => _transitions;

        public void FinishCreatingTransition(Point point)
        {
            IsCreatingTransition = false;

            if (_sourceForNewTransition == null)
                return;

            //Attempt to find the target state
            var targetState = States.FirstOrDefault(s =>
            {
                var geometry = s.GetStateGeometry();

                return geometry.FillContains(point);
                //return true;
            });

            if (targetState != null)
            {
                var transition = AddTransition(_sourceForNewTransition, targetState, Transitions.CreateUniqueName("Transition {0}"));
            }
        }

        public StateMachine GetModel()
        {
            _model.States = States.Select(s => s.GetModel()).ToArray();

            //_model.Transitions = Transitions.Select(t => t.GetModel()).ToArray();
            var transitions = new List<StateMachineTransition>();

            foreach (var item in Transitions)
            {
                transitions.Add(item.GetModel());
            }
            _model.Transitions = transitions.ToArray();

            _model.Inputs = Inputs.Select(i => i.GetModel()).ToArray();
            _model.Actions = Outputs.Select(o => o.GetModel()).ToArray();
            return _model.Clone();
        }

        public void CancelCreatingTransition()
        {
            IsCreatingTransition = false;
        }

        public TransitionViewModel AddTransition(
            StateViewModel source,
            StateViewModel target,
            string transitionName,
            StateMachineCondition condition = null,
            Guid[] actionGuids = null,
            double pullLength = 0
            )
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            StateMachineTransition transition = new StateMachineTransition
            {
                Name = transitionName,
                SourceStateId = source.Id,
                TargetStateId = target.Id,
                Condition = condition, //?? new StateMachineCondition(),
                TransitionActions = actionGuids,
                PullLength = pullLength
            };

            var transitionViewModel = ApplicationContainer.Container.Resolve<TransitionViewModel>(
                    new TypedParameter(typeof(StateMachineViewModel), this),
                    new TypedParameter(typeof(StateMachineTransition), transition)
                );
            transitionViewModel.Source = source;
            transitionViewModel.Target = target;

            Transitions.Add(transitionViewModel);
            SaveUndoState();

            return transitionViewModel;
        }

        public void SaveUndoState()
        {
            if (_isInitialized && !_isBulkDeleteInProgress && !_isStateDeleteInProgress)
            {
                _undoProvider?.SaveUndoState();
                _dirtyService?.MarkDirty();
            }
        }

        public override void SelectBox(Rect rec)
        {
            var selection = States
                .Select(s => new { State = s, Rect = new Rect(s.Location, new Size(s.Width / 2, s.Height / 2)) })
                .Where(t => rec.Contains(t.Rect))
                .Select(t => t.State);

            foreach (var state in selection)
            {
                SelectionService.Select(state);
            }
        }

        private void TransitionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is TransitionViewModel transition)
                    {
                        transition.Source = null;
                        transition.Target = null;
                    }
                }

                SaveUndoState();
            }
        }

        private void StatesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                _isStateDeleteInProgress = true;

                foreach (var item in e.OldItems)
                {
                    var state = item as StateViewModel;

                    foreach (var transition in state?.Transitions?.ToList())
                    {
                        Transitions.Remove(transition);
                    }
                }

                _isStateDeleteInProgress = false;

                SaveUndoState();
            }
        }
    }
}
