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

        public int TotalNumberOfInstances
        {
            get
            {
                int sum = 0;

                foreach (var efsmDef in StateMachines)
                {
                    sum += efsmDef.StateMachine.NumberOfInstances;
                }

                return sum;
            }
        }

        public string TotalNumberOfInstancesDefine { get { return "EFSM_TOTAL_NUMBER_OF_STATE_MACHINE_INSTANCES"; } }
    }

    internal class StateMachineBinaryGenerationResult
    {
        public StateMachineBinaryGenerationResult(StateMachineGenerationModel stateMachine, BinarySegment2[] segments)
        {
            StateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            Segments = segments ?? throw new ArgumentNullException(nameof(segments));
        }

        public StateMachineGenerationModel StateMachine { get; }

        public BinarySegment2[] Segments { get; }
    }
}