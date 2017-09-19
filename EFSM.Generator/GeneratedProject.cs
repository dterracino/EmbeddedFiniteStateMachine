using EFSM.Domain;

namespace EFSM.Generator
{
    internal class GeneratedProject
    {
        public GeneratedProject(StateMachineProject model, GeneratedStateMachine[] stateMachines)
        {
            Model = model;
            StateMachines = stateMachines;
        }

        public StateMachineProject Model { get; }

        public GeneratedStateMachine[] StateMachines { get; }
    }

    internal class GeneratedStateMachine
    {
        public GeneratedStateMachine(StateMachine model, GeneratedState[] states)
        {
            Model = model;
            States = states;
        }

        /// <summary>
        /// The underlying model
        /// </summary>
        public StateMachine Model { get; }

        /// <summary>
        /// The states
        /// </summary>
        public GeneratedState[] States { get; }
    }

    internal class GeneratedState
    {
        public GeneratedState(State model, int stateIndex)
        {
            Model = model;
            StateIndex = stateIndex;
        }

        /// <summary>
        /// The index of this model within its model machine
        /// </summary>
        public int StateIndex { get; }

        /// <summary>
        /// Gets the underlying model
        /// </summary>
        public State Model { get; }
    }

    internal class GeneratedInput
    {
        
    }

    internal class GeneratedOutput
    {
        
    }

    internal class GeneratedTransition
    {
        
    }


}