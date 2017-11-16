using System;
using System.Collections.Generic;

namespace EFSM.Domain
{
    public class EmbeddedInputModel
    {
        public EmbeddedInputModel(Guid? id)
        {
            Id = id;
        }

        public Guid? Id { get; }
    }

    public class StateMachineTransition
    {
        public Guid SourceStateId { get; set; }

        public Guid TargetStateId { get; set; }

        public string Name { get; set; }

        public Guid[] TransitionActions { get; set; }

        public StateMachineCondition Condition { get; set; }

       public static bool IsInputInList(Guid? id, List<EmbeddedInputModel> inputList)
        {
            foreach (var input in inputList)
            {
                if (id == input.Id)
                    return true;
            }

            return false;
        }

        public List<EmbeddedInputModel> GetTransitionInputs()
        {
            bool moreToEvaluate = true;
            List<EmbeddedInputModel> _inputsForTransition = new List<EmbeddedInputModel>();

            while (moreToEvaluate)
            {
                /*Check the conditions of the transition and assemble a list of the relevant inputs.*/
                if (Condition.ConditionType == ConditionType.Input)
                {               
                    _inputsForTransition.Add(new EmbeddedInputModel(Condition.SourceInputId));
                    moreToEvaluate = false;
                }
                else
                {
                    foreach (var condition in Condition.Conditions)
                    {
                        if (condition.ConditionType == ConditionType.Input)
                        {
                            if (!IsInputInList(condition.SourceInputId, _inputsForTransition))
                            {
                                _inputsForTransition.Add(new EmbeddedInputModel(Condition.SourceInputId));
                            }
                        }
                    }

                    moreToEvaluate = false;
                }
            }

            return _inputsForTransition;
        }

        public List<StateMachineOutputAction> GetAllAssociatedActions(StateMachine parent)
        {
            var associatedActions = new List<StateMachineOutputAction>();

            /*Get a list of actions associated with the transition.*/
            /*First, get the exit actions for this transitions source state.*/
            var sourceState = parent.GetState(SourceStateId);

            foreach (var actionItem in sourceState.ExitActions)
            {
                
            }


            /*Second, get the actions for this transition itself.*/
            /*Finally, get the entry actions for this transitions' target state.*/


            return associatedActions;
        }
    }
}