using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Designer.Metadata;
using EFSM.Designer.Model;
using EFSM.Designer.ViewModel.StateEditor;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateViewModel : ViewModelBase, IMoveable, IDeleteable, ISelectable, IPropertyGridSource, INamedItem
    {
        private const double DefaultHeightWidth = 100;
        private double _width = DefaultHeightWidth;
        private double _height = DefaultHeightWidth;
        private State _model;
        public StateMachineViewModel Parent { get; }
        private Point _startLocation;
        private readonly IViewService _viewService;
        private readonly List<TransitionViewModel> _transitions = new List<TransitionViewModel>();
        public List<TransitionViewModel> Transitions => _transitions;
        private IMessageBoxService _messageBoxService;

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public StateViewModel(State state, StateMachineViewModel parent, IViewService viewService, IMessageBoxService messageBoxService)
        {
            _model = state ?? throw new ArgumentNullException(nameof(state));
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Location = new Point(_model.X, _model.Y);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            _propertyGridSource = new StatePropertyGridSource(this);
        }

        private bool CanEdit() => !Parent.IsReadOnly;

        private bool CanDelete() => !Parent.IsReadOnly;

        private void Edit()
        {
            try
            {
                var viewModel = new StateDialogViewModel(Parent, GetModel(), Reload, _messageBoxService);

                _viewService.ShowDialog(viewModel);
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void Reload(State newModel)
        {
            _model = newModel;
            RaisePropertyChanged(nameof(Name));
            Parent.SaveUndoState();
        }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                _model.X = value.X;
                _model.Y = value.Y;
                RaisePropertyChanged();
                //_undoProvider.SaveUndoState();
            }
        }

        public void SoftMove(Point point)
        {
            _location = point;
            RaisePropertyChanged(() => Location);
        }

        public double Width
        {
            get { return _width; }
            set { _width = value; RaisePropertyChanged(); }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; RaisePropertyChanged(); }
        }

        public Guid Id
        {
            get { return _model.Id; }
            set
            {
                _model.Id = value;
                RaisePropertyChanged();
                Parent.SaveUndoState();
            }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                Parent.SaveUndoState();
            }
        }

        public Guid[] EntryActions
        {
            get { return _model.EntryActions; }
            set { _model.EntryActions = value; RaisePropertyChanged(); }
        }

        public Guid[] ExitActions
        {
            get { return _model.EntryActions; }
            set { _model.EntryActions = value; RaisePropertyChanged(); }
        }

        public double X
        {
            get { return _model.X; }
            set { _model.X = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public double Y
        {
            get { return _model.Y; }
            set { _model.Y = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public StateType StateType
        {
            get { return (StateType)_model.StateType; }
            set { _model.StateType = (int)value; RaisePropertyChanged(); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; RaisePropertyChanged(); }
        }

        private readonly StatePropertyGridSource _propertyGridSource;
        public object PropertyGridData => _propertyGridSource;

        public State GetModel()
        {
            _model.X = Location.X;
            _model.Y = Location.Y;
            return _model.Clone();
        }

        public void DeleteAction(Guid outputId)
        {
            if (EntryActions != null && EntryActions.Contains(outputId))
            {
                var actions = new List<Guid>(EntryActions.Where(x => x != outputId));
                EntryActions = actions.ToArray();
            }

            if (ExitActions != null && ExitActions.Contains(outputId))
            {
                var actions = new List<Guid>(ExitActions.Where(x => x != outputId));
                ExitActions = actions.ToArray();
            }
        }

        public void StartMove()
        {
            _startLocation = Location;
        }

        public void ContinueMove(Vector vector)
        {
            SoftMove(_startLocation + vector);
        }

        public void CancelMove()
        {
            SoftMove(_startLocation);
        }

        public void CompleteMove(Vector vector)
        {
            _location = _startLocation + vector;
            RaisePropertyChanged();
            Parent.SaveUndoState();
        }

        internal void RemoveTransition(TransitionViewModel transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));

            if (_transitions.Contains(transition))
            {
                _transitions.Remove(transition);
            }
        }

        internal void AddTransition(TransitionViewModel transition)
        {
            if (transition == null) throw new ArgumentNullException(nameof(transition));

            if (!_transitions.Contains(transition))
            {
                _transitions.Add(transition);
            }
        }

        public void Delete()
        {
            try
            {
                Parent.DeleteOfSelected();
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }
    }
}