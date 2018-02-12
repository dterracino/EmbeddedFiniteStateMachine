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
            UInt16 tempCount;

            code.AppendLine($"#include \"{project.HeaderFileName}\"");
            code.AppendLine($"#include <stdint.h>");
            code.AppendLine("#include \"efsm_core.h\"");

            if ((project.DebugMode == DebugMode.Desktop) && project.DebuggingEnabled)
            {
                code.AppendLine("#include \"stdio.h\"");
                code.AppendLine("#include \"stdlib.h\"");
            }


            code.AppendLine();

            var plural = false;

            if (binaryGenerationResult.StateMachines.Count() > 1)
                plural = true;
          
            var notesString = "Notes\n\n" +

                              "The entire contents of this file are generated.\n" +

                              "Typically, the user should not modify files which are generated. Reasons for this are:\n\n" +
                              "-To avoid introducing errors.\n" +
                              "-Additions are lost every time a file is generated (this can be counterproductive).\n\n" +

                              "An important distinction to make is between a state machine definition, and an actual state machine.\n" +
                              "instance.\n\n" +
                              "In the EFSM environment, a state machine definition and a state machine instance are described as follows:\n\n" +

                              "   State Machine Definition\n"+
                              "      -A set of binary instructions (an array of 16 bit integers).\n" +
                              "      -An array of pointers to the functions required for evaluating inputs.\n" +
                              "      -An array of pointers to the functions required for performing actions.\n" +
                              "      -Serves as a template for behavior.\n\n" +

                              "   State Machine Instance\n"+
                              "      -A variable of type EFSM_INSTANCE which has been initialized to a particular\n"+
                              "       state machine definition.\n\n"+

                              "Contents of this File:\n\n" +

                              $"-Binary Structure {(plural?"Declarations":"Declaration")} ({(plural?"variables":"variable")} of type EFSM_BINARY)\n" +
                              $"-State Machine Instance Declaration{(plural?"s":"")} (variable{(plural?"s":"")} of type EFSM_INSTANCE)\n" +
                              "-Arrays for Function Pointers\n" +                              
                              $"-EFSM State Machine Binary {(plural?"Arrays":"Array")} ({(plural?"arrays":"an array")} of type uint16_t)\n" +
                              "-Initialization Function\n" +
                              "-EFSM Process Initialization Function (the only function in this file that should be called\n"+ 
                              " from user code)";

            code.AppendLine("/*\n----------------------------------------------------------------------------------------------------");
            code.AppendLine(notesString);
            
            code.StandardSeparator($"Binary Structure Declaration{(plural?"s":"")}.\n\n" + 

                "Note:\n"+
                "Reference to a set of binary instructions (an array of 16 bit integers) is \n"+
                "\"wrapped\" in a corresponding structure of type EFSM_BINARY. In turn, it is the initialized\n"+
                "instance of an EFSM_BINARY variable which is used by the EFSM execution mechanism. The \n"+
                "typedef for the EFSM_BINARY struct may be found in efsm_core.h.");

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                if (stateMachine.StateMachine.IncludeInGeneration)
                {
                    /*Declare the containing binary.*/
                    code.AppendLine($"EFSM_BINARY {stateMachine.StateMachine.BinaryContainerName};");
                }                
            }

            code.StandardSeparator($"State Machine Instance{(plural ? "s" : "")}");

            //Given every state machine definition, there are also state machine instances. Need to go through each state machine type, and create as many instances are required.
            foreach (var stateMachineDefinition in binaryGenerationResult.StateMachines)
            {
                if (stateMachineDefinition.StateMachine.IncludeInGeneration)
                {
                    code.AppendLine($"/*{stateMachineDefinition.StateMachine.IndexDefineName} Instances*/");

                    for (int i = 0; i < stateMachineDefinition.StateMachine.NumberOfInstances; i++)
                    {
                        code.AppendLine($"EFSM_INSTANCE {stateMachineDefinition.StateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i.ToString()};");
                    }

                    code.AppendLine();
                }                
            }

            code.StandardSeparator($"State Machine Instance Array Declaration");
            code.AppendLine($"EFSM_INSTANCE * efsmInstanceArray[{binaryGenerationResult.TotalNumberOfInstancesDefine}];");
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
                if (stateMachine.StateMachine.IncludeInGeneration)
                {
                    code.AppendLine($"/*State Machine Definition \"{stateMachine.StateMachine.IndexDefineName}\"*/");
                    code.AppendLine();
                    code.AppendLine("/*Array for pointers to input query functions.*/");

                    /*Declare the input function reference array.*/
                    code.AppendLine($"uint8_t {stateMachine.StateMachine.InputReferenceArrayString};");
                    code.AppendLine();

                    code.AppendLine("/*Array for pointers to action functions.*/");

                    /*Declare the output action function reference array.*/
                    code.AppendLine($"void {stateMachine.StateMachine.ActionReferenceArrayString};");

                    if (tempCount != (binaryGenerationResult.StateMachines.Length - 1))
                        code.AppendLine();
                }                
            }                    

            code.StandardSeparator($"EFSM Definition Binary {(plural?"Arrays":"Array")}");            

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                if (stateMachine.StateMachine.IncludeInGeneration)
                {
                    /*Create the state machine binary array.*/
                    int currentAddress = 0;

                    code.AppendLine();
                    code.AppendLine($"/* {stateMachine.StateMachine.Model.Name} Definition*/");
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
            }          

            code.StandardSeparator("Initialization Function.\n\n" +
                "Note:\n" +
                "This function: \n" + 
                "-Initializes the efsmInstanceArray.\n"+
                "-Initializes the EFSM_BINARY variables, as well as the function reference arrays for \n" +
                " every state machine definition. \n"+
                "-Makes calls to EFSM_InitializeInstance() function for the purpose of initializing the \n"+
                " state  machine instances themselves.");

            code.AppendLine($"void EFSM_{project.ProjectName}_Init()\n{{");
            code.Indent();

            tempCount = 0;

            /*Generate code to initialize instance array.*/
            code.AppendLine("/*efsmInstanceArray initialization.*/");
            code.AppendLine();

            foreach (var efsmDef in binaryGenerationResult.StateMachines)
            {
                if (efsmDef.StateMachine.IncludeInGeneration)
                {
                    for (int i = 0; i < efsmDef.StateMachine.NumberOfInstances; i++)
                    {
                        code.AppendLine($"efsmInstanceArray[{tempCount.ToString()}] = &{(efsmDef.StateMachine.IndexDefineName.Replace(' ', '_'))}_Instance_{i.ToString()};");
                        tempCount++;
                    }
                }                
            }

            code.AppendLine();

            tempCount = 0;

            foreach (var stateMachine in binaryGenerationResult.StateMachines)
            {
                if (stateMachine.StateMachine.IncludeInGeneration)
                {
                    /*Set up the binary.*/
                    code.AppendLine($"/*State Machine Definition \"{stateMachine.StateMachine.IndexDefineName}\" Initialization*/");
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
            }

            /*Now, need to generate code which initializes the instances themselves.*/

            code.AppendLine();
            code.AppendLine("/*Instance initializations.*/");            

            string argString;

            foreach (var efsmDef in binaryGenerationResult.StateMachines)
            {
                if (efsmDef.StateMachine.IncludeInGeneration)
                {
                    for (int i = 0; i < efsmDef.StateMachine.NumberOfInstances; i++)
                    {
                        argString = $"&{efsmDef.StateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i.ToString()}, " +     //Current Instance
                                    $"&{efsmDef.StateMachine.BinaryContainerName}, " +     //Relevant Binary
                                    $"{efsmDef.StateMachine.ActionReferenceArrayName}, " +     //Actions array for relevant binary
                                    $"{efsmDef.StateMachine.InputReferenceArrayName}, " +   //Inputs array for relevant binary   
                                    $"{i.ToString()}";                                                      //Instance of type index. 

                        code.AppendLine($"EFSM_InitializeInstance({argString});");
                    }
                }               
            }

            code.RemoveIndent();
            code.AppendLine("}\n");

            code.StandardSeparator("EFSM Process Initialization.");

            code.AppendLine();

            code.AppendLine("void EFSM_InitializeProcess()\n{");
            code.Indent();
            code.AppendLine($"EFSM_{project.ProjectName}_Init();");
            code.RemoveIndent();
            code.AppendLine("}");
            code.StandardSeparator("Diagnostics");

            code.AppendLine("void EFSM_GeneratedDiagnostics(EFSM_INSTANCE * efsmInstance)");
            code.AppendLine("{");
            code.AddIndent();

            code.AppendLine("for(int i = 0; i < efsmInstance->totalNumberOfInputs; i++)");
            code.AppendLine("{");
            code.AddIndent();
            code.AppendLine("efsmInstance->inputBuffer[i] = efsmInstance->InputQueries[i](efsmInstance->indexOnEfsmType);");
            code.RemoveIndent();
            code.AppendLine("}");
            code.RemoveIndent();
            code.AppendLine("}");
            code.AppendLine();

            if (project.DiagnosticsEnabled)
            {
                code.AppendLine("/*State Machine State Accessors*/");
                code.AppendLine();

                foreach (var stateMachine in project.StateMachinesGenerationModel)
                {
                    if (stateMachine.IncludeInGeneration)
                    {
                        for (int i = 0; i < stateMachine.NumberOfInstances; i++)
                        {
                            code.AppendLine($"uint32_t Get_{stateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i}_State()");
                            code.AppendLine("{");
                            code.AddIndent();

                            code.AppendLine($"return {stateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i}.state;");
                            
                            code.RemoveIndent();
                            code.AppendLine("}");
                        }
                    }
                }

                code.AppendLine();
                code.AppendLine("/*State Machine Input Accessors*/");
                code.AppendLine();

                foreach (var stateMachine in project.StateMachinesGenerationModel)
                {
                    if (stateMachine.IncludeInGeneration)
                    {
                        for (int i = 0; i < stateMachine.NumberOfInstances; i++)
                        {
                            for (int j = 0; j < stateMachine.Inputs.Length; j++)
                            {
                                code.AppendLine($"/*Corresponds to input \"{stateMachine.Inputs[j].Name}\" for instance {i} of state machine definition \"{stateMachine.IndexDefineName}\".*/");
                                //code.AppendLine($"UINT32 Get_{stateMachine.Name.Replace(' ', '_')}_{i}_Input_{j}()");
                                code.AppendLine($"uint32_t Get_{stateMachine.IndexDefineName.Replace(' ', '_')}_{i}_Input_{j}()");
                                code.AppendLine("{");
                                code.AddIndent();

                                code.AppendLine($"return {stateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i}.inputBuffer[{j}];");

                                code.RemoveIndent();
                                code.AppendLine("}");
                                code.AppendLine();
                            }
                        }
                    }
                }
            }

            if ((project.DebugMode == DebugMode.Desktop) && (project.DebuggingEnabled))
            {
                code.StandardSeparator("EFSM Debugging");

                foreach (var stateMachine in binaryGenerationResult.StateMachines)
                {
                    foreach (var input in stateMachine.StateMachine.Inputs)
                    {
                        code.AppendLine(input.FunctionSignature);
                        code.AppendLine("{");
                        code.AddIndent();
                        code.AppendLine($"return efsmDebugControl.debugBuffer[EFSM_DEBUG_PROTOCOL_INDEX_CYCLE_CMD_INPUTS_START + {input.FunctionReferenceIndex}];");
                        code.RemoveIndent();
                        code.AppendLine("}");
                    }

                    code.AppendLine();

                    foreach (var action in stateMachine.StateMachine.Actions)
                    {
                        code.AppendLine(action.FunctionSignature);
                        code.AppendLine("{");
                        code.AddIndent();
                        
                        code.RemoveIndent();
                        code.AppendLine("}");
                    }
                }

                /*Generate the main function.*/
                code.AppendLine();
                code.AppendLine("int main(int argc, char *argv[])");
                code.AppendLine("{");
                code.AddIndent();
                code.AppendLine("printf(\"Starting the EFSM Debug Manager...\\n\\n\");");
                code.AppendLine("strcpy(debugStatusTxFilename, argv[1]);");
                code.AppendLine("strcpy(debugCommandRxFilename, argv[2]);\n");
                code.AppendLine("while(1)");
                code.AppendLine("{");
                code.AddIndent();
                code.AppendLine("EfsmDebugManager();");                
                code.RemoveIndent();                
                code.AppendLine("}");
                code.AppendLine();
                code.AppendLine("return 0;");
                code.RemoveIndent();
                code.AppendLine("}");
            }

            code.AppendLine();
            return code.ToString();
        }
    }
    /****************************************************************************************************************************************************/
}