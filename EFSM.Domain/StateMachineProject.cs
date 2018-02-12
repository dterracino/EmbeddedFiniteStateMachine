namespace EFSM.Domain
{
    public enum DebugMode { Embedded = 0, Desktop = 1, None = 2 };
    public class StateMachineProject
    {
        public StateMachine[] StateMachines { get; set; }

        public GenerationOptions GenerationOptions { get; set; }        
    }
}

