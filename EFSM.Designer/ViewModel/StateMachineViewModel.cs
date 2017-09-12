using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Designer.Metadata;
using EFSM.Designer.Model;
using EFSM.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineViewModel : DesignerViewModelBase
    {
        public ICommand OnMouseLeftButtonDownCommand { get; private set; }
        public ICommand OnDropCommand { get; private set; }

        private StateViewModel _sourceForNewTransition;
        private StateMachine _model;

        private IUndoProvider _undoProvider;
        private IIsDirtyService _dirtyService;

        private ObservableCollection<StateMachineInputViewModel> _inputs = new ObservableCollection<StateMachineInputViewModel>();
        public ObservableCollection<StateMachineInputViewModel> Inputs
        {
            get { return _inputs; }
            set { _inputs = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<StateViewModel> _states = new ObservableCollection<StateViewModel>();
        public ObservableCollection<StateViewModel> States
        {
            get { return _states; }
            set { _states = value; RaisePropertyChanged(); }
        }

        public StateMachineViewModel(StateMachine model, IViewService viewService, IUndoProvider undoProvider, IIsDirtyService dirtyService)
        {
            _model = model;
            _undoProvider = undoProvider;
            _dirtyService = dirtyService;

            InitiateModel();


            OnMouseLeftButtonDownCommand = new RelayCommand<MouseEventArgs>(OnMouseLeftButtonDown);
            OnDropCommand = new RelayCommand<DragEventArgs>(OnDrop);
        }

        private void InitiateModel()
        {
            AddStateViewModels();
            AddTransitionViewModel();
            AddInputViewModel();
        }




        public void AddNewState(StateFactory factory, Point location)
        {
            var state = factory.Create();

            CreateNewState((StateType)state.StateType, location);
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
            string name = stateType == StateType.Initial ? "Initial State" : States.CreateUniqueName("State {0}"); ;

            //Create the new view model.
            var state = new StateViewModel(new State { Name = name, Id = Guid.NewGuid() }, this, _undoProvider)
            {
                Location = location.Value
            };

            States.Add(state);
            _undoProvider.SaveUndoState();
            _dirtyService.MarkDirty();
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

        private bool IsStatePositionInUse(Point point)
        {
            return States.Any(s => Math.Abs(point.X - s.Location.X) < 10 && Math.Abs(point.Y - s.Location.Y) < 10);
        }

        private void OnDrop(DragEventArgs e)
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

        private void OnMouseLeftButtonDown(MouseEventArgs e)
        {
            SelectionService.SelectNone();
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
                    var transition = AddTransition(source, target, item.Name, item.Condition);
                }
            }
        }

        private void AddInputViewModel()
        {
            Inputs.Clear();

            if (_model.Inputs != null)
            {
                Inputs.AddRange(_model.Inputs.Select(i => new StateMachineInputViewModel(i)));
            }
        }

        private void AddStateViewModels()
        {
            States.Clear();

            if (_model.States != null)
            {
                States.AddRange(_model.States.Select(st => new StateViewModel(st, this, _undoProvider)));
            }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                _undoProvider.SaveUndoState();
                _dirtyService.MarkDirty();
            }
        }

        private const double DefaultWidth = 800;
        private const double DefaultHeight = 800;

        private double _width = DefaultWidth;
        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    RaisePropertyChanged();
                    _undoProvider.SaveUndoState();
                    _dirtyService.MarkDirty();
                }
            }
        }

        private double _height = DefaultHeight;
        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    RaisePropertyChanged();
                    _undoProvider.SaveUndoState();
                    _dirtyService.MarkDirty();
                }
            }
        }

        private bool _isPointerMode = true;
        public bool IsPointerMode
        {
            get { return _isPointerMode; }
            set { _isPointerMode = value; RaisePropertyChanged(); }
        }

        public bool IsReadOnly => false;

        private Point _newTransitionStart;
        public Point NewTransitionStart
        {
            get { return _newTransitionStart; }
            private set { _newTransitionStart = value; RaisePropertyChanged(); }
        }

        private Point _newTransitionEnd;
        public Point NewTransitionEnd
        {
            get { return _newTransitionEnd; }
            private set { _newTransitionEnd = value; RaisePropertyChanged(); }
        }

        private bool _isCreatingTransition;
        public bool IsCreatingTransition
        {
            get { return _isCreatingTransition; }
            private set { _isCreatingTransition = value; RaisePropertyChanged(); }
        }

        private bool _isConnectorMode;
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
            //NewTransitionStart = source.Location;
            //NewTransitionEnd = source.Location;
            _sourceForNewTransition = source;
        }

        public void ContinueCreatingTransition(Point point)
        {
            NewTransitionEnd = point;
        }

        private readonly ObservableCollection<TransitionViewModel> _transitions = new ObservableCollection<TransitionViewModel>();
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
            _model.Transitions = Transitions.Select(t => t.GetModel()).ToArray();
            _model.Inputs = Inputs.Select(i => i.GetModel()).ToArray();
            return _model.Clone();
        }

        internal void RemoveTransition(TransitionViewModel transition)
        {
            if (_transitions.Contains(transition))
            {
                _transitions.Remove(transition);
                _undoProvider.SaveUndoState();
                _dirtyService.MarkDirty();
            }
        }


        public void CancelCreatingTransition()
        {
            IsCreatingTransition = false;
        }

        public TransitionViewModel AddTransition(StateViewModel source, StateViewModel target, string transitionName, StateMachineCondition condition = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            StateMachineTransition transition = new StateMachineTransition { Name = transitionName, SourceStateId = source.Id, TargetStateId = target.Id, Condition = condition };

            var transitionViewModel = new TransitionViewModel(this, transition, _undoProvider)
            {
                Source = source,
                Target = target,

            };

            Transitions.Add(transitionViewModel);

            return transitionViewModel;
        }
    }
}
