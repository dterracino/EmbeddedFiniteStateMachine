using System;
using System.Windows.Input;
using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineReferenceViewModel : ViewModelBase
    {
        private StateMachine _model;
        private readonly IViewService _viewService;
        private readonly IIsDirtyService _parentDirtyService;

        public StateMachineReferenceViewModel(StateMachine model, IViewService viewService, IIsDirtyService parentDirtyService)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (viewService == null) throw new ArgumentNullException(nameof(viewService));
            if (parentDirtyService == null) throw new ArgumentNullException(nameof(parentDirtyService));

            _model = model;
            _viewService = viewService;
            _parentDirtyService = parentDirtyService;

            RenameCommand = new RelayCommand(Rename);
        }

        public ICommand RenameCommand { get; }

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

        public StateMachine GetModel()
        {
            return _model.Clone();
        }

        public bool Edit()
        {
            StateMachineDialogWindowViewModel viewModel = ApplicationContainer.Container
                .Resolve<StateMachineDialogWindowViewModel>(
                    new TypedParameter(typeof(StateMachine), GetModel()),
                    new TypedParameter(typeof(IIsDirtyService), _parentDirtyService)
                );

            if (_viewService.ShowDialog(viewModel) == true)
            {
                _model = viewModel.GetModel();
                return true;
            }

            return false;
        }
    }
}
