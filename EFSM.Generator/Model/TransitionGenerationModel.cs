using EFSM.Domain;
using System;
using System.Collections.Generic;

namespace EFSM.Generator.Model
{
    internal class TransitionGenerationModel 
    {
        public TransitionGenerationModel(Guid parentStateId, Guid targetStateId, string name, List<ActionReferenceGenerationModel> actionList)
        {
            ParentStateId = parentStateId;
            TargetStateId = targetStateId;
            Name = name;
            Actions = actionList;
        }
        
        public Guid ParentStateId { get; }
        public Guid TargetStateId { get; }
        public string Name { get; }
        public List<ActionReferenceGenerationModel> Actions { get; set; }
        public OpcodeGenerationModel Opcodes { get; set; }
    }
}