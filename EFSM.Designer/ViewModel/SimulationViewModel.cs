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
        private StateMachineDialogWindowViewModel _parent;


        private TransitionViewModel _currentTransition = null;
        private StateViewModel _currentState => _currentTransition?.Source;

        public ICommand SimulationCommand { get; private set; }

        private StateMachineViewModel _stateMachineViewModel = null;
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

        public SimulationViewModel(StateMachine model, StateMachineDialogWindowViewModel parent)
        {
            _model = model;
            _parent = parent;
            InitializeModel();
            InitializeCommands();
            SelectInitialState();
        }

        private void InitializeCommands()
        {
            SimulationCommand = new RelayCommand(Simulate);
        }

        private void Simulate()
        {
            Guid? inputGuid = _currentTransition.Condition.SourceInputId;

            if (inputGuid != null)
            {
                var input = GetInputByGuid(inputGuid.Value);

                if (input.IsOn == _currentTransition.Condition.Value.Value)
                {
                    StateMachineViewModel.SelectionService.Select(_currentTransition.Target);
                }
            }

        }

        private StateMachineInputViewModel GetInputByGuid(Guid id)
        {
            return StateMachineViewModel.Inputs.First(i => i.Id == id);
        }

        private void InitializeModel()
        {
            StateMachineViewModel = new StateMachineViewModel(_model, ApplicationContainer.Container.Resolve<IViewService>(), _parent, _parent.DirtyService);
        }

        private void SelectInitialState()
        {
            var initialTransition = StateMachineViewModel.Transitions.First(t => t.Source.StateType == Metadata.StateType.Initial);
            SetCurrentTransition(initialTransition);
        }

        private void SetCurrentTransition(TransitionViewModel transition)
        {
            _currentTransition = transition;
            StateMachineViewModel.SelectionService.Select(transition.Source);
        }
    }
}
