using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
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

        public void Edit()
        {
            StateMachineDialogWindowViewModel viewModel = ApplicationContainer.Container
                .Resolve<StateMachineDialogWindowViewModel>(
                new TypedParameter(typeof(StateMachine), GetModel()));
            _viewService.ShowDialog(viewModel);
        }
    }
}
