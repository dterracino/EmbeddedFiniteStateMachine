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

        public int NumberOfInstances { get; set; }

        public bool IsDisabled { get; set; }
    }
}