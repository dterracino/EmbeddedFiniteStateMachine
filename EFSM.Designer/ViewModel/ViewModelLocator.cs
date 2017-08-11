using System;
using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public static class ViewModelLocator
    {
        private static readonly Lazy<MainViewModel> _main = new Lazy<MainViewModel>(CreateMain);

        private static MainViewModel CreateMain()
        {
            var main = new MainViewModel();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                main.StateMachines.Add(new StateMachineViewModel(new StateMachine() { Name = "State Machine 1"}));
                main.StateMachines.Add(new StateMachineViewModel(new StateMachine() { Name = "State Machine 2" }));
                main.StateMachines.Add(new StateMachineViewModel(new StateMachine() { Name = "State Machine 3" }));
            }


            return main;
        }

        public static MainViewModel Main => _main.Value;
    }
}