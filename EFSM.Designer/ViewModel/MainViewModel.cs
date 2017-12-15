using Autofac;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Const;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using EFSM.Designer.Messages;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class MainViewModel : ViewModelBase, ICloseableViewModel
    {
        public IViewService _viewService;
        public IPersistor _persistor;

        private ProjectViewModel _project;
        private string _filename;
        private IMessageBoxService _messageBoxService;

        private readonly IDirtyService _dirtyService;

        public MainViewModel(IViewService viewService, IPersistor persistor, IDirtyService dirtyService, IMessageBoxService messageBoxService)
        {
            _viewService = viewService;
            _persistor = persistor;
            _dirtyService = dirtyService;
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            SaveCommand = new RelayCommand(() => Save(), CanSave);
            SaveAsCommand = new RelayCommand(() => SaveAs());
            OpenCommand = new RelayCommand(Open);
            NewCommand = new RelayCommand(New);
            EditCommand = new RelayCommand<StateMachineReferenceViewModel>(Edit);

            _dirtyService.PropertyChanged += DirtyService_PropertyChanged;

            New();
            MessengerInstance.Register<SaveMessage>(this, Save);
        }

        private void DirtyService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(() => Title);
        }

        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }

        public ProjectViewModel Project
        {
            get { return _project; }
            private set
            {
                _project = value;
                RaisePropertyChanged();
            }
        }

        public string Filename
        {
            get { return _filename; }
            private set
            {
                _filename = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(nameof(ProjectDirectory));
            }
        }


        public string ProjectDirectory => Path.GetDirectoryName(Filename);

        public string Title
        {
            get
            {
                string prefix = _dirtyService.IsDirty ? "*" : "";

                string fileDisplayName = string.IsNullOrWhiteSpace(Filename) ? "Untiteld" : Filename;

                return $"{prefix}{fileDisplayName} - State Machine Designer";
            }
        }

        private void Edit(StateMachineReferenceViewModel stateMachineViewModel)
        {
            try
            {
                stateMachineViewModel.Edit();
            }
            catch (Exception e)
            {
                _messageBoxService.Show(e);
            }
        }

        private void New()
        {
            try
            {
                if (Save())
                {
                    Filename = null;

                    Project = new ProjectViewModel(_persistor.Create(), _dirtyService, _messageBoxService);

                    _dirtyService.MarkClean();
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void Open()
        {
            try
            {
                if (Save())
                {
                    var dialog = new OpenFileDialog()
                    {
                        Filter = DesignerConstants.FileFilter
                    };

                    if (dialog.ShowDialog() == true)
                    {
                        AddStateMachineProject(dialog.FileName);
                        Filename = dialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void AddStateMachineProject(string fileName)
        {
            StateMachineProject stateMachineProject = _persistor.LoadProject(fileName);
            Project = ApplicationContainer.Container.Resolve<ProjectViewModel>
                (
                    new TypedParameter(typeof(StateMachineProject), stateMachineProject)
                );
        }

        private void Save(SaveMessage saveMessage)
        {
            Save();
        }

        private bool Save()
        {
            try
            {
                if (Project == null || !_dirtyService.IsDirty)
                    return true;

                if (string.IsNullOrWhiteSpace(Filename))
                {
                    return SaveAs();
                }

                SaveAndGenerate();
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }

            return true;
        }

        private bool CanSave() => _dirtyService.IsDirty;

        private bool SaveAs()
        {
            try
            {
                var dialog = new SaveFileDialog()
                {
                    Filter = DesignerConstants.FileFilter
                };

                if (dialog.ShowDialog() == true)
                {
                    Filename = dialog.FileName;
                    SaveAndGenerate();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }

            return false;
        }

        private void SaveAndGenerate()
        {
            _persistor.SaveProject(Project.GetModel(), Filename);
            _dirtyService.MarkClean();
            Project.GenerateProjectCCodeAndDocumentation(ProjectDirectory);
        }

        public bool CanClose()
        {
            if (_dirtyService.IsDirty)
            {
                var result = MessageBox.Show("Save changes?", "Project has changed", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:

                        Save();

                        break;

                    case MessageBoxResult.Cancel:

                        return false;
                }

            }

            return true;
        }

        public void Closed()
        {
        }

        public event EventHandler<CloseEventArgs> Close;
    }
}