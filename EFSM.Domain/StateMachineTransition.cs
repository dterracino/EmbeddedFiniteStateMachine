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

        public double PullLength { get; set; }        
    }
}