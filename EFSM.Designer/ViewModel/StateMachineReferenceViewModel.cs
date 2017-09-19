using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineReferenceViewModel : ViewModelBase
    {
        private StateMachine _model;
        private readonly IViewService _viewService;
        private readonly IDirtyService _parentDirtyService;

        public StateMachineReferenceViewModel(StateMachine model, IViewService viewService, IDirtyService parentDirtyService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _parentDirtyService = parentDirtyService ?? throw new ArgumentNullException(nameof(parentDirtyService));

            RenameCommand = new RelayCommand(Rename);
            EditCommand = new RelayCommand(() => Edit());
        }

        public ICommand RenameCommand { get; }
        public ICommand EditCommand { get; }

        private void Rename()
        {
            var textEditService = new TextEditService();

            textEditService.EditText(Name, "New Name", "Rename", s => Name = s);
        }

        public string Name
        {
            get { return _model.Name; }
            private set
            {
                _model.Name = value;
                RaisePropertyChanged();
                _parentDirtyService.MarkDirty();
            }
        }

        public StateMachine GetModel() => _model.Clone();

        public void Edit()
        {
            Action<StateMachine> reloadModel = ReloadModel;
            StateMachineDialogWindowViewModel viewModel = ApplicationContainer.Container
                .Resolve<StateMachineDialogWindowViewModel>(
                    new TypedParameter(typeof(StateMachine), GetModel()),
                    new TypedParameter(typeof(IDirtyService), _parentDirtyService),
                    new TypedParameter(typeof(Action<StateMachine>), reloadModel)
                );

            _viewService.ShowDialog(viewModel);
        }

        private void ReloadModel(StateMachine stateMachine)
        {
            _model = stateMachine;
            RaisePropertyChanged(nameof(Name));
        }
    }
}
