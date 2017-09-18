using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineReferenceViewModel : ViewModelBase
    {
        private StateMachine _model;
        private IViewService _viewService;

        public StateMachineReferenceViewModel(StateMachine model, IViewService viewService)
        {
            _model = model;
            _viewService = viewService;
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; RaisePropertyChanged(); }
        }

        public StateMachine GetModel()
        {
            return _model.Clone();
        }

        public bool Edit(IIsDirtyService dirty)
        {
            StateMachineDialogWindowViewModel viewModel = ApplicationContainer.Container
                .Resolve<StateMachineDialogWindowViewModel>(
                    new TypedParameter(typeof(StateMachine), GetModel()),
                    new TypedParameter(typeof(IIsDirtyService), dirty)
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
