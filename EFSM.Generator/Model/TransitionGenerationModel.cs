using EFSM.Domain;
using System;

namespace EFSM.Generator.Model
{
    internal class TransitionGenerationModel 
    {
        public TransitionGenerationModel(Guid parentStateId, Guid targetStateId, string name)
        {
            ParentStateId = parentStateId;
            TargetStateId = targetStateId;
            Name = name;            
        }
        
        Guid ParentStateId { get; }
        Guid TargetStateId { get; }
        public string Name { get; }
        ActionReferenceGenerationModel Actions { get; set; }
        OpcodeGenerationModel Opcodes { get; set; }
    }
}