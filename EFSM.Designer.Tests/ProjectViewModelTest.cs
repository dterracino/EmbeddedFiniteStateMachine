using Cas.Common.WPF;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using Xunit;

namespace EFSM.Designer.Tests
{
    public class ProjectViewModelTest
    {
        [Fact]
        public void InstanceCreated()
        {
            var instance = new ProjectViewModel(new StateMachineProject(), new ViewService());
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetModel_InsertedStateMachineView_ProjectHasStateMachine()
        {
            var viewModel = new ProjectViewModel(new StateMachineProject(), new ViewService());
            viewModel.StateMachineViewModels.Add(new StateMachineReferenceViewModel(new StateMachine(), new ViewService()));
            StateMachineProject projectModel = viewModel.GetModel();
            Assert.Equal(1, projectModel.StateMachines.Length);
        }
    }
}
