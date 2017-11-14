using System;
using System.Collections.Generic;

namespace EFSM.Domain
{
    public class StateMachineCondition
    {
        /// <summary>
        /// If a simple condition, this is the source of the condition.
        /// </summary>
        public Guid? SourceInputId { get; set; }

        /// <summary>
        /// The child conditions that make up this group.
        /// </summary>
        public List<StateMachineCondition> Conditions { get; set; }

        /// <summary>
        /// If null, this is a simple condition (just SourceInputId and Value should be populated). Otherwise, only Conditions should be populated.
        /// </summary>
        public ConditionType ConditionType { get; set; }
    }
    
    public static class StateMachineConditionOps
    {
        /*Gets a list of references to inputs relevant to a given transition.*/
        public static void AddInputsForTransition(StateMachineTransition transition, StateMachine parentStateMachine, List <StateMachineInput> inputList)
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
            if (parentStateMachine.DoesInputExist(guid))
            {
                inputList.Add(parentStateMachine.GetInput(guid));
            }
        }
    }
}