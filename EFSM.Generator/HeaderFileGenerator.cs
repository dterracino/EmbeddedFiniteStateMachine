using System.Text;
using EFSM.Generator.Model;

namespace EFSM.Generator
{
    internal class HeaderFileGenerator
    {
        internal string GenerateHeader(ProjectGenerationModel project)
        {
            StringBuilder headerFile = new StringBuilder();

            headerFile.Append(project.Model.GenerationOptions.HeaderFileHeader);

            headerFile.AppendLine($"/* Number of state machines */");
            headerFile.AppendLine($"#define EFSM_NUM_STATE_MACHINES {project.StateMachinesGenerationModel.Length}");
            headerFile.AppendLine();

            headerFile.AppendLine("/* Model Machines */");

            foreach (var stateMachine in project.StateMachinesGenerationModel)
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
                foreach (var state in stateMachine.States)
                {
                    headerFile.AppendLine($"#define EFSM_{stateMachine.Model.Name.FixDefineName()}_{state.Model.Name.FixDefineName()} {state.Index}");
                }

                headerFile.AppendLine();
            }

            headerFile.Append(project.Model.GenerationOptions.HeaderFileFooter);

            return headerFile.ToString();
        }

    }
}