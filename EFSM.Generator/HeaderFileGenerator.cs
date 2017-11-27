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

            //headerFile.Append(project.Model.GenerationOptions.HeaderFileHeader);

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

                headerFile.AppendLine($"/*State machine {stateMachineName} (at index {stateMachineIndex}) information.*/\n");               

                headerFile.AppendLine($"#define {currentStateMachine.NumberOfInputsDefineString}      {numberOfInputs}");
                headerFile.AppendLine($"#define {currentStateMachine.NumberOfActionsDefineString}      {numberOfActions}");

                headerFile.AppendLine();
              
                headerFile.AppendLine($"extern uint8_t {currentStateMachine.InputReferenceArrayString};");
                headerFile.AppendLine($"extern void {currentStateMachine.ActionReferenceArrayString};");

                headerFile.AppendLine($"extern EFSM_BINARY {currentStateMachine.BinaryContainerName};");
                headerFile.Append("\n");

                /* code.AppendLine($"void EFSM_{stateMachine.StateMachine.IndexDefineName}_Init()\n{{\n");*/
                headerFile.AppendLine($"void EFSM_{project.StateMachinesGenerationModel[stateMachineIndex].IndexDefineName}_Init();");
                headerFile.AppendLine();                
                headerFile.AppendLine("/*Input function prototypes.*/\n");

                /*Get the inputs.*/
                var inputArray = project.StateMachinesGenerationModel[stateMachineIndex].Inputs;

                /*Prototype generation.*/
                foreach (var input in inputArray)
                {
                    headerFile.AppendLine($"uint8_t EFSM_{stateMachineName}_{input.Name}();");
                }

                headerFile.Append("\n");
                headerFile.AppendLine("/*Action function prototypes.*/\n");

                /*Get the actions.*/
                var actionArray = project.StateMachinesGenerationModel[stateMachineIndex].Actions;

                foreach (var act in actionArray)
                {
                    headerFile.AppendLine($"void EFSM_{stateMachineName}_{act.Name}();");
                }
            }

            headerFile.AppendLine();
            headerFile.AppendLine("#endif");

            

            

            return headerFile.ToString();

                /*
                 
            extern uint8_t(*efsm_NAME_inputs[1])();
   extern void(*efsm_NAME_actions[2])();
                 
                 * /

            //headerFile.AppendLine($"/* Number of state machines */
            //headerFile.AppendLine($"#define EFSM_NUM_STATE_MACHINES {project.StateMachinesGenerationModel.Length}");
            //headerFile.AppendLine();

            //headerFile.AppendLine("/* Model Machines */");

            //    foreach (var stateMachine in project.StateMachinesGenerationModel)
            //    {
            //        headerFile.AppendLine($"/* Model Machine: {stateMachine.Model.Name} */");
            //        headerFile.AppendLine(stateMachine.IndexDefine);
            //        headerFile.AppendLine(stateMachine.NumStatesDefine);
            //        headerFile.AppendLine(stateMachine.NumInputsDefine);
            //        headerFile.AppendLine(stateMachine.NumOutputsDefine);
            //        headerFile.AppendLine();

            //        // ----------- Input Functions --------------------
            //        //headerFile.AppendLine("/* Input functions */");
            //        //foreach (var input in stateMachine.Inputs)
            //        //{
            //        //    headerFile.AppendLine(input.FunctionPrototype);
            //        //}

            //        //headerFile.AppendLine();

            //        //foreach (var input in stateMachine.Inputs)
            //        //{
            //        //    headerFile.AppendLine(input.IndexDefine);
            //        //}

            //        //headerFile.AppendLine();

            //        // ----------- Output Functions --------------------
            //        //headerFile.AppendLine("/* Output functions */");
            //        //foreach (var output in stateMachine.Outputs)
            //        //{
            //        //    headerFile.AppendLine(output.FunctionPrototype);
            //        //}

            //        //headerFile.AppendLine();

            //        //foreach (var output in stateMachine.Outputs)
            //        //{
            //        //    headerFile.AppendLine(output.IndexDefine);
            //        //}

            //        //headerFile.AppendLine();

            //        //---- States ---------------
            //        headerFile.AppendLine($"/* States */");
            //        foreach (var state in stateMachine.States)
            //        {
            //            //headerFile.AppendLine($"#define EFSM_{stateMachine.Model.Name.FixDefineName()}_{state.Model.Name.FixDefineName()} {state.Index}");
            //            headerFile.AppendLine($"#define EFSM_STATE_NAME");
            //        }

            //        headerFile.AppendLine();
            //    }

            //    headerFile.Append(project.Model.GenerationOptions.HeaderFileFooter);

            //    return headerFile.ToString();
        }
    }
}