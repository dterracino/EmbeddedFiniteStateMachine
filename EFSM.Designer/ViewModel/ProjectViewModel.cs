using Autofac;
using Cas.Common.WPF;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Extensions;
using GalaSoft.MvvmLight.CommandWpf;

namespace EFSM.Designer.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly StateMachineProject _project;
        private readonly IIsDirtyService _dirtyService = new IsDirtyService();

        private readonly ObservableCollection<StateMachineReferenceViewModel> _stateMachines = new ObservableCollection<StateMachineReferenceViewModel>();
        private StateMachineReferenceViewModel _selectedStateMachine;

        public ProjectViewModel(StateMachineProject project)
        {
            _project = project;

            if (_project.StateMachines != null)
            {
                StateMachines.AddRange(_project.StateMachines
                    .Select(sm => ApplicationContainer.Container.Resolve<StateMachineReferenceViewModel>(new TypedParameter(typeof(StateMachine), sm))));
            }

            NewStateMachineCommand = new RelayCommand(NewStateMachine);
            DeleteStateMachineCommand = new RelayCommand(DeleteStateMachine, CanDeleteStateMachine);
        }

        public ICommand NewStateMachineCommand { get; }
        public ICommand DeleteStateMachineCommand { get; }

        private void NewStateMachine()
        {
            var textEditService = new TextEditService();

            string initialName = StateMachines
                .Select(sm => sm.Name)
                .CreateUniqueName("State Machine {0}");

            textEditService.EditText(initialName, "State Machine Name", "Create State Machine", name =>
            {
                var model = new StateMachine()
                {
                    Name = name,
                    Actions = new StateMachineOutputAction[] { },
                    Inputs = new StateMachineInput[] { }
                };

                var viewService = ApplicationContainer.Container.Resolve<IViewService>();

                var viewModel = new StateMachineReferenceViewModel(model, viewService);

                StateMachines.Add(viewModel);
            });
        }

        private void DeleteStateMachine()
        {
            _stateMachines.Remove(SelectedStateMachine);
            _dirtyService.MarkDirty();
        }

        private bool CanDeleteStateMachine()
        {
            return SelectedStateMachine != null;
        }

        public ObservableCollection<StateMachineReferenceViewModel> StateMachines
        {
            get { return _stateMachines; }
        }

        public IIsDirtyService DirtyService
        {
            get { return _dirtyService; }
        }

        public StateMachineReferenceViewModel SelectedStateMachine
        {
            get { return _selectedStateMachine; }
            set
            {
                _selectedStateMachine = value; 
                RaisePropertyChanged();
            }
        }

        public StateMachineProject GetModel()
        {
            _project.StateMachines = StateMachines.Select(vm => vm.GetModel()).ToArray();
            return _project;
        }
    }
}

