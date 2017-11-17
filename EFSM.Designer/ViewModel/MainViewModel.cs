using Autofac;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Const;
using EFSM.Designer.Interfaces;
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

        private readonly IDirtyService _dirtyService;

        public MainViewModel(IViewService viewService, IPersistor persistor, IDirtyService dirtyService)
        {
            _viewService = viewService;
            _persistor = persistor;
            _dirtyService = dirtyService;

            SaveCommand = new RelayCommand(() => Save(), CanSave);
            SaveAsCommand = new RelayCommand(() => SaveAs());
            OpenCommand = new RelayCommand(Open);
            NewCommand = new RelayCommand(New);
            EditCommand = new RelayCommand<StateMachineReferenceViewModel>(Edit);

            _dirtyService.PropertyChanged += DirtyService_PropertyChanged;

            New();
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
                MessageBox.Show(e.Message);
            }
        }

        private void New()
        {
            if (Save())
            {
                Filename = null;

                Project = new ProjectViewModel(_persistor.Create(), _dirtyService);

                _dirtyService.MarkClean();
            }
        }

        private void Open()
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

        private void AddStateMachineProject(string fileName)
        {
            StateMachineProject stateMachineProject = _persistor.LoadProject(fileName);
            Project = ApplicationContainer.Container.Resolve<ProjectViewModel>(new TypedParameter(typeof(StateMachineProject), stateMachineProject));
        }

        private bool Save()
        {
            if (Project == null || !_dirtyService.IsDirty)
                return true;

            if (string.IsNullOrWhiteSpace(Filename))
            {
                return SaveAs();
            }

            _persistor.SaveProject(Project.GetModel(), Filename);
            _dirtyService.MarkClean();

            Project.GenerateProjectCCodeAndDocumentation(ProjectDirectory);

            return true;
        }

        private bool CanSave() => _dirtyService.IsDirty;

        private bool SaveAs()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = DesignerConstants.FileFilter
            };

            if (dialog.ShowDialog() == true)
            {
                Filename = dialog.FileName;
                _persistor.SaveProject(Project.GetModel(), dialog.FileName);
                _dirtyService.MarkClean();

                Project.GenerateProjectCCodeAndDocumentation(ProjectDirectory);

                return true;
            }

            return false;
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

#pragma warning disable CS0067
        public event EventHandler<CloseEventArgs> Close;
    }
}