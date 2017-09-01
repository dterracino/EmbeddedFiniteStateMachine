using Autofac;
using EFSM.Designer.Model;
using GalaSoft.MvvmLight;
using System;

namespace EFSM.Designer.ViewModel
{
    public static class ViewModelLocator
    {
        private static readonly Lazy<MainViewModel> _main = new Lazy<MainViewModel>(CreateMain);

        private static MainViewModel CreateMain()
        {
            MainViewModel main = ApplicationContainer.Container.Resolve<MainViewModel>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //main.StateMachineProjectViewModel = new StateMachineProjectViewModel(new StateMachineProject(null)
                //{
                //    StateMachines = new[]
                //                   {
                //        new StateMachine() { Name = "State Machine 1"},
                //        new StateMachine() { Name = "State Machine 2"},
                //        new StateMachine() { Name = "State Machine 3"}
                //    }
                //});
            }

            return main;
        }

        public static StateMachineViewModel StateMachineDialogViewModel
        {
            get { return ApplicationContainer.Container.Resolve<StateMachineViewModel>(); }
        }

        //public static MainViewModel Main => _main.Value;

        public static MainViewModel Main
        {
            get
            {
                return ApplicationContainer.Container.Resolve<MainViewModel>();
            }
        }

        public static ToolsViewModel<StateFactory> StateTools
        {
            get
            {
                return new ToolsViewModel<StateFactory>(new StateTools().Tools);
            }
        }
    }
}