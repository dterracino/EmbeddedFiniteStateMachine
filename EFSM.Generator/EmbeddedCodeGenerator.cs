using System.IO;
using System.Text;
using EFSM.Domain;
using Newtonsoft.Json;

namespace EFSM.Generator
{
    public class EmbeddedCodeGenerator
    {
        public void Generate(StateMachineProject project, GenerationOptions options)
        {
            StringBuilder headerFile = new StringBuilder();
            StringBuilder codeFile = new StringBuilder();

            headerFile.Append(options.HeaderFileHeader);

            headerFile.AppendLine($"/* Number of state machines */");
            headerFile.AppendLine($"#define EFSM_NUMBER_OF_STATE_MACHINES {project.StateMachines.Length}");
            headerFile.AppendLine();

            headerFile.AppendLine("/* State Machines */");

            for (int stateMachineIndex = 0; stateMachineIndex < project.StateMachines.Length; stateMachineIndex++)
            {
                StateMachine stateMachine = project.StateMachines[stateMachineIndex];

                headerFile.AppendLine($"/* State Machine: {stateMachine.Name} */");
                headerFile.AppendLine($"#define EFSM_{stateMachine.Name.FixDefineName()}_INDEX {stateMachineIndex}");
                headerFile.AppendLine();

                // ----------- Input Functions --------------------
                headerFile.AppendLine("/* Input functions */");
                foreach (var input in stateMachine.Inputs)
                {
                    headerFile.AppendLine($"unsigned char EFSM_{stateMachine.Name.FixFunctionName()}_Input_{input.Name.FixFunctionName()}();");
                }
                headerFile.AppendLine();

                // ----------- Output Functions --------------------
                headerFile.AppendLine("/* Output functions */");
                foreach (var output in stateMachine.Actions)
                {
                    headerFile.AppendLine($"void EFSM_{stateMachine.Name.FixFunctionName()}_Output_{output.Name.FixFunctionName()}();");
                }

                headerFile.AppendLine();

                //---- States ---------------
                for (int stateIndex = 0; stateIndex < stateMachine.States.Length; stateIndex++)
                {
                    //Get the state
                    State state = stateMachine.States[stateIndex];

                    headerFile.AppendLine($"#define EFSM_{stateMachine.Name.FixDefineName()}_{state.Name.FixDefineName()} {stateIndex}");
                }

                headerFile.AppendLine();
            }

            headerFile.Append(options.HeaderFileFooter);

            File.WriteAllText(options.HeaderFilePath, headerFile.ToString());
            File.WriteAllText(options.CodeFilePath, codeFile.ToString());
        }
    }


    public class GenerationOptions
    {
        public string HeaderFilePath { get; set; }

        public string HeaderFileHeader { get; set; }

        public string HeaderFileFooter { get; set; }

        public string CodeFilePath { get; set; }

    }
}