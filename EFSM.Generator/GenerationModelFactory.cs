using System;
using System.Linq;
using EFSM.Domain;
using EFSM.Generator.Model;
using System.Collections.Generic;

namespace EFSM.Generator
{
    internal class GenerationModelFactory
    {
        internal ProjectGenerationModel GetGenerationModel(StateMachineProject project)
        {
            //Create the state machines
            //var stateMachines = project.StateMachines.Select(GenerateStateMachine).ToArray();
            List<StateMachineGenerationModel> localStateMachineList = new List<StateMachineGenerationModel>();

            for(int i = 0; i < project.StateMachines.Length; i++)
            {
                localStateMachineList.Add(GenerateStateMachine(project.StateMachines[i], i));
            }            

            return new ProjectGenerationModel(project, localStateMachineList.ToArray());
        }

        private List <InputGenerationModel>GetListOfInputsForState(State state, StateMachine stateMachine)
        {
            var stateInputList = new List<StateMachineInput>();
            var generationModelInputList = new List<InputGenerationModel>();

            /*Find the relevant transitions in the state machine, investigate their respective inputs, and compile a list of inputs for the given state.*/
            foreach (var transition in stateMachine.Transitions)
            {
                if (transition.SourceStateId == state.Id)
                {
                    StateMachineConditionOps.AddInputsForTransition(transition, stateMachine, stateInputList);
                }
            }

            foreach (var stateInput in stateInputList)
            {
                generationModelInputList.Add(new InputGenerationModel(stateMachine, stateInput.Id, stateInput.Name));
            }

            return generationModelInputList;
        }

        private List<TransitionGenerationModel> GetListOfTransitionsForState(State state, StateMachine stateMachine)
        {
            var transitionList = new List<TransitionGenerationModel>();

            foreach (var transition in stateMachine.Transitions)
            {
                if (transition.SourceStateId == state.Id)
                    transitionList.Add(new TransitionGenerationModel(state.Id, transition.TargetStateId, transition.Name));
            }

            return transitionList;
        }

        private StateMachineGenerationModel GenerateStateMachine(StateMachine stateMachine, int stateMachineIndex)
        {
            StateMachineGenerationModel stateMachineGenerationModel = new StateMachineGenerationModel(stateMachine, stateMachineIndex);

            /*Cycle through the states in that state machine, and fill in the  */
            foreach (var state in stateMachine.States)
            {
                /*Create the model.*/
                var stateGenerationModel = new StateGenerationModel();

                /*Initialize the model.*/
                stateGenerationModel.inputList = GetListOfInputsForState(state, stateMachine);
                stateGenerationModel.transitionList = GetListOfTransitionsForState(state, stateMachine);
                

                /*Add the model.*/
                stateMachineGenerationModel.States.Add();
            }

            return stateMachineGenerationModel;
        }



        //private StateMachineGenerationModel GenerateStateMachine(StateMachine stateMachine, int stateMachineIndex)
        //{
        //    //Ensure that the idle state is first
        //    var orderedStates = stateMachine.States
        //        .OrderByDescending(s => s.StateType)
        //        .ToArray();

//    //Inputs
//    var generatedInputs = stateMachine.Inputs
//        .Select((m, index) => GenerateInput(m, index, stateMachine))
//        .ToArray();

//    //Outputs
//    var generatedOutputs = stateMachine.Actions
//        .Select((m, index) => GenerateOutput(m, index, stateMachine))
//        .ToArray();

//    //Generate the states
//    var generatedStates = orderedStates
//        .Select((m, index) => GenerateState(m, index, stateMachine, generatedOutputs))
//        .ToArray();


//    int transitionIndex = 0;

//    //Hook up the transitions
//    foreach (var transition in stateMachine.Transitions)
//    {
//        var sourceState = generatedStates
//            .FirstOrDefault(s => s.Model.Id == transition.SourceStateId);

//        var targetState = generatedStates
//            .FirstOrDefault(s => s.Model.Id == transition.TargetStateId);

//        if (sourceState == null)
//            throw new ApplicationException($"Unablet to find source state {transition.SourceStateId} in transition '{transition.Name}'.");

//        if (targetState == null)
//            throw new ApplicationException($"Unablet to find target state {transition.TargetStateId} in transition '{transition.Name}'.");

//        var actions = GetActions(generatedOutputs, transition.TransitionActions, $"Transition '{transition.Name}'");

//        //TODO: Deal with the input conditions

//        var generatedTransition = new TransitionGenerationModel(transition, transitionIndex, stateMachine, targetState, actions.ToArray());

//        sourceState.Transitions.Add(generatedTransition);

//        transitionIndex++;
//    }

//    return new StateMachineGenerationModel(
//        stateMachine,
//        generatedStates,
//        generatedInputs,
//        generatedOutputs,
//        stateMachineIndex);
//}

//        private StateGenerationModel GenerateState(State state, int index, StateMachine parent, OutputGenerationModel[] allOutputs)
//        {
//            var entryActions = GetActions(allOutputs, state.EntryActions, $"state {state.Name} entry actions");

//            var exitActions = GetActions(allOutputs, state.ExitActions, $"state {state.Name} exit actions");

//            var transitions = GetTransitions(state, index, parent);

//            return new StateGenerationModel(state, index, parent, entryActions, exitActions, transitions);
//        }

//        private List<TransitionGenerationModel> GetTransitions(State state, int index, StateMachine parent)
//        {
//            /*Find the transitions associated with a particular state, concatenate them, and return a reference to the list.*/

//            /*Go through each transition. If its source state is the same as the given state, add the transition.*/
//            foreach (var transition in parent.Transitions)
//            {

//            }


//            return new List<TransitionGenerationModel>();
//        }

//        private InputGenerationModel GenerateInput(StateMachineInput input, int index, StateMachine parent)
//        {
//            return new InputGenerationModel(input, index, parent);
//        }

//        private OutputGenerationModel GenerateOutput(StateMachineOutputAction output, int index, StateMachine parent)
//        {
//            return new OutputGenerationModel(output, index, parent);
//        }

//        private ActionReferenceGenerationModel[] GetActions(OutputGenerationModel[] allOutputs, Guid[] actionIds, string objectName)
//        {
//            ActionReferenceGenerationModel[] actions = new ActionReferenceGenerationModel[actionIds.Length];

//            int actionIndex = 0;

//            foreach (var outputActionId in actionIds)
//            {
//                var outputAction = allOutputs.FirstOrDefault(o => o.Model.Id == outputActionId);

//                if (outputAction == null)
//                    throw new ApplicationException($"Unable to find action {outputActionId} in {objectName}.");

//                actions[actionIndex] = new ActionReferenceGenerationModel(outputAction, actionIndex);

//                actionIndex++;
//            }

//            return actions;
//        }
//    }
//}