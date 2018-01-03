namespace EFSM.Domain
{
    public class StateMachine
    {
        private bool _isEnabled = true;
        public string Name { get; set; }

        public State[] States { get; set; }

        public StateMachineInput[] Inputs { get; set; }

        public StateMachineTransition[] Transitions { get; set; }

        /// <summary>
        /// The available actions
        /// </summary>
        public StateMachineOutputAction[] Actions { get; set; }

        public int NumberOfInstances { get; set; }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
    }
}