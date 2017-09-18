using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Const;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SaveStateMachineCommand { get; private set; }
        public ICommand SaveAsStateMachineCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand AddStateMachineCommand { get; private set; }
        public ICommand OpenDialogCommand { get; private set; }
        public ICommand ClosingCommand { get; private set; }

        public IViewService _viewService;
        public IPersistor _persistor;

        private ProjectViewModel _stateMachineProjectViewModel = null;
        public ProjectViewModel StateMachineProjectViewModel
        {
            get { return _stateMachineProjectViewModel; }
            set { _stateMachineProjectViewModel = value; RaisePropertyChanged(); }
        }

        public string Title => "State Machine Designer";

        public MainViewModel(IViewService viewService, IPersistor persistor)
        {
            _viewService = viewService;
            _persistor = persistor;

            InitializeCommands();
            New();
        }

        private void InitializeCommands()
        {
            ClosingCommand = new RelayCommand<CancelEventArgs>(OnClosing);
            SaveAsStateMachineCommand = new RelayCommand(SaveAs);
            OpenCommand = new RelayCommand(OpenStateMachine);
            NewCommand = new RelayCommand(New);
            OpenDialogCommand = new RelayCommand<StateMachineReferenceViewModel>(OpenDialog);
            AddStateMachineCommand = new RelayCommand(AddStateMachine);
        }

        private void AddStateMachine()
        {
            StateMachineProjectViewModel.StateMachineViewModels.Add(new StateMachineReferenceViewModel(new StateMachine { Name = "New name" }, ApplicationContainer.Container.Resolve<IViewService>()));
        }

        private void OnClosing(CancelEventArgs e)
        {
            if (StateMachineProjectViewModel.DirtyService.IsDirty)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result = MessageBox.Show(msg, "Data App", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void OpenDialog(StateMachineReferenceViewModel stateMachineViewModel)
        {
            if (stateMachineViewModel.Edit(StateMachineProjectViewModel.DirtyService))
            {
                // Save
            }
        }

        private void New()
        {
            AddStateMachineProject(null);
        }

        private void OpenStateMachine()
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

        private void AddStateMachineProject(string fileName)
        {
            StateMachineProject stateMachineProject = _persistor.LoadProject(fileName);
            StateMachineProjectViewModel = ApplicationContainer.Container.Resolve<ProjectViewModel>(new TypedParameter(typeof(StateMachineProject), stateMachineProject));
            StateMachineProjectViewModel.Filename = fileName ?? "new.csdsn";
        }

        private void SaveAs()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = DesignerConstants.FileFilter
            };

            if (dialog.ShowDialog() == true)
            {
                StateMachineProjectViewModel.Save(_persistor, dialog.FileName);
                StateMachineProjectViewModel.DirtyService.MarkClean();
            }
        }
    }
}