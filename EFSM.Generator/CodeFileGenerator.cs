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
            UInt16 tempCount;

            code.AppendLine($"#include \"{project.HeaderFileName}\"");
            code.AppendLine($"#include <stdint.h>");
            code.AppendLine("#include \"efsm_core.h\"");
            code.AppendLine();

            var plural = false;

            if (binaryGenerationResult.StateMachines.Count() > 1)
                plural = true;
          
            var notesString = "Notes\n\n" +

                              "The entire contents of this file are generated. The user must implement (in a separate file)\n" +
                              "the following:\n\n" +

                              "-The instances of the state machines themselves (variables of type EFSM_INSTANCE).\n" +
                              "-The definitions of the input query and action functions which are prototyped (for each EFSM\n" +
                              $" binary) in the {project.HeaderFileName} file.\n\n" +

                              "Typically, the user should not modify files which are generated. Reasons for this are:\n\n" +
                              "-To avoid introducing errors.\n" +
                              "-Additions are lost every time a file is generated (this can be counterproductive).\n\n" +

                              "An important distinction to make is between a state machine definition, and an actual state machine.\n" +
                              "In the EFSM environment, a state machine definition and a state machine are defined as follows:\n\n" +

                              "   State Machine Definition\n"+
                              "      -A set of binary instructions (an array of 16 bit integers).\n" +
                              "      -An array of pointers to the functions required for evaluating inputs.\n" +
                              "      -An array of pointers to the functions required for performing actions.\n" +
                              "      -Serves as a template for behavior.\n\n" +

                              "   State Machine\n"+
                              "      -A variable of type EFSM_INSTANCE which has been initialized to a particular\n"+
                              "       state machine definition.\n\n"+

                              "Contents of this File:\n\n" +

                              $"-Binary Structure {(plural?"Declarations":"Declaration")} ({(plural?"variables":"variable")} of type EFSM_BINARY)\n" +
                              "-Arrays for Function Pointers\n" +
                              "-Initialization Function\n" +
                              $"-EFSM State Machine Binary {(plural?"Arrays":"Array")} ({(plural?"arrays":"an array")} of type uint16_t)\n";

            code.AppendLine("/*\n----------------------------------------------------------------------------------------------------");
            code.AppendLine(notesString);
            
            code.StandardSeparator($"Binary Structure {(plural?"Declarations":"Declaration")}.\n\n" + 

                "Note:\n"+
                "Reference to a set of binary instructions (an array of 16 bit integers) is essentially\n"+
                "\"wrapped\" in a corresponding structure of type EFSM_BINARY. In turn, it is the initialized\n"+
                "instance of an EFSM_BINARY variable which is used by the EFSM execution mechanism. The \n"+
                "typedef for the EFSM_BINARY struct may be found in efsm_core.h.");

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                /*Declare the containing binary.*/
                code.AppendLine($"EFSM_BINARY {stateMachine.StateMachine.BinaryContainerName};");
            }

            code.StandardSeparator("Arrays for Function Pointers.\n\n"+
                "Note:\n"+
                "These arrays are used by the EFSM execution mechanism to access the functions which perform actions\n"+
                "and evaluate inputs as required by the binary instructions for a given state machine definition.\n"+
                "A given state machine definition will have a single array of pointers to input query functions, and\n"+
                "a single array of pointers to action functions. They are initialized by calling the Initialization \n" +
                "function (generated below), and are collectively referred to as the \"function reference arrays\" for \n" +
                "a given state machine definition. ");                  
            
            code.AppendLine();

            tempCount = 0;

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

                if(tempCount != (binaryGenerationResult.StateMachines.Length - 1))
                    code.AppendLine();
            }         
            
            code.StandardSeparator("Initialization Function.\n\n"+
                "Note:\n"+
                "This function initializes the EFSM_BINARY variables, as well as the function reference arrays for \n"+
                "every state machine definition. ");
            
            code.AppendLine($"void EFSM_{project.ProjectName}_Init()\n{{");
            code.Indent();

            tempCount = 0;

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                /*Set up the binary.*/
                code.AppendLine($"/*State Machine \"{stateMachine.StateMachine.IndexDefineName}\"*/");
                code.AppendLine();
                code.AppendLine("/*Associate an array of binary instructions with an EFSM_BINARY structure.*/");                
                code.AppendLine($"{stateMachine.StateMachine.BinaryContainerName}.data = {stateMachine.StateMachine.LocalBinaryVariableName};");
                code.AppendLine();

                code.AppendLine("/*Initialize the input functions array.*/");
                /*Set up the input reference array.*/
                var numberOfInputs = stateMachine.StateMachine.Inputs.Length;

                for (int inputIndex = 0; inputIndex < numberOfInputs; inputIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.InputReferenceArrayName}[{inputIndex}] = &{stateMachine.StateMachine.Inputs[inputIndex].FunctionName};");
                }

                code.AppendLine();
                code.AppendLine("/*Initialize the output functions array.*/");
                /*Set up the action reference array.*/
                var numberOfActions = stateMachine.StateMachine.Actions.Length;

                for (int actionIndex = 0; actionIndex < numberOfActions; actionIndex++)
                {
                    code.AppendLine($"{stateMachine.StateMachine.ActionReferenceArrayName}[{actionIndex}] = &{stateMachine.StateMachine.Actions[actionIndex].FunctionName};");
                }

                if (tempCount != (binaryGenerationResult.StateMachines.Length - 1))
                {
                    code.AppendLine();
                }

                tempCount++;
            }

            code.RemoveIndent();
            code.AppendLine("}\n");

            code.StandardSeparator($"EFSM State Machine Binary {(plural?"Arrays":"Array")}");            

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
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
            }

            code.StandardSeparator();
            code.AppendLine();
            return code.ToString();
        }
    }
    /****************************************************************************************************************************************************/
}