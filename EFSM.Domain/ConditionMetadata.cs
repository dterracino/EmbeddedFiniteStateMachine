using System;
using System.Collections.Generic;

namespace EFSM.Domain
{
    public class ConditionMetadata
    {
        /// <summary>
        /// If a simple condition, this is the source of the condition.
        /// </summary>
        public Guid? SourceInputId { get; set; }

        /// <summary>
        /// If a simple condition, this is the value
        /// </summary>
        public bool? Value { get; set; }

        /// <summary>
        /// The child conditions that make up this group.
        /// </summary>
        public List<ConditionMetadata> Conditions { get; set; }

        /// <summary>
        /// If null, this is a simple condition (just SourceInputId and Value should be populated). Otherwise, only Conditions should be populated.
        /// </summary>
        public CompoundConditionType? CompoundConditionType { get; set; }

    }
}