using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public class InputViewModel : ViewModelBase
    {
        private StateMachineInput _model;

        public InputViewModel(StateMachineInput model)
        {
            _model = model;
        }

        public string Name
        {
            get { return _model.Name; }
        }

        private bool _value = false;
        public bool Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); }
        }
    }
}
