using System;
using EFSM.OffsetWriter;

namespace EFSM.Generator.Model
{

    internal class ProjectBinaryGenerationResult
    {
        public StateMachineBinaryGenerationResult[] StateMachines { get; }

        internal ProjectBinaryGenerationResult(StateMachineBinaryGenerationResult[] stateMachines)
        {
            StateMachines = stateMachines;
        }
    }

    internal class StateMachineBinaryGenerationResult
    {
        public StateMachineBinaryGenerationResult(StateMachineGenerationModel stateMachine, BinarySegment[] segments)
        {
            StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            Segments = segments ?? throw new ArgumentNullException(nameof(segments));
        }

        public StateMachineGenerationModel StateMachine { get; }

        public BinarySegment[] Segments { get; }

    }
}