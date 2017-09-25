using System;
using System.Collections.Generic;
using System.Linq;
using EFSM.Domain;
using EFSM.Generator.Model;
using EFSM.Generator.TextGeneration;

namespace EFSM.Generator
{
    public static class IEnumerableExtensions
    {
        // http://stackoverflow.com/a/3935352/232566
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source.Where((x, i) => i % chunkSize == 0).Select((x, i) => source.Skip(i * chunkSize).Take(chunkSize));
        }
    }

    internal class CodeFileGenerator
    {
        internal string GenerateCode(ProjectGenerationModel project, ProjectBinaryGenerationResult binaryGenerationResult)
        {
            var code = new TextGenerator();

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                int currentAddress = 0;

                code.AppendLine();

                code.AppendLine($"/* {stateMachine.StateMachine.Model.Name} */");
                code.AppendLine($"unsigned char {stateMachine.StateMachine.LocalBinaryVariableName}[] = {{");
                code.AppendLine();

                using (code.Indent())
                {
                    foreach (var segment in stateMachine.Segments)
                    {
                        code.AppendLine($"/*[{currentAddress}]: {segment.Source.GetComment()} */");

                        if (segment.Content.Length > 0)
                        {
                            foreach (var b in segment.Content)
                            {
                                code.Append($"0x{Convert.ToString(b, 16).PadLeft(2, '0')}, ");
                            }

                            code.AppendLine();
                            code.AppendLine();
                        }

                        if (segment.Content != null)
                        {
                            currentAddress += segment.Content.Length;
                        }
                    }
                }
                code.AppendLine("};");
            }

            code.AppendLine("/* All state machines  */");
            code.AppendLine("unsigned char * efsm_stateMachineDefinitions[] = {");

            using (code.Indent())
            {
                foreach (var stateMachine in binaryGenerationResult.StateMachines)
                {
                    code.AppendLine($"{stateMachine.StateMachine.LocalBinaryVariableName},");
                }
            }
            code.AppendLine("};");
            code.AppendLine();

            //foreach (var stateMachine in project.StateMachinesGenerationModel)
            //{
            //    code.AppendLine($"/* [{stateMachine.Index}]State Machine: {stateMachine.Model.Name} */");

            //    using (code.Indent())
            //    {
            //        code.AppendLine($"/* Inputs:  */");

            //        using (code.Indent())
            //        {
            //            foreach (var input in stateMachine.Inputs)
            //            {
            //                code.AppendLine($"/*[{input.Index}]{input.Model.Name} */");
            //            }
            //        }

            //        code.AppendLine($"/* Outputs: */");

            //        using (code.Indent())
            //        {
            //            foreach (var output in stateMachine.Outputs)
            //            {
            //                code.AppendLine($"/*[{output.Index}]{output.Model.Name} */");
            //            }
            //        }

            //        code.AppendLine($"/* States: */");

            //        using (code.Indent())
            //        {
            //            foreach (var state in stateMachine.States)
            //            {
            //                code.AppendLine($"/* [{state.Index}]{state.Model.Name} */");

            //                using (code.Indent())
            //                {
            //                    code.AppendLine("/* Transitions: */");

            //                    using (code.Indent())
            //                    {
            //                        foreach (var transition in state.Transitions)
            //                        {
            //                            code.AppendLine($"/* {transition.Model.Name} */");

            //                            using (code.Indent())
            //                            {
            //                                code.AppendLine("/* Actions: */");

            //                                using (code.Indent())
            //                                {
            //                                    foreach (var action in transition.Actions)
            //                                    {
            //                                        code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name}  */");
            //                                    }
            //                                }

            //                                code.AppendLine("/* Condition */");

            //                                if (transition.Model.Condition != null)
            //                                {
            //                                    using (code.Indent())
            //                                    {
            //                                        AppendCondition(transition.Model.Condition, code, stateMachine.Inputs);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }

            //                    code.AppendLine($"/* Entry Actions */");

            //                    using (code.Indent())
            //                    {
            //                        foreach (var action in state.EntryActions)
            //                        {
            //                            code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name} */");
            //                        }
            //                    }

            //                    code.AppendLine($"/* Exit Actions */");

            //                    using (code.Indent())
            //                    {
            //                        foreach (var action in state.ExitActions)
            //                        {
            //                            code.AppendLine($"/* [{action.Index}]{action.Model.Model.Name} */");
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            return code.ToString();
        }

        //private static void AppendCondition(StateMachineCondition condition, TextGenerator code, InputGenerationModel[] inputs)
        //{

        //    if (condition.CompoundConditionType == null && condition.Conditions == null &&
        //        condition.SourceInputId == null)
        //    {
        //        code.AppendLine("/* None */");

        //        return;
        //    }

        //    if (condition.CompoundConditionType == null)
        //    {
        //        if (condition.SourceInputId == null)
        //            throw new ApplicationException($"No source input was specified in a non-compound condition.");

        //        var input = inputs.FirstOrDefault(i => i.Model.Id == condition.SourceInputId.Value);

        //        if (input == null)
        //            throw new ApplicationException($"Unabe to find input {condition.SourceInputId}.");

        //        string prefix = (condition.Value == true) ? "" : "!";

        //        code.AppendLine($"/* {prefix}{input.Model.Name} */");
        //    }
        //    else
        //    {
        //        switch (condition.CompoundConditionType.Value)
        //        {
        //            case CompoundConditionType.And:

        //                code.AppendLine("/* And */");

        //                break;

        //            case CompoundConditionType.Or:
        //                code.AppendLine("/* Or */");
        //                break;

        //            default:
        //                throw new InvalidOperationException($"Unexpected enum value '{condition.CompoundConditionType.Value}'");
        //        }

        //        using (code.Indent())
        //        {
        //            if (condition.Conditions == null || condition.Conditions.Count == 0)
        //                throw new ApplicationException($"A condition was marked as compound, but had no child conditions.");

        //            foreach (var childCondition in condition.Conditions)
        //            {
        //                AppendCondition(childCondition, code, inputs);
        //            }
        //        }
        //    }
        //}
    }
}