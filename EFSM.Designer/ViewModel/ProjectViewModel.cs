using System;
using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Extensions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EFSM.Designer.Engine;
using EFSM.Generator;

namespace EFSM.Designer.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly StateMachineProject _project;
        private readonly IMarkDirty _dirtyService;

        private readonly ObservableCollection<StateMachineReferenceViewModel> _stateMachines = new ObservableCollection<StateMachineReferenceViewModel>();
        private StateMachineReferenceViewModel _selectedStateMachine;
        private readonly GenerationOptionsViewModel _generationOptions;

        public ProjectViewModel(StateMachineProject project, IMarkDirty dirtyService)
        {
            _project = project;
            _dirtyService = dirtyService;

            if (_project.StateMachines != null)
            {
                StateMachines.AddRange(_project.StateMachines
                    .Select(sm => ApplicationContainer.Container.Resolve<StateMachineReferenceViewModel>(
                        new TypedParameter(typeof(StateMachine), sm),
                        new TypedParameter(typeof(IDirtyService), _dirtyService)
                        )));
            }

            _generationOptions = new GenerationOptionsViewModel(project.GenerationOptions, dirtyService);

            NewStateMachineCommand = new RelayCommand(NewStateMachine);
            DeleteStateMachineCommand = new RelayCommand(DeleteStateMachine, CanDeleteStateMachine);
            GenerateCommand = new RelayCommand(Generate, CanGenerate);
        }

        public ICommand NewStateMachineCommand { get; }
        public ICommand DeleteStateMachineCommand { get; }
        public ICommand GenerateCommand { get; }

        private void Generate()
        {
            try
            {
                //Create the generator
                var generator = new EmbeddedCodeGenerator();

                //Get the project model
                var project = GetModel();

                //Massage it real nice like
                project.Massage();

                //Generate. Do it now.
                generator.Generate(project);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanGenerate() => StateMachines.Any();

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

                var viewModel = new StateMachineReferenceViewModel(model, viewService, DirtyService);

                StateMachines.Add(viewModel);

                _dirtyService.MarkDirty();
            });
        }

        private void DeleteStateMachine()
        {
            _stateMachines.Remove(SelectedStateMachine);
            _dirtyService.MarkDirty();
        }

        private bool CanDeleteStateMachine() => SelectedStateMachine != null;

        public ObservableCollection<StateMachineReferenceViewModel> StateMachines => _stateMachines;

        public IMarkDirty DirtyService => _dirtyService;

        public StateMachineReferenceViewModel SelectedStateMachine
        {
            get { return _selectedStateMachine; }
            set
            {
                _selectedStateMachine = value;
                RaisePropertyChanged();
            }
        }

        public GenerationOptionsViewModel GenerationOptions
        {
            get { return _generationOptions; }
        }

        public StateMachineProject GetModel()
        {
            _project.StateMachines = StateMachines.Select(vm => vm.GetModel()).ToArray();
            return _project;
        }
    }
}

