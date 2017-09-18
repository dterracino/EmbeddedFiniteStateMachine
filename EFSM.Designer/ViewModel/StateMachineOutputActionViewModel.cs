using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineOutputActionViewModel : ViewModelBase
    {
        private StateMachineOutputAction _model;
        private StateMachineViewModel _parent;

        public StateMachineOutputActionViewModel() : this(new StateMachineOutputAction() { Id = Guid.NewGuid() }, null)
        {
        }

        public StateMachineOutputActionViewModel(StateMachineOutputAction model, StateMachineViewModel parent)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _parent = parent;
        }

        public StateMachineOutputAction GetModel() => _model.Clone();

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; RaisePropertyChanged(); _parent?.SaveUndoState(); }
        }

        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; RaisePropertyChanged(); }
        }
    }
}
