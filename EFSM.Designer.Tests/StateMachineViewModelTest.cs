using Cas.Common.WPF;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using System;
using Xunit;

namespace EFSM.Designer.Tests
{
    public class StateMachineViewModelTest
    {
        [Fact]
        public void GetModel_InsertedStateViewModels_StateMachineHasStates()
        {
            var stateMachine = new StateMachine();
            var provider = new StateMachineDialogWindowViewModel(stateMachine);
            var viewModel = new StateMachineViewModel(stateMachine, new ViewService(), provider);
            viewModel.States.Add(new StateViewModel(new State(), viewModel, provider));
            StateMachine stateMachineModel = viewModel.GetModel();
            Assert.Equal(1, stateMachineModel.States.Length);
        }

        [Fact]
        public void AddTransition_DeleteTransition_TransitionDeleted()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            var stateMachine = new StateMachine
            {
                Transitions = new[] { new StateMachineTransition { Name = "Name1", SourceStateId = guid1, TargetStateId = guid2 } },
                States = new[] { new State { Id = guid1 }, new State { Id = guid2 } }

            };
            var vm = new StateMachineDialogWindowViewModel(stateMachine);
            TransitionViewModel transition = vm.StateMachineViewModel.Transitions[0];
            transition.DeleteCommand.Execute(null);
            Assert.Empty(vm.StateMachineViewModel.Transitions);
        }
    }
}
