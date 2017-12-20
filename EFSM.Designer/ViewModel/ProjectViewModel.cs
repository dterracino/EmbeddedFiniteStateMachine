using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Engine;
using EFSM.Designer.Extensions;
using EFSM.Domain;
using EFSM.Generator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly StateMachineProject _project;
        private readonly IMarkDirty _dirtyService;
        private IMessageBoxService _messageBoxService;

        private const string DocumentationFileName = "doc.md";

        private readonly ObservableCollection<StateMachineReferenceViewModel> _stateMachines = new ObservableCollection<StateMachineReferenceViewModel>();
        private StateMachineReferenceViewModel _selectedStateMachine;
        private readonly GenerationOptionsViewModel _generationOptions;

        public ProjectViewModel(StateMachineProject project, IMarkDirty dirtyService, IMessageBoxService messageBoxService)
        {
            _project = project;
            _dirtyService = dirtyService;
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            if (_project.StateMachines != null)
            {
                StateMachines.AddRange(_project.StateMachines
                    .Select(sm => ApplicationContainer.Container.Resolve<StateMachineReferenceViewModel>(
                        new TypedParameter(typeof(StateMachine), sm),
                        new TypedParameter(typeof(IDirtyService), _dirtyService)
                        )));
            }

            _generationOptions = new GenerationOptionsViewModel(project.GenerationOptions, dirtyService, _messageBoxService);

            NewStateMachineCommand = new RelayCommand(NewStateMachine);
            DeleteStateMachineCommand = new RelayCommand(DeleteStateMachine, CanDeleteStateMachine);
            GenerateCommand = new RelayCommand<string>((s) => Generate(s), (s) => CanGenerate(s));
        }

        public ICommand NewStateMachineCommand { get; }
        public ICommand DeleteStateMachineCommand { get; }
        public ICommand GenerateCommand { get; }

        private void Generate(string projectPath)
        {
            try
            {
                //Create the generator
                var generator = new EmbeddedCodeGenerator();

                //Get the project model
                var project = GetModel();

                string documentationFullPath = string.Empty;

                if (!string.IsNullOrWhiteSpace(GenerationOptions.DocumentationFolder))
                {
                    var directory = Path.Combine(projectPath, GenerationOptions.DocumentationFolder ?? string.Empty);

                    Directory.CreateDirectory(directory);

                    documentationFullPath = Path.Combine(directory, DocumentationFileName);

                    new MarkdownGenerator().Generate(project.StateMachines.ToList(), documentationFullPath);
                }

                //Massage it real nice like
                project.Massage();

                try
                {
                    //Generate. Do it now.
                    generator.Generate(project, projectPath);
                }
                catch (Exception ex)
                {
                    string errorMessage = "\nException during code generation: " + ex.Message;

                    if (!string.IsNullOrWhiteSpace(documentationFullPath))
                    {
                        AppendTextToFile(documentationFullPath, errorMessage);
                    }

                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void AppendTextToFile(string fileFullPath, string text)
        {
            using (var streamWriter = File.AppendText(fileFullPath))
            {
                streamWriter.WriteLine(text);
            }
        }

        public void GenerateProjectCCodeAndDocumentation(string projectPath)
        {
            if (CanGenerate(projectPath))
            {
                Generate(projectPath);
            }
        }

        private bool CanGenerate(string projectPath)
        {
            return StateMachines.Any() && !string.IsNullOrWhiteSpace(projectPath);
        }

        private void NewStateMachine()
        {
            try
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

                    var viewModel = new StateMachineReferenceViewModel(model, viewService, DirtyService, _messageBoxService);

                    StateMachines.Add(viewModel);

                    _dirtyService.MarkDirty();
                });
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void DeleteStateMachine()
        {
            try
            {
                _stateMachines.Remove(SelectedStateMachine);
                _dirtyService.MarkDirty();
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
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
            _project.GenerationOptions = GenerationOptions.GetModel();
            return _project;
        }
    }
}

