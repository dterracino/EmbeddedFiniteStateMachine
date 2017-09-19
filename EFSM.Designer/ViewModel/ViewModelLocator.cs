using Autofac;
using EFSM.Designer.Model;

namespace EFSM.Designer.ViewModel
{
    public static class ViewModelLocator
    {
        public static MainViewModel Main
        {
            get { return ApplicationContainer.Container.Resolve<MainViewModel>(); }
        }

        public static ToolsViewModel<StateFactory> StateTools
        {
            get { return new ToolsViewModel<StateFactory>(new StateTools().Tools); }
        }
    }
}