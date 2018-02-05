using System.Text;
using EFSM.Generator.Model;
using System.Linq;

namespace EFSM.Generator
{
    internal class HeaderFileGenerator
    {
        internal string GenerateHeader(ProjectGenerationModel project, ProjectBinaryGenerationResult binaryGenerationResult)
        {
            StringBuilder headerFile = new StringBuilder();            

            var headerNameOnly = project.HeaderFileName.Split('.')[0];

            headerFile.AppendLine($"#ifndef {project.Model.GenerationOptions.HeaderFileHeader}_H");
            headerFile.AppendLine($"#define {project.Model.GenerationOptions.HeaderFileHeader}_H");       

            headerFile.AppendLine();
            headerFile.AppendLine("#include <stdint.h>");
            headerFile.AppendLine("#include \"efsm_core.h\"");
            headerFile.AppendLine();

            headerFile.AppendLine($"/*\n----------------------------------------------------------------------------------------------------\nGeneral information.\n*/");
            headerFile.AppendLine();
            headerFile.AppendLine($"#define {binaryGenerationResult.TotalNumberOfInstancesDefine}       {binaryGenerationResult.TotalNumberOfInstances.ToString()}");            
                        
            headerFile.AppendLine();
            headerFile.AppendLine("void EFSM_GeneratedDiagnostics(EFSM_INSTANCE * efsmInstance);");

            headerFile.AppendLine($"/*\n----------------------------------------------------------------------------------------------------\nConfiguration parameters and debugging.\n*/");

            headerFile.AppendLine($"#define {project.DebugModeEmbeddedDefine}             0");
            headerFile.AppendLine($"#define {project.DebugModeDesktopDefine}              1\n");

            headerFile.AppendLine("#define EFSM_CONFIG_PROJECT_AVAILABLE        1");
            headerFile.AppendLine("#define EFSM_CONFIG_ENABLE_DEBUGGING         1");
            headerFile.AppendLine($"#define EFSM_CONFIG_DEBUG_MODE               {project.DebugModeDefine}");            

            for (int stateMachineIndex = 0; stateMachineIndex < project.StateMachinesGenerationModel.Length; stateMachineIndex++)
            {
                if (project.StateMachinesGenerationModel[stateMachineIndex].IncludeInGeneration)
                {
                    var currentStateMachine = project.StateMachinesGenerationModel[stateMachineIndex];
                    var stateMachineName = project.StateMachinesGenerationModel[stateMachineIndex].IndexDefineName;
                    var numberOfInputs = project.StateMachinesGenerationModel[stateMachineIndex].Inputs.Length;
                    var numberOfActions = project.StateMachinesGenerationModel[stateMachineIndex].Actions.Length;

                    headerFile.AppendLine($"/*\n----------------------------------------------------------------------------------------------------\nState machine \"{stateMachineName}\" information.\n*/");

                    headerFile.AppendLine($"#define {currentStateMachine.NumberOfInputsDefineString}      {numberOfInputs}");
                    headerFile.AppendLine($"#define {currentStateMachine.NumberOfActionsDefineString}      {numberOfActions}");

                    headerFile.AppendLine();

                    headerFile.AppendLine($"extern uint8_t {currentStateMachine.InputReferenceArrayString};");
                    headerFile.AppendLine($"extern void {currentStateMachine.ActionReferenceArrayString};");

                    headerFile.AppendLine($"extern EFSM_BINARY {currentStateMachine.BinaryContainerName};");
                    headerFile.Append("\n");

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //headerFile.AppendLine($"void EFSM_{project.StateMachinesGenerationModel[stateMachineIndex].IndexDefineName}_Init();");
                    headerFile.AppendLine();
                    headerFile.AppendLine("/*Input function prototypes.*/\n");

                    /*Get the inputs.*/
                    var inputArray = project.StateMachinesGenerationModel[stateMachineIndex].Inputs;

                    /*Prototype generation.*/
                    foreach (var input in inputArray)
                    {
                        headerFile.AppendLine(input.FunctionPrototype);
                    }

                    headerFile.Append("\n");
                    headerFile.AppendLine("/*Action function prototypes.*/\n");

                    /*Get the actions.*/
                    var actionArray = project.StateMachinesGenerationModel[stateMachineIndex].Actions;

                    foreach (var act in actionArray)
                    {
                        headerFile.AppendLine(act.FunctionPrototype);
                    }

                    headerFile.AppendLine();
                    headerFile.AppendLine();
                }                
            }

            headerFile.AppendLine($"/*\n----------------------------------------------------------------------------------------------------\nGeneral (applies to all state machine definitions).\n*/\n");
            headerFile.AppendLine("/*State machine definitions initialization prototype.*/");
            headerFile.AppendLine($"void EFSM_{project.ProjectName}_Init();");
            headerFile.AppendLine($"void EFSM_InitializeProcess();");
            headerFile.AppendLine();

            headerFile.AppendLine($"/*\n----------------------------------------------------------------------------------------------------\nDiagnostics.\n*/\n");

            if (project.DebuggingEnabled)
            {
                headerFile.AppendLine("#define EFSM_GENERATED_DIAGNOSTICS");
                headerFile.AppendLine();
                headerFile.AppendLine("/*State Machine State Accessor Prototypes*/");
                headerFile.AppendLine();

                foreach (var stateMachine in project.StateMachinesGenerationModel)
                {
                    if (stateMachine.IncludeInGeneration)
                    {
                        for (int i = 0; i < stateMachine.NumberOfInstances; i++)
                        {
                            headerFile.AppendLine($"uint32_t Get_{stateMachine.IndexDefineName.Replace(' ', '_')}_Instance_{i}_State();");                         
                        }
                    }
                }

                headerFile.AppendLine();
                headerFile.AppendLine("/*State Machine Input Accessor Prototypes*/");
                headerFile.AppendLine();

                foreach (var stateMachine in project.StateMachinesGenerationModel)
                {
                    if (stateMachine.IncludeInGeneration)
                    {
                        for (int i = 0; i < stateMachine.NumberOfInstances; i++)
                        {
                            for (int j = 0; j < stateMachine.Inputs.Length; j++)
                            {
                                headerFile.AppendLine($"uint32_t Get_{stateMachine.IndexDefineName.Replace(' ', '_')}_{i}_Input_{j}();");
                            }
                        }
                    }
                }
            }

            headerFile.AppendLine("#endif");
            return headerFile.ToString();
        }
    }
}