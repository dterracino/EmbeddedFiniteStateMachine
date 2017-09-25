using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineConditionViewModel : ViewModelBase
    {
        private readonly StateMachineCondition _model;
        private List<StateMachineConditionViewModel> _conditions = new List<StateMachineConditionViewModel>();
        private ConditionType _conditionType;

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
            set
            {
                _model.SourceInputId = value;
                RaisePropertyChanged();
            }
        }
      
        public List<StateMachineConditionViewModel> Conditions
        {
            get { return _conditions; }
            private set
            {
                _conditions = value;
                RaisePropertyChanged();
            }
        }

        public ConditionType ConditionType
        {
            get { return _conditionType; }
            set
            {
                _conditionType = value; 
                RaisePropertyChanged();
            }
        }
    }
}
