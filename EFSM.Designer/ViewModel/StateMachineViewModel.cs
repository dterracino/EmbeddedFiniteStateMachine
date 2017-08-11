using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{

    public class StateMachineViewModel : ViewModelBase
    {
        private StateMachine _model;

        public StateMachineViewModel(StateMachine model)
        {
            _model = model;
        }

        public string Name => _model.Name;

    }
}