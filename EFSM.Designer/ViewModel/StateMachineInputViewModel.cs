using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineInputViewModel : ViewModelBase
    {
        private StateMachineInput _model;
        private StateMachineViewModel _parent;

        public StateMachineInputViewModel(StateMachineInput model, StateMachineViewModel parent)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; RaisePropertyChanged(); _parent.SaveUndoState(); }
        }

        public StateMachineInput GetModel() => _model.Clone();

        private bool _value = false;
        public bool Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); _parent.SaveUndoState(); }
        }

        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; RaisePropertyChanged(); }
        }
    }
}
