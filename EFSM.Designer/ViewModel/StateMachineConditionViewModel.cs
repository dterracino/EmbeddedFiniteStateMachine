using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineConditionViewModel : ViewModelBase
    {
        private StateMachineCondition _model;

        public StateMachineConditionViewModel(StateMachineCondition model)
        {
            _model = model;
        }

        public StateMachineCondition GetModel()
        {
            return _model.Clone();
        }

        public Guid? SourceInputId
        {
            get { return _model.SourceInputId; }
            set { _model.SourceInputId = value; RaisePropertyChanged(); }
        }

        public bool? Value
        {
            get { return _model.Value; }
            set { _model.Value = value; RaisePropertyChanged(); }
        }

        public List<StateMachineCondition> Conditions
        {
            get { return _model.Conditions; }
            set { _model.Conditions = value; RaisePropertyChanged(); }
        }

        public CompoundConditionType? CompoundConditionType { get; set; }
    }
}
