using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineInputViewModel : ViewModelBase
    {
        private StateMachineInput _model;

        public StateMachineInputViewModel(StateMachineInput model)
        {
            _model = model;
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; RaisePropertyChanged(); }
        }

        public StateMachineInput GetModel()
        {
            return _model.Clone();
        }

        private bool _isOn = false;
        public bool IsOn
        {
            get { return _isOn; }
            set { _isOn = value; RaisePropertyChanged(); }
        }

        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; RaisePropertyChanged(); }
        }
    }
}
