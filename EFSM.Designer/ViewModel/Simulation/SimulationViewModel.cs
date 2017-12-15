using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Extensions;
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
        private IMessageBoxService _messageBoxService;

        public SimulationViewModel(StateMachine model, IMessageBoxService messageBoxService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
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
            try
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
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private bool IsConditionFulfilled(TransitionViewModel transition)
        {
            if (transition == null || transition.Condition == null || transition.Condition.SourceInputId == null)
            {
                return false;
            }

            var input = StateMachineViewModel.GetInputById(transition.Condition.SourceInputId.Value);

            //if (input.Value != transition.Condition.Value)
            //{
            //    return false;
            //}

            return true;
        }

        private void InitializeModel()
        {
            var viewService = ApplicationContainer.Container.Resolve<IViewService>();
            StateMachineViewModel = new SimulationStateMachineViewModel(_model, viewService, _messageBoxService);
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
