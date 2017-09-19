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

            //Generate the states
            var generatedStates = orderedStates
                .Select((m, index) => GenerateState(m, index, stateMachine))
                .ToArray();

            //Inputs
            var generatedInputs = stateMachine.Inputs
                .Select((m, index) => GenerateInput(m, index, stateMachine))
                .ToArray();

            //Outputs
            var generatedOutputs = stateMachine.Actions
                .Select((m, index) => GenerateOutput(m, index, stateMachine))
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

                var outputActions = new List<GeneratedActionReference>(transition.TransitionActions.Length);

                int actionIndex = 0;

                foreach (var outputActionId in transition.TransitionActions)
                {
                    var outputAction = generatedOutputs.FirstOrDefault(o => o.Model.Id == outputActionId);

                    if (outputAction == null)
                        throw new ApplicationException($"Unable to find output action {outputActionId} in transition '{transition.Name}'.");

                    outputActions.Add(new GeneratedActionReference(outputAction, actionIndex));

                    actionIndex++;
                }

                //TODO: Deal with the input conditions

                var generatedTransition = new GeneratedTransition(transition, transitionIndex, stateMachine, targetState, outputActions.ToArray());

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

        private GeneratedState GenerateState(State state, int index, StateMachine parent)
        {
            return new GeneratedState(state, index, parent);
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
                                        }
                                    }
                                }

                                code.AppendLine($"/* Entry Actions */");

                                using (new Indenter(code))
                                {
                                    foreach (var action in state.Model.EntryActions)
                                    {
                                        code.AppendLine($"/* {action} */");
                                    }
                                }

                                code.AppendLine($"/* Exit Actions */");

                                using (new Indenter(code))
                                {
                                    foreach (var action in state.Model.ExitActions)
                                    {
                                        code.AppendLine($"/* {action} */");
                                    }
                                }
                            }
                        }
                    }   
                }
            }

            return code.ToString();
        }
    }
}