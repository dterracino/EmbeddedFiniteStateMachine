using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
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
        private readonly IMarkDirty _parentDirtyService;
        private IMessageBoxService _messageBoxService;

        public StateMachineReferenceViewModel(StateMachine model, IViewService viewService, IMarkDirty parentDirtyService, IMessageBoxService messageBoxService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _parentDirtyService = parentDirtyService ?? throw new ArgumentNullException(nameof(parentDirtyService));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            RenameCommand = new RelayCommand(Rename);
            EditCommand = new RelayCommand(() => Edit());

            CheckNumberOfInstancesValue();
        }


        public ICommand RenameCommand { get; }
        public ICommand EditCommand { get; }

        private void Rename()
        {
            try
            {
                var textEditService = new TextEditService();

                textEditService.EditText(Name, "New Name", "Rename", s => Name = s);
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        public string DisplayedName => $"{_model.Name} ({_model.NumberOfInstances})";

        public bool IsDisabled
        {
            get => _model.IsDisabled;
            set
            {
                _model.IsDisabled = value;
                RaisePropertyChanged();
                _parentDirtyService.MarkDirty();
            }
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
            try
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
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void ReloadModel(StateMachine stateMachine)
        {
            _model = stateMachine;
            // ReSharper disable once ExplicitCallerInfoArgument
            RaisePropertyChanged(null);
        }

        private void CheckNumberOfInstancesValue()
        {
            if (_model.NumberOfInstances < 1)
            {
                _model.NumberOfInstances = 1;
                RaisePropertyChanged(nameof(DisplayedName));
            }
        }
    }
}
