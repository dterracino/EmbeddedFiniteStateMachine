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
    }
}