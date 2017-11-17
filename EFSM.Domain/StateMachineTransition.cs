using System;

namespace EFSM.Domain
{
    public class StateMachineTransition
    {
        public Guid SourceStateId { get; set; }

        public Guid TargetStateId { get; set; }

        public string Name { get; set; }

        public Guid[] TransitionActions { get; set; }

        public StateMachineCondition Condition { get; set; }

        public double PullLength { get; set; }
    }
}