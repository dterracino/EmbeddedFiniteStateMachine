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

    //internal class CodeFileGenerator
    //{
    //    internal string GenerateCode(ProjectGenerationModel project, ProjectBinaryGenerationResult binaryGenerationResult)
    //    {
    //        var code = new TextGenerator();

    //        code.AppendLine($"#include \"{project.HeaderFileName}\"");
    //        code.AppendLine($"#include <stdint.h>");
    //        code.AppendLine("#include \"efsm_core.h\"");
    //        code.AppendLine();

    //        foreach (var stateMachine in binaryGenerationResult.StateMachines)
    //        {
    //            /*Opening annotation for state machine.*/
    //            code.AppendLine($"/*State machine \"{stateMachine.StateMachine.Model.Name}\" source code.*/\n\n");

    //            /*Declare the input function reference array.*/
    //            code.AppendLine($"uint8_t {stateMachine.StateMachine.InputReferenceArrayString};");

    //            /*Declare the output action function reference array.*/
    //            code.AppendLine($"void {stateMachine.StateMachine.ActionReferenceArrayString};");

    //            code.AppendLine();

    //            /*Create the state machine binary array.*/
    //            int currentAddress = 0;

    //            code.AppendLine();

    //            code.AppendLine($"/* {stateMachine.StateMachine.Model.Name} */");
    //            code.AppendLine($"uint16_t {stateMachine.StateMachine.LocalBinaryVariableName}[] = {{");
    //            code.AppendLine();

    //            using (code.Indent())
    //            {
    //                foreach (var segment in stateMachine.Segments)
    //                {
    //                    code.AppendLine($"/*[{currentAddress}]: {segment.Source.GetComment()} */");

    //                    if (segment.Content.Length > 0)
    //                    {
    //                        foreach (var b in segment.Content)
    //                        {
    //                            //code.Append($"0x{Convert.ToString(b, 16).PadLeft(2, '0')}, ");

    //                            if (segment == stateMachine.Segments.Last())
    //                            {
    //                                code.Append($"{Convert.ToString(b, 10).PadLeft(0, '0')} ");
    //                            }
    //                            else
    //                            {
    //                                code.Append($"{Convert.ToString(b, 10).PadLeft(0, '0')}, ");
    //                            }                                
    //                        }

    //                        code.AppendLine();
    //                        code.AppendLine();
    //                    }

    //                    if (segment.Content != null)
    //                    {
    //                        currentAddress += segment.Content.Length;
    //                    }
    //                }
    //            }

    //            code.AppendLine("};");
    //            code.AppendLine();

    //            /*Declare the containing binary.*/
    //            code.AppendLine($"EFSM_BINARY {stateMachine.StateMachine.BinaryContainerName};");

    //            /*Create the initialization routine.*/
    //            code.AppendLine();

    //            code.AppendLine($"void EFSM_{stateMachine.StateMachine.IndexDefineName}_Init()\n{{\n");
    //            code.Indent();

    //            /*Set up the binary.*/
    //            code.AppendLine("/*Associate the raw binary with the binary container.*/");
    //            code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.id = {stateMachine.StateMachine.BinaryContainerId};");
    //            code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.data = {stateMachine.StateMachine.LocalBinaryVariableName};");
    //            code.AppendLine();

    //            /*Set up the input reference array.*/
    //            var numberOfInputs = stateMachine.StateMachine.Inputs.Length;

    //            for (int inputIndex = 0; inputIndex < numberOfInputs; inputIndex++)
    //            {
    //                code.AppendLine($"{stateMachine.StateMachine.InputReferenceArrayName}[{inputIndex}] = &{stateMachine.StateMachine.Inputs[inputIndex].FunctionName};");
    //            }

    //            code.AppendLine();

    //            /*Set up the action reference array.*/
    //            var numberOfActions = stateMachine.StateMachine.Actions.Length;

    //            for (int actionIndex = 0; actionIndex < numberOfActions; actionIndex++)
    //            {
    //                code.AppendLine($"{stateMachine.StateMachine.ActionReferenceArrayName}[{actionIndex}] = &{stateMachine.StateMachine.Actions[actionIndex].FunctionName};");
    //            }

    //            code.RemoveIndent();
    //            code.AppendLine("}\n");
    //        }

    //        code.AppendLine();
    //        return code.ToString();
    //    }    
    //}
    /****************************************************************************************************************************************************/
    internal class CodeFileGenerator
    {
        internal string GenerateCode(ProjectGenerationModel project, ProjectBinaryGenerationResult binaryGenerationResult)
        {
            var code = new TextGenerator();

            code.AppendLine($"#include \"{project.HeaderFileName}\"");
            code.AppendLine($"#include <stdint.h>");
            code.AppendLine("#include \"efsm_core.h\"");
            code.AppendLine();

            code.AppendLine("/**********Begin User Declaration of EFSM_INSTANCE instances.**********/");
            code.AppendLine();
            code.AppendLine("/**********End User Declaration of EFSM_INSTANCE instances.**********/");
            code.AppendLine();
            code.AppendLine("/**********Begin Generated EFSM_BINARY Binary Instance Declarations.**********/");
            code.AppendLine();

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                /*Declare the containing binary.*/
                code.AppendLine($"EFSM_BINARY {stateMachine.StateMachine.BinaryContainerName};");
            }

            code.AppendLine();
            code.AppendLine("/**********End Generated EFSM_BINARY Binary Instance Declarations.**********/");
            code.AppendLine();
            code.AppendLine("/**********Begin Generated Arrays for Function Pointers**********/");
            code.AppendLine();

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                code.AppendLine($"/*State Machine \"{stateMachine.StateMachine.IndexDefineName}\"*/");
                code.AppendLine();
                code.AppendLine("/*Array for pointers to input query functions.*/");
                /*Declare the input function reference array.*/
                code.AppendLine($"uint8_t {stateMachine.StateMachine.InputReferenceArrayString};");
                code.AppendLine();
                code.AppendLine("/*Array for pointers to action functions.*/");
                /*Declare the output action function reference array.*/
                code.AppendLine($"void {stateMachine.StateMachine.ActionReferenceArrayString};");
                code.AppendLine();
            }

            code.AppendLine();
            code.AppendLine("/**********End Generated Arrays for Function Pointers**********/");
            code.AppendLine();
            code.AppendLine("/**********Begin User Definition of Input Query and Action Functions.**********/");
            code.AppendLine();
            code.AppendLine("/**********End User Definition of Input Query and Action Functions.**********/");
            code.AppendLine();
            code.AppendLine("/**********Generated Initialization Function.**********/");

            code.AppendLine($"void EFSM_{"ProjectNameHere"}_Init()\n{{\n");
            code.Indent();

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                /*Set up the binary.*/
                code.AppendLine("/*Associate the binary array with the EFSM_BINARY structures.*/");
                code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.id = {stateMachine.StateMachine.BinaryContainerId};");
                code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.data = {stateMachine.StateMachine.LocalBinaryVariableName};");
                code.AppendLine();

                /*Set up the input reference array.*/
                var numberOfInputs = stateMachine.StateMachine.Inputs.Length;

                for (int inputIndex = 0; inputIndex < numberOfInputs; inputIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.InputReferenceArrayName}[{inputIndex}] = &{stateMachine.StateMachine.Inputs[inputIndex].FunctionName};");
                }

                code.AppendLine();

                /*Set up the action reference array.*/
                var numberOfActions = stateMachine.StateMachine.Actions.Length;

                for (int actionIndex = 0; actionIndex < numberOfActions; actionIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.ActionReferenceArrayName}[{actionIndex}] = &{stateMachine.StateMachine.Actions[actionIndex].FunctionName};");
                }
            }

            code.RemoveIndent();
            code.AppendLine("}\n");

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                /*Opening annotation for state machine.*/
                code.AppendLine($"/*State machine \"{stateMachine.StateMachine.Model.Name}\" source code.*/\n\n");

                /*Declare the input function reference array.*/
                code.AppendLine($"uint8_t {stateMachine.StateMachine.InputReferenceArrayString};");

                /*Declare the output action function reference array.*/
                code.AppendLine($"void {stateMachine.StateMachine.ActionReferenceArrayString};");

                code.AppendLine();

                /*Create the state machine binary array.*/
                int currentAddress = 0;

                code.AppendLine();

                code.AppendLine($"/* {stateMachine.StateMachine.Model.Name} */");
                code.AppendLine($"uint16_t {stateMachine.StateMachine.LocalBinaryVariableName}[] = {{");
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
                                //code.Append($"0x{Convert.ToString(b, 16).PadLeft(2, '0')}, ");

                                if (segment == stateMachine.Segments.Last())
                                {
                                    code.Append($"{Convert.ToString(b, 10).PadLeft(0, '0')} ");
                                }
                                else
                                {
                                    code.Append($"{Convert.ToString(b, 10).PadLeft(0, '0')}, ");
                                }
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
                code.AppendLine();

                /*Declare the containing binary.*/
                code.AppendLine($"EFSM_BINARY {stateMachine.StateMachine.BinaryContainerName};");

                /*Create the initialization routine.*/
                code.AppendLine();

                code.AppendLine($"void EFSM_{stateMachine.StateMachine.IndexDefineName}_Init()\n{{\n");
                code.Indent();

                /*Set up the binary.*/
                code.AppendLine("/*Associate the raw binary with the binary container.*/");
                code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.id = {stateMachine.StateMachine.BinaryContainerId};");
                code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.data = {stateMachine.StateMachine.LocalBinaryVariableName};");
                code.AppendLine();

                /*Set up the input reference array.*/
                var numberOfInputs = stateMachine.StateMachine.Inputs.Length;

                for (int inputIndex = 0; inputIndex < numberOfInputs; inputIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.InputReferenceArrayName}[{inputIndex}] = &{stateMachine.StateMachine.Inputs[inputIndex].FunctionName};");
                }

                code.AppendLine();

                /*Set up the action reference array.*/
                var numberOfActions = stateMachine.StateMachine.Actions.Length;

                for (int actionIndex = 0; actionIndex < numberOfActions; actionIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.ActionReferenceArrayName}[{actionIndex}] = &{stateMachine.StateMachine.Actions[actionIndex].FunctionName};");
                }

                code.RemoveIndent();
                code.AppendLine("}\n");
            }

            code.AppendLine();
            return code.ToString();
        }
    }
    /****************************************************************************************************************************************************/
}