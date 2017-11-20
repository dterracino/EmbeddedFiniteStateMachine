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

            headerFile.Append(project.Model.GenerationOptions.HeaderFileHeader);
            headerFile.AppendLine();

            var stateMachineName = project.StateMachinesGenerationModel[0].IndexDefineName.ToUpper();
            var numberOfInputs = project.StateMachinesGenerationModel[0].Inputs.Length;
            var numberOfActions = project.StateMachinesGenerationModel[0].Actions.Length;

            headerFile.AppendLine($"#define EFSM_{stateMachineName}_NUMBER_OF_INPUTS     {numberOfInputs}");
            headerFile.AppendLine($"#define EFSM_{stateMachineName}_NUMBER_OF_ACTIONS       {numberOfActions}");

            headerFile.AppendLine();

            headerFile.AppendLine($"extern uint8_t (*EFSM_{stateMachineName}_Inputs[EFSM_{stateMachineName}_NUMBER_OF_INPUTS])();");
            headerFile.AppendLine($"extern void (*EFSM_{stateMachineName}_Actions[EFSM_{stateMachineName}_NUMBER_OF_ACTIONS])();");

            /*Get the inputs.*/
            var inputArray = project.StateMachinesGenerationModel[0].Inputs;

            /*Prototype generation.*/
            foreach (var input in inputArray)
            {
                headerFile.AppendLine($"uint8_t EFSM_{stateMachineName}_{input.Name}();");
            }

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