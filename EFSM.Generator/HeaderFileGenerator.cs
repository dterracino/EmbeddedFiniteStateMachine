using System.Text;
using EFSM.Generator.Model;
using System.Linq;

namespace EFSM.Generator
{
    internal class HeaderFileGenerator
    {
        internal string GenerateHeader(ProjectGenerationModel project)
        {
            StringBuilder headerFile = new StringBuilder();                     

            var headerNameOnly = project.HeaderFileName.Split('.')[0];

            headerFile.AppendLine($"#ifndef {project.Model.GenerationOptions.HeaderFileHeader}_H");
            headerFile.AppendLine($"#define {project.Model.GenerationOptions.HeaderFileHeader}_H");

            headerFile.AppendLine();
            headerFile.AppendLine("#include <stdint.h>");
            headerFile.AppendLine("#include \"efsm_core.h\"");
            headerFile.AppendLine();


            for (int stateMachineIndex = 0; stateMachineIndex < project.StateMachinesGenerationModel.Length; stateMachineIndex++)
            {
                var currentStateMachine = project.StateMachinesGenerationModel[stateMachineIndex];
                var stateMachineName = project.StateMachinesGenerationModel[stateMachineIndex].IndexDefineName;
                var numberOfInputs = project.StateMachinesGenerationModel[stateMachineIndex].Inputs.Length;
                var numberOfActions = project.StateMachinesGenerationModel[stateMachineIndex].Actions.Length;

                headerFile.AppendLine($"/*State machine \"{stateMachineName}\" information.*/\n");               

                headerFile.AppendLine($"#define {currentStateMachine.NumberOfInputsDefineString}      {numberOfInputs}");
                headerFile.AppendLine($"#define {currentStateMachine.NumberOfActionsDefineString}      {numberOfActions}");

                headerFile.AppendLine();
              
                headerFile.AppendLine($"extern uint8_t {currentStateMachine.InputReferenceArrayString};");
                headerFile.AppendLine($"extern void {currentStateMachine.ActionReferenceArrayString};");

                headerFile.AppendLine($"extern EFSM_BINARY {currentStateMachine.BinaryContainerName};");
                headerFile.Append("\n");
              
                headerFile.AppendLine($"void EFSM_{project.StateMachinesGenerationModel[stateMachineIndex].IndexDefineName}_Init();");
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

            headerFile.AppendLine();
            headerFile.AppendLine("#endif");
            return headerFile.ToString();
        }
    }
}