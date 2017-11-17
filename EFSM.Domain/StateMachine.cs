using System;

namespace EFSM.Domain
{
    public class StateMachine
    {
        public string Name { get; set; }

        public State[] States { get; set; }

        public StateMachineInput[] Inputs { get; set; }

        public StateMachineTransition[] Transitions { get; set; }

        /// <summary>
        /// The available actions
        /// </summary>
        public StateMachineOutputAction[] Actions { get; set; }

        public bool DoesInputExist(Guid id)
        {
            foreach (var input in Inputs)
            {
                if (id == input.Id)
                    return true;
            }

            return false;
        }

        public StateMachineInput GetInput(Guid id)
        {         
            foreach (var input in Inputs)
            {
                if (id == input.Id)
                    return input;
            }           

            return null;
        }

        public int? GetInputIndex(Guid id)
        {
            for (int i = 0; i < Inputs.Length; i++)
            {
                if (Inputs[i].Id == id)
                    return i;
            }

            return null;
        }

        public State GetState(Guid id)
        {
            foreach (var state in States)
            {
                if (state.Id == id)
                    return state;
            }

            return null;
        }

        public int? GetActionReferenceIndex(Guid actionId)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                if (Actions[i].Id == actionId)
                    return i;
            }

            return null;
        }

        public string GetActionName(Guid actionId)
        {
            foreach (var a in Actions)
            {
                if (a.Id == actionId)
                    return a.Name;
            }

            return null;
        }
    }
}