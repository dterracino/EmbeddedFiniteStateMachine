using Autofac;
using Cas.Common.WPF;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace EFSM.Designer.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly StateMachineProject _project;
        private readonly IIsDirtyService _dirtyService = new IsDirtyService();

        private readonly ObservableCollection<StateMachineReferenceViewModel> _stateMachineViewModels = new ObservableCollection<StateMachineReferenceViewModel>();

        public ProjectViewModel(StateMachineProject project)
        {
            _project = project;

            if (_project.StateMachines != null)
            {
                StateMachineViewModels.AddRange(_project.StateMachines
                    .Select(sm => ApplicationContainer.Container.Resolve<StateMachineReferenceViewModel>(new TypedParameter(typeof(StateMachine), sm))));
            }
        }

        public ObservableCollection<StateMachineReferenceViewModel> StateMachineViewModels
        {
            get { return _stateMachineViewModels; }
        }

        public IIsDirtyService DirtyService
        {
            get { return _dirtyService; }
        }

        public StateMachineProject GetModel()
        {
            _project.StateMachines = StateMachineViewModels.Select(vm => vm.GetModel()).ToArray();
            return _project;
        }
    }
}

