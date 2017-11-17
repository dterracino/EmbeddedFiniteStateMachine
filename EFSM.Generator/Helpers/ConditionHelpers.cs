using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSM.Domain;

namespace EFSM.Generator
{
    public static class ConditionHelpers
    {
        /*Gets a list of references to inputs relevant to a given transition.*/
        public static void AddInputsForTransition(StateMachineTransition transition, StateMachine parentStateMachine, List<StateMachineInput> inputList)
        {
            List<StateMachineCondition> conditionList = new List<StateMachineCondition>();
            conditionList.Add(transition.Condition);
            AccumulateInputsFromConditionList(conditionList, inputList, parentStateMachine);
        }

        public static void AccumulateInputsFromConditionList(List<StateMachineCondition> conditionList, List<StateMachineInput> inputList, StateMachine parentStateMachine)
        {
            foreach (var condition in conditionList)
            {
                if (condition.ConditionType == ConditionType.Input)
                {
                    AddInputToList(parentStateMachine, inputList, (Guid)condition.SourceInputId);
                }
                else
                {
                    AccumulateInputsFromConditionList(condition.Conditions, inputList, parentStateMachine);
                }
            }
        }

        public static void AddInputToList(StateMachine parentStateMachine, List<StateMachineInput> inputList, Guid guid)
        {
            /*Use the guid to find the input in the parent state machine.*/
            //if (parentStateMachine.DoesInputExist(guid))
            if (parentStateMachine.DoesInputExist(guid))
            {
                inputList.Add(parentStateMachine.GetInput(guid));
            }
        }
    }
}
