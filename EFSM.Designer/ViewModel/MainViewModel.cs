using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Const;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand SaveStateMachineCommand { get; private set; }
        public ICommand SaveAsStateMachineCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand OpenDialogCommand { get; private set; }

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

            SaveAsStateMachineCommand = new RelayCommand(SaveAs);
            OpenCommand = new RelayCommand(OpenStateMachine);
            NewCommand = new RelayCommand(New);
            OpenDialogCommand = new RelayCommand<StateMachineReferenceViewModel>(OpenDialog);
            New();
        }

        private void OpenDialog(StateMachineReferenceViewModel stateMachineViewModel)
        {
            if (stateMachineViewModel.Edit())
            {
                // Save
            }
                ;
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
            }
        }
    }
}