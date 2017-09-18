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
            _model = model ?? throw new ArgumentNullException(nameof(model));
            SetConditions(model);
        }

        private void SetConditions(StateMachineCondition model)
        {
            if (model.Conditions == null)
            {
                Conditions = new List<StateMachineConditionViewModel>();
            }
            else
            {
                foreach (var item in model.Conditions)
                {
                    Conditions.Add(new StateMachineConditionViewModel(item));
                }
            }
        }

        public StateMachineCondition GetModel() => _model.Clone();

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

        public List<StateMachineConditionViewModel> _conditions = new List<StateMachineConditionViewModel>();
        public List<StateMachineConditionViewModel> Conditions
        {
            get { return _conditions; }
            set { _conditions = value; RaisePropertyChanged(); }
        }

        public CompoundConditionType? CompoundConditionType { get; set; }
    }
}
