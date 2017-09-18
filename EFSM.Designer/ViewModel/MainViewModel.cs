using System;
using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Const;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Cas.Common.WPF.Behaviors;

namespace EFSM.Designer.ViewModel
{
    public class MainViewModel : ViewModelBase, ICloseableViewModel
    {
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }

        public IViewService _viewService;
        public IPersistor _persistor;

        private ProjectViewModel _project;
        private string _filename;

        public MainViewModel(IViewService viewService, IPersistor persistor)
        {
            _viewService = viewService;
            _persistor = persistor;

            SaveCommand = new RelayCommand(() => Save(), CanSave);
            SaveAsCommand = new RelayCommand(() => SaveAs(), CanSave);
            OpenCommand = new RelayCommand(Open);
            NewCommand = new RelayCommand(New);
            EditCommand = new RelayCommand<StateMachineReferenceViewModel>(Edit);

            New();
        }

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
            }
        }

        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Filename))
                    return "Embedded State Machine Designer";

                return $"Embedded State Machine Designer - {Filename}";
            }
        }

        private void Edit(StateMachineReferenceViewModel stateMachineViewModel)
        {
            if (stateMachineViewModel.Edit(Project.DirtyService))
            {
                
            }
        }

        private void New()
        {
            if (Save())
            {
                Filename = null;

                Project = new ProjectViewModel(_persistor.Create());
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
            if (!Project.DirtyService.IsDirty)
                return true;

            if (string.IsNullOrWhiteSpace(Filename))
            {
                return SaveAs();
            }

            _persistor.SaveProject(Project.GetModel(), Filename);

            return false;
        }

        private bool CanSave()
        {
            return Project.DirtyService.IsDirty;
        }

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

                return true;
            }

            return false;
        }

        public bool CanClose()
        {
            if (Project.DirtyService.IsDirty)
            {
                string msg = "Data is dirty. Close without saving?";

                MessageBoxResult result = MessageBox.Show(msg, "Data App", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No)
                {
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