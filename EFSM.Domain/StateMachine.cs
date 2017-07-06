namespace EFSM.Domain
{
    public class StateMachine
    {
        public string Name { get; set; }

        public StateMachineInput[] Inputs { get; set; }

        /// <summary>
        /// The available actions
        /// </summary>
        public StateMachineOutputAction[] Actions { get; set; }
    }
}