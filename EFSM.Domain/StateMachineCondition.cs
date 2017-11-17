using System;
using System.Collections.Generic;


namespace EFSM.Domain
{
    public class StateMachineCondition
    {
        /// <summary>
        /// If a simple condition, this is the source of the condition.
        /// </summary>
        public Guid? SourceInputId { get; set; }

        /// <summary>
        /// The child conditions that make up this group.
        /// </summary>
        public List<StateMachineCondition> Conditions { get; set; }

        /// <summary>
        /// If null, this is a simple condition (just SourceInputId and Value should be populated). Otherwise, only Conditions should be populated.
        /// </summary>
        public ConditionType ConditionType { get; set; }
    }    
}