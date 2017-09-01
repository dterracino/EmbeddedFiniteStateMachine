using EFSM.Designer.Metadata;
using EFSM.Designer.Model;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using System;
using Xunit;

namespace EFSM.Designer.Tests
{
    public class StateMachineDialogWindowViewModelTest
    {
        [Fact]
        public void StateMachineNameChanged_UndoPerformed_OriginalNameAssigned()
        {
            var stateMachine = new StateMachine() { Name = "Name1" };
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.Name = "Name2";
            viewModel.UndoCommand.Execute(null);
            Assert.Equal(viewModel.StateMachineViewModel.Name, "Name1");
        }

        [Fact]
        public void StateNameChanged_UndoPerformed_OriginalNameAssigned()
        {
            var stateMachine = new StateMachine { States = new[] { new State { Name = "Name1" } } };
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.States[0].Name = "Name2";
            viewModel.UndoCommand.Execute(null);
            Assert.Equal(viewModel.StateMachineViewModel.States[0].Name, "Name1");
        }

        [Fact]
        public void StateNameChangedTwice_UndoOncePerformed_ProperStateNameSet()
        {
            var stateMachine = new StateMachine { States = new[] { new State { Name = "Name1" } } };
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.States[0].Name = "Name2";
            viewModel.StateMachineViewModel.States[0].Name = "Name3";
            viewModel.UndoCommand.Execute(null);
            Assert.Equal(viewModel.StateMachineViewModel.States[0].Name, "Name2");
        }

        [Fact]
        public void NewStateAdded_UndoPerformed_NoStatesInViewModel()
        {
            var viewModel = new StateMachineDialogWindowViewModel(new StateMachine());
            viewModel.StateMachineViewModel.AddNewState(CreateStateFactory(), new System.Windows.Point(0, 0));
            Assert.Equal(1, viewModel.StateMachineViewModel.States.Count);
            viewModel.UndoCommand.Execute(null);
            Assert.Equal(0, viewModel.StateMachineViewModel.States.Count);
        }

        [Fact]
        public void StateMachineNameChanged_UndoAndRedoPerformed_ProperStateMachineNameIsSet()
        {
            var stateMachine = new StateMachine { States = new[] { new State { Name = "Name1" } } };
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.States[0].Name = "Name2";
            viewModel.UndoCommand.Execute(null);
            viewModel.RedoCommand.Execute(null);
            Assert.Equal(viewModel.StateMachineViewModel.States[0].Name, "Name2");
        }

        [Fact]
        public void NewStateAddedUndoPerformed_AddAndUndoAgain_NoStatesInViewModel()
        {
            var viewModel = new StateMachineDialogWindowViewModel(new StateMachine());
            viewModel.StateMachineViewModel.AddNewState(CreateStateFactory(), new System.Windows.Point(0, 0));
            viewModel.UndoCommand.Execute(null);
            viewModel.StateMachineViewModel.AddNewState(CreateStateFactory(), new System.Windows.Point(0, 0));
            viewModel.UndoCommand.Execute(null);
            Assert.Equal(0, viewModel.StateMachineViewModel.States.Count);
        }

        [Fact]
        public void TrasitionCreatedAndRenamed_UndoPerformed_TransitionProperNameSet()
        {
            var stateMachine = CreateStateMachineWithStatesAndTransition();
            stateMachine.Transitions[0].Name = "Name1";
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.Transitions[0].Name = "Name2";
            viewModel.UndoCommand.Execute(null);
            Assert.Equal("Name1", viewModel.StateMachineViewModel.Transitions[0].Name);
        }

        [Fact]
        public void TransitionDeleted_UndoPerformed_TransitionReturned()
        {
            var stateMachine = CreateStateMachineWithStatesAndTransition();
            var viewModel = new StateMachineDialogWindowViewModel(stateMachine);
            viewModel.StateMachineViewModel.Transitions[0].DeleteCommand.Execute(null);
            viewModel.UndoCommand.Execute(null);
            Assert.NotEmpty(viewModel.StateMachineViewModel.Transitions);
        }

        private StateFactory CreateStateFactory()
        {
            return new StateFactory("CategoryName", "State", () => new State()
            {
                StateType = (int)StateType.Normal
            });
        }

        private StateMachine CreateStateMachineWithStatesAndTransition()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            var stateMachine = new StateMachine
            {
                Transitions = new[] { new StateMachineTransition { Name = "Name1", SourceStateId = guid1, TargetStateId = guid2 } },
                States = new[] { new State { Id = guid1 }, new State { Id = guid2 } }

            };
            return stateMachine;
        }
    }
}
