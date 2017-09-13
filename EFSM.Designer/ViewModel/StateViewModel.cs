using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Designer.Metadata;
using EFSM.Designer.Model;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateViewModel : ViewModelBase, IMoveable, IDeleteable, ISelectable, IPropertyGridSource, INamedItem
    {
        private const double DefaultHeightWidth = 100;
        private double _width = DefaultHeightWidth;
        private double _height = DefaultHeightWidth;
        private State _state;
        public StateMachineViewModel Parent { get; private set; }
        private Point _startLocation;

        private readonly List<TransitionViewModel> _transitions = new List<TransitionViewModel>();

        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public StateViewModel(State state, StateMachineViewModel parent)
        {
            _state = state;

            Parent = parent;
            Location = new Point(_state.X, _state.Y);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            _propertyGridSource = new StatePropertyGridSource(this);
        }

        private bool CanEdit()
        {
            return !Parent.IsReadOnly;
        }

        private bool CanDelete()
        {
            return !Parent.IsReadOnly;
        }

        private void Edit()
        {
            //throw new NotImplementedException();
        }

        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                _state.X = value.X;
                _state.Y = value.Y;
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
            get { return _state.Id; }
            set { _state.Id = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public string Name
        {
            get { return _state.Name; }
            set { _state.Name = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public Guid[] EntryActions
        {
            get { return _state.EntryActions; }
            set { _state.EntryActions = value; RaisePropertyChanged(); }
        }

        public Guid[] ExitActions
        {
            get { return _state.EntryActions; }
            set { _state.EntryActions = value; RaisePropertyChanged(); }
        }

        public double X
        {
            get { return _state.X; }
            set { _state.X = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public double Y
        {
            get { return _state.Y; }
            set { _state.Y = value; RaisePropertyChanged(); Parent.SaveUndoState(); }
        }

        public StateType StateType
        {
            get { return (StateType)_state.StateType; }
            set { _state.StateType = (int)value; RaisePropertyChanged(); }
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
            return _state.Clone();
        }

        public void StartMove()
        {
            _startLocation = Location;
        }

        public void ContinueMove(Vector vector)
        {
            //throw new NotImplementedException();
        }

        public void CancelMove()
        {
            //
        }

        public void CompleteMove(Vector vector)
        {
            _location = _startLocation + vector;
            RaisePropertyChanged();
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
            foreach (var transition in _transitions.ToArray())
            {
                Parent.RemoveTransition(transition);
                _transitions.Remove(transition);
            }

            Parent.States.Remove(this);
            Parent.SaveUndoState();
        }
    }
}