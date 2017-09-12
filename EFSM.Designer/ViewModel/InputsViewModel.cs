using EFSM.Domain;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public class InputsViewModel : ViewModelBase
    {
        private StateMachineInput _model;

        public InputsViewModel(StateMachineInput model)
        {
            _model = model;
        }

        public string Name
        {
            get { return _model.Name; }
        }

        private bool _isOn = false;
        public bool IsOn
        {
            get { return _isOn; }
            set { _isOn = value; RaisePropertyChanged(); }
        }
    }
}
