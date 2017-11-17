using EFSM.Domain;
using System;
using System.Collections.Generic;

namespace EFSM.Generator.Model
{
    internal class TransitionGenerationModel 
    {
        public TransitionGenerationModel(StateMachine stateMachineSource, Guid parentStateId, Guid targetStateId, string name, List<ActionReferenceGenerationModel> actionList, byte[] opcodes)
        {
            StateMachineSource = stateMachineSource;
            ParentStateId = parentStateId;
            TargetStateId = targetStateId;
            Name = name;
            Actions = actionList;
            Opcodes = opcodes;
        }
        
        public StateMachine StateMachineSource { get; }
        public Guid ParentStateId { get; }
        public Guid TargetStateId { get; }
        public UInt16 ParentStateIndex
        {
            get { return StateMachineSource.GetStateIndex(ParentStateId); }
        }
        
        public UInt16 TargetStateIndex
        {
            get { return StateMachineSource.GetStateIndex(TargetStateId); }
        }

        public string Name { get; }
        public List<ActionReferenceGenerationModel> Actions { get; set; }
        public byte[] Opcodes { get; set; }
    }
}