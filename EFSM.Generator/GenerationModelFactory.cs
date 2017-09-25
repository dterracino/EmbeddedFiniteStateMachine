using System;
using System.Linq;
using EFSM.Domain;
using EFSM.Generator.Model;

namespace EFSM.Generator
{
    internal class GenerationModelFactory
    {
        internal ProjectGenerationModel GetGenerationModel(StateMachineProject project)
        {
            //Create the state machines
            var stateMachines = project.StateMachines
                .Select(GenerateStateMachine)
                .ToArray();

            return new ProjectGenerationModel(project, stateMachines);
        }

        private StateMachineGenerationModel GenerateStateMachine(StateMachine stateMachine, int stateMachineIndex)
        {
            //Ensure that the idle state is first
            var orderedStates = stateMachine.States
                .OrderByDescending(s => s.StateType)
                .ToArray();

            //Inputs
            var generatedInputs = stateMachine.Inputs
                .Select((m, index) => GenerateInput(m, index, stateMachine))
                .ToArray();

            //Outputs
            var generatedOutputs = stateMachine.Actions
                .Select((m, index) => GenerateOutput(m, index, stateMachine))
                .ToArray();

            //Generate the states
            var generatedStates = orderedStates
                .Select((m, index) => GenerateState(m, index, stateMachine, generatedOutputs))
                .ToArray();


            int transitionIndex = 0;

            //Hook up the transitions
            foreach (var transition in stateMachine.Transitions)
            {
                var sourceState = generatedStates
                    .FirstOrDefault(s => s.Model.Id == transition.SourceStateId);

                var targetState = generatedStates
                    .FirstOrDefault(s => s.Model.Id == transition.TargetStateId);

                if (sourceState == null)
                    throw new ApplicationException($"Unablet to find source state {transition.SourceStateId} in transition '{transition.Name}'.");

                if (targetState == null)
                    throw new ApplicationException($"Unablet to find target state {transition.TargetStateId} in transition '{transition.Name}'.");

                var actions = GetActions(generatedOutputs, transition.TransitionActions, $"Transition '{transition.Name}'");

                //TODO: Deal with the input conditions

                var generatedTransition = new TransitionGenerationModel(transition, transitionIndex, stateMachine, targetState, actions.ToArray());

                sourceState.Transitions.Add(generatedTransition);

                transitionIndex++;
            }

            return new StateMachineGenerationModel(
                stateMachine,
                generatedStates,
                generatedInputs,
                generatedOutputs,
                stateMachineIndex);
        }

        private StateGenerationModel GenerateState(State state, int index, StateMachine parent, OutputGenerationModel[] allOutputs)
        {
            var entryActions = GetActions(allOutputs, state.EntryActions, $"state {state.Name} entry actions");

            var exitActions = GetActions(allOutputs, state.ExitActions, $"state {state.Name} exit actions");

            return new StateGenerationModel(state, index, parent, entryActions, exitActions);
        }

        private InputGenerationModel GenerateInput(StateMachineInput input, int index, StateMachine parent)
        {
            return new InputGenerationModel(input, index, parent);
        }

        private OutputGenerationModel GenerateOutput(StateMachineOutputAction output, int index, StateMachine parent)
        {
            return new OutputGenerationModel(output, index, parent);
        }

        private ActionReferenceGenerationModel[] GetActions(OutputGenerationModel[] allOutputs, Guid[] actionIds, string objectName)
        {
            ActionReferenceGenerationModel[] actions = new ActionReferenceGenerationModel[actionIds.Length];

            int actionIndex = 0;

            foreach (var outputActionId in actionIds)
            {
                var outputAction = allOutputs.FirstOrDefault(o => o.Model.Id == outputActionId);

                if (outputAction == null)
                    throw new ApplicationException($"Unable to find action {outputActionId} in {objectName}.");

                actions[actionIndex] = new ActionReferenceGenerationModel(outputAction, actionIndex);

                actionIndex++;
            }

            return actions;
        }

    }
}