using EFSM.Designer.ViewModel;
using EFSM.Designer.ViewModel.TransitionEditor;
using EFSM.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFSM.Designer.Common
{
    public class MarkdownConditionGenerator
    {
        private List<Guid> _conditionGuids;

        public StringBuilder Generate(StateMachineTransition transition, StateMachineInput[] inputs)
        {
            _conditionGuids = new List<Guid>();
            StringBuilder s = new StringBuilder();

            if (transition.Condition != null)
            {
                s.Append(AddCondition(transition.Condition, inputs));
            }

            return s;
        }
              

        private StringBuilder AddCondition(StateMachineCondition condition, StateMachineInput[] inputs)
        {
            StringBuilder s = new StringBuilder();

            if ((condition.ConditionType == ConditionType.Input || condition.ConditionType == ConditionType.Not)
                && inputs != null)
            {
                Guid conditionGuid = condition.ConditionType == ConditionType.Input ? condition.SourceInputId.Value : condition.Conditions[0].SourceInputId.Value;
                StateMachineInput input = inputs.FirstOrDefault(i => i.Id == conditionGuid);

                if (input != null)
                {
                    if (!_conditionGuids.Contains(conditionGuid))
                    {
                        _conditionGuids.Add(conditionGuid);
                    }

                    string equalityOperator = condition.ConditionType == ConditionType.Input ? string.Empty : "!";
                    s.Append($" {equalityOperator}{input.Name}");
                }
            }
            else if (condition.ConditionType == ConditionType.And || condition.ConditionType == ConditionType.Or)
            {
                s.Append(" ( ");
                string operation = condition.ConditionType == ConditionType.And ? " AND " : " OR ";

                for (int i = 0; i < condition.Conditions.Count; i++)
                {
                    var item = condition.Conditions[i];

                    if (i > 0)
                    {
                        s.Append(operation);
                    }

                    s.Append(AddCondition(item, inputs));
                }

                s.Append(" ) ");
            }

            return s;
        }
    }
}
