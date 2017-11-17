using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFSM.Domain;

namespace EFSM.Generator
{
    public static class StateMachineTransitionsExtensions
    {
        public static List<EmbeddedInputModel> GetTransitionInputs(this StateMachineTransition transition)
        {
            bool moreToEvaluate = true;
            List<EmbeddedInputModel> _inputsForTransition = new List<EmbeddedInputModel>();

            while (moreToEvaluate)
            {
                /*Check the conditions of the transition and assemble a list of the relevant inputs.*/
                if (transition.Condition.ConditionType == ConditionType.Input)
                {
                    _inputsForTransition.Add(new EmbeddedInputModel(transition.Condition.SourceInputId));
                    moreToEvaluate = false;
                }
                else
                {
                    foreach (var condition in transition.Condition.Conditions)
                    {
                        if (condition.ConditionType == ConditionType.Input)
                        {
                            if (!transition.IsInputInList(condition.SourceInputId, _inputsForTransition))
                            {
                                _inputsForTransition.Add(new EmbeddedInputModel(transition.Condition.SourceInputId));
                            }
                        }
                    }

                    moreToEvaluate = false;
                }
            }

            return _inputsForTransition;
        }

        public static bool IsInputInList(this StateMachineTransition transition, Guid? id, List<EmbeddedInputModel> inputList)
        {
            foreach (var input in inputList)
            {
                if (id == input.Id)
                    return true;
            }

            return false;
        }

        public static List<StateMachineOutputAction> GetAllAssociatedActions(this StateMachineTransition transition, StateMachine parent)
        {
            var associatedActions = new List<StateMachineOutputAction>();

            /*Get a list of actions associated with the transition.*/
            /*First, get the exit actions for this transitions source state.*/
            var sourceState = parent.GetState(transition.SourceStateId);

            foreach (var actionItem in sourceState.ExitActions)
            {

            }


            /*Second, get the actions for this transition itself.*/
            /*Finally, get the entry actions for this transitions' target state.*/


            return associatedActions;
        }
    }
}
