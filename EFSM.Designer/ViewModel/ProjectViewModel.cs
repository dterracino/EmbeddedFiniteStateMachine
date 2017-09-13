using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
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
        private StateMachineProject _project;
        private string _filename = string.Empty;
        private IViewService _viewService;
        private readonly IIsDirtyService _dirtyService = new IsDirtyService();

        private ObservableCollection<StateMachineReferenceViewModel> _stateMachineViewModels = new ObservableCollection<StateMachineReferenceViewModel>();
        public ObservableCollection<StateMachineReferenceViewModel> StateMachineViewModels
        {
            get { return _stateMachineViewModels; }
            set { _stateMachineViewModels = value; RaisePropertyChanged(); }
        }

        public ProjectViewModel(StateMachineProject project, IViewService viewService)
        {
            _project = project;
            _viewService = viewService;
            InitializeStateMachines();
        }

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; RaisePropertyChanged(); }
        }

        private void InitializeStateMachines()
        {
            if (_project.StateMachines != null)
            {
                StateMachineViewModels.AddRange(_project.StateMachines
                    .Select(sm => ApplicationContainer.Container.Resolve<StateMachineReferenceViewModel>(new TypedParameter(typeof(StateMachine), sm))));
            }
        }

        public IIsDirtyService DirtyService
        {
            get { return _dirtyService; }
        }

        public void Save(IPersistor persistor, string fileName)
        {
            Filename = fileName;
            persistor.SaveProject(GetModel(), fileName);
        }

        public StateMachineProject GetModel()
        {
            _project.StateMachines = StateMachineViewModels.Select(vm => vm.GetModel()).ToArray();
            return _project;
        }
    }
}

