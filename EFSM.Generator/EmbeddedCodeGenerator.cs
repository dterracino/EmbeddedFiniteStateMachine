using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EFSM.Domain;
using EFSM.Generator.Model;

namespace EFSM.Generator
{
    public class EmbeddedCodeGenerator
    {
        public void Generate(StateMachineProject project)
        {
            var generatedProject = GetGeneratedModel(project);

            string header = GenerateHeader(generatedProject);
            string code = GenerateCode(generatedProject);

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.HeaderFilePath))
            {
                File.WriteAllText(project.GenerationOptions.HeaderFilePath, header);
            }

            if (!string.IsNullOrWhiteSpace(project.GenerationOptions.CodeFilePath))
            {
                File.WriteAllText(project.GenerationOptions.CodeFilePath, code);
            }
        }

        private GeneratedProject GetGeneratedModel(StateMachineProject project)
        {
            //Create the state machines
            var stateMachines = project.StateMachines
                .Select(GenerateStateMachine)
                .ToArray();

            return new GeneratedProject(project, stateMachines);
        }

        private GeneratedStateMachine GenerateStateMachine(StateMachine stateMachine, int stateMachineIndex)
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

                var generatedTransition = new GeneratedTransition(transition, transitionIndex, stateMachine, targetState, actions.ToArray());

                sourceState.Transitions.Add(generatedTransition);

                transitionIndex++;
            }

            return new GeneratedStateMachine(
                stateMachine, 
                generatedStates,
                generatedInputs, 
                generatedOutputs, 
                stateMachineIndex);
        }

        private GeneratedActionReference[] GetActions(GeneratedOutput[] allOutputs, Guid[] actionIds, string objectName)
        {
            GeneratedActionReference[] actions = new GeneratedActionReference[actionIds.Length];

            int actionIndex = 0;

            foreach (var outputActionId in actionIds)
            {
                var outputAction = allOutputs.FirstOrDefault(o => o.Model.Id == outputActionId);

                if (outputAction == null)
                    throw new ApplicationException($"Unable to find action {outputActionId} in {objectName}.");

                actions[actionIndex] = new GeneratedActionReference(outputAction, actionIndex);

                actionIndex++;
            }

            return actions;
        }

        private GeneratedState GenerateState(State state, int index, StateMachine parent, GeneratedOutput[] allOutputs)
        {
            var entryActions = GetActions(allOutputs, state.EntryActions, $"state {state.Name} entry actions");

            var exitActions = GetActions(allOutputs, state.ExitActions, $"state {state.Name} exit actions");

            return new GeneratedState(state, index, parent, entryActions, exitActions);
        }

        private GeneratedInput GenerateInput (StateMachineInput input, int index, StateMachine parent)
        {
            return new GeneratedInput(input, index, parent);
        }

        private GeneratedOutput GenerateOutput(StateMachineOutputAction output, int index, StateMachine parent)
        {
            return new GeneratedOutput(output, index, parent);
        }

        private string GenerateHeader(GeneratedProject project)
        {
            StringBuilder headerFile = new StringBuilder();

            headerFile.Append(project.Model.GenerationOptions.HeaderFileHeader);

            headerFile.AppendLine($"/* Number of state machines */");
            headerFile.AppendLine($"#define EFSM_NUM_STATE_MACHINES {project.StateMachines.Length}");
            headerFile.AppendLine();

            headerFile.AppendLine("/* Model Machines */");

            foreach(var stateMachine in project.StateMachines)
            {
                headerFile.AppendLine($"/* Model Machine: {stateMachine.Model.Name} */");
                headerFile.AppendLine(stateMachine.IndexDefine);
                headerFile.AppendLine(stateMachine.NumStatesDefine);
                headerFile.AppendLine(stateMachine.NumInputsDefine);
                headerFile.AppendLine(stateMachine.NumOutputsDefine);
                headerFile.AppendLine();

                // ----------- Input Functions --------------------
                headerFile.AppendLine("/* Input functions */");
                foreach (var input in stateMachine.Inputs)
                {
                    headerFile.AppendLine(input.FunctionPrototype);
                }

                headerFile.AppendLine();

                foreach (var input in stateMachine.Inputs)
                {
                    headerFile.AppendLine(input.IndexDefine);
                }

                headerFile.AppendLine();

                // ----------- Output Functions --------------------
                headerFile.AppendLine("/* Output functions */");
                foreach (var output in stateMachine.Outputs)
                {
                    headerFile.AppendLine(output.FunctionPrototype);
                }

                headerFile.AppendLine();

                foreach (var output in stateMachine.Outputs)
                {
                    headerFile.AppendLine(output.IndexDefine);
                }

                headerFile.AppendLine();

                //---- States ---------------
                headerFile.AppendLine($"/* States */");
                foreach(var state in stateMachine.States)
                {
                    headerFile.AppendLine($"#define EFSM_{stateMachine.Model.Name.FixDefineName()}_{state.Model.Name.FixDefineName()} {state.Index}");
                }

                headerFile.AppendLine();
            }

            headerFile.Append(project.Model.GenerationOptions.HeaderFileFooter);

            return headerFile.ToString();
        }

        private string GenerateCode(GeneratedProject project)
        {
            var code = new TextGenerator();

            foreach (var stateMachine in project.StateMachines)
            {
                code.AppendLine($"/* [{stateMachine.Index}]State Machine: {stateMachine.Model.Name} */");

                using (new Indenter(code))
                {
                    code.AppendLine($"/* Inputs:  */");

                    using (new Indenter(code))
                    {
                        foreach (var input in stateMachine.Inputs)
                        {
                            code.AppendLine($"/*[{input.Index}]{input.Model.Name} */");
                        }
                    }

                    code.AppendLine($"/* Outputs: */");

                    using (new Indenter(code))
                    {
                        foreach (var output in stateMachine.Outputs)
                        {
                            code.AppendLine($"/*[{output.Index}]{output.Model.Name} */");
                        }
                    }

                    code.AppendLine($"/* States: */");

                    using (new Indenter(code))
                    {
                        foreach (var state in stateMachine.States)
                        {
                            code.AppendLine($"/* [{state.Index}]{state.Model.Name} */");

                            using (new Indenter(code))
                            {
                                code.AppendLine("/* Transitions: */");

                                using (new Indenter(code))
                                {
                                    foreach (var transition in state.Transitions)
                                    {
                                        code.AppendLine($"/* {transition.Model.Name} */");

                                        using (new Indenter(code))
                                        {
                                            code.AppendLine("/* Actions: */");

                                            using (new Indenter(code))
                                            {
                                                foreach (var action in transition.Actions)
                                                {
                                                    code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name}  */");
                                                }
                                            }

                                            code.AppendLine("/* Condition */");

                                            if (transition.Model.Condition != null)
                                            {
                                                using (new Indenter(code))
                                                {
                                                    AppendCondition(transition.Model.Condition, code, stateMachine.Inputs);
                                                }
                                            }
                                        }
                                    }
                                }

                                code.AppendLine($"/* Entry Actions */");

                                using (new Indenter(code))
                                {
                                    foreach (var action in state.EntryActions)
                                    {
                                        code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name} */");
                                    }
                                }

                                code.AppendLine($"/* Exit Actions */");

                                using (new Indenter(code))
                                {
                                    foreach (var action in state.ExitActions)
                                    {
                                        code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name} */");
                                    }
                                }
                            }
                        }
                    }   
                }
            }

            return code.ToString();
        }

        private static void AppendCondition(StateMachineCondition condition, TextGenerator code, GeneratedInput[] inputs)
        {

            if (condition.CompoundConditionType == null && condition.Conditions == null &&
                condition.SourceInputId == null)
            {
                code.AppendLine("/* None */");

                return;
            }

            if (condition.CompoundConditionType == null)
            {
                if (condition.SourceInputId == null)
                    throw new ApplicationException($"No source input was specified in a non-compound condition.");

                var input = inputs.FirstOrDefault(i => i.Model.Id == condition.SourceInputId.Value);

                if (input == null)
                    throw new ApplicationException($"Unabe to find input {condition.SourceInputId}.");

                string prefix = (condition.Value == true) ? "" : "!";

                code.AppendLine($"/* {prefix}{input.Model.Name} */");
            }
            else
            {
                switch (condition.CompoundConditionType.Value)
                {
                    case CompoundConditionType.And:

                        code.AppendLine("/* And */");

                        break;

                    case CompoundConditionType.Or:
                        code.AppendLine("/* Or */");
                        break;

                    default:
                        throw new InvalidOperationException($"Unexpected enum value '{condition.CompoundConditionType.Value}'");
                }

                using (new Indenter(code))
                {
                    if (condition.Conditions == null || condition.Conditions.Count == 0)
                        throw new ApplicationException($"A condition was marked as compound, but had no child conditions.");

                    foreach (var childCondition in condition.Conditions)
                    {
                        AppendCondition(childCondition, code, inputs);
                    }
                }
            }
        }
    }
}