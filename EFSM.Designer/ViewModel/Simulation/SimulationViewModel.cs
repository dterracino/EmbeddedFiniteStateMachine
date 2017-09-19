using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class SimulationViewModel : ViewModelBase
    {
        private StateMachine _model;
        private StateViewModel _currentState = null;
        private StateMachineViewModel _stateMachineViewModel = null;

        public SimulationViewModel(StateMachine model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            InitializeModel();
            InitializeCommands();
            SelectInitialState();
        }

        public ICommand SimulationCommand { get; private set; }

        public StateViewModel CurrentState
        {
            get { return _currentState; }
            set
            {
                if (value != null)
                {
                    _currentState = value;
                }
            }
        }

        public StateMachineViewModel StateMachineViewModel
        {
            get { return _stateMachineViewModel; }
            set
            {
                if (_stateMachineViewModel != value)
                {
                    _stateMachineViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void InitializeCommands()
        {
            SimulationCommand = new RelayCommand(Simulate);
        }

        private void Simulate()
        {
            var transitionViewModels = StateMachineViewModel.Transitions.Where(t => t.Source.Id == _currentState.Id);

            foreach (var transitionViewModel in transitionViewModels)
            {
                if (IsConditionFulfilled(transitionViewModel))
                {
                    SetCurrentState(transitionViewModel.Target);
                }
            }
        }

        private bool IsConditionFulfilled(TransitionViewModel transition)
        {
            if (transition == null || transition.Condition == null || transition.Condition.SourceInputId == null)
            {
                return false;
            }

            var input = StateMachineViewModel.GetInputById(transition.Condition.SourceInputId.Value);

            if (input.Value != transition.Condition.Value)
            {
                return false;
            }

            return true;
        }

        private void InitializeModel()
        {
            StateMachineViewModel = new SimulationStateMachineViewModel(_model, ApplicationContainer.Container.Resolve<IViewService>());
        }

        private void SelectInitialState()
        {
            var initialState = StateMachineViewModel.States.First(s => s.StateType == Metadata.StateType.Initial);
            SetCurrentState(initialState);
        }

        private void SetCurrentState(StateViewModel state)
        {
            _currentState = state;
            StateMachineViewModel.SelectionService.SelectNone();
            StateMachineViewModel.SelectionService.Select(state);
        }
    }
}
