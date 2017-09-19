using System;
using System.IO;
using System.Linq;
using System.Text;
using EFSM.Domain;
using Newtonsoft.Json;

namespace EFSM.Generator
{
    public class EmbeddedCodeGenerator
    {
        public void Generate(StateMachineProject project)
        {
            var generatedProject = GetGeneratedModel(project);

            GenerateOutput(generatedProject);
        }

        private GeneratedProject GetGeneratedModel(StateMachineProject project)
        {
            return new GeneratedProject(project, project.StateMachines.Select(s => new GeneratedStateMachine(s, null)).ToArray());
        }

        private void GenerateOutput(GeneratedProject project)
        {
            StringBuilder headerFile = new StringBuilder();
            StringBuilder codeFile = new StringBuilder();

            headerFile.Append(project.Model.GenerationOptions.HeaderFileHeader);

            headerFile.AppendLine($"/* Number of state machines */");
            headerFile.AppendLine($"#define EFSM_NUM_STATE_MACHINES {project.StateMachines.Length}");
            headerFile.AppendLine();

            headerFile.AppendLine("/* Model Machines */");

            for (int stateMachineIndex = 0; stateMachineIndex < project.StateMachines.Length; stateMachineIndex++)
            {
                StateMachine stateMachine = project.StateMachines[stateMachineIndex].Model;

                headerFile.AppendLine($"/* Model Machine: {stateMachine.Name} */");
                headerFile.AppendLine($"#define EFSM_{stateMachine.Name.FixDefineName()}_INDEX {stateMachineIndex}");
                headerFile.AppendLine($"#define EFSM_{stateMachine.Name.FixDefineName()}_NUM_STATES {stateMachine.States.Length}");
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
                headerFile.AppendLine($"/* States */");
                for (int stateIndex = 0; stateIndex < stateMachine.States.Length; stateIndex++)
                {
                    //Get the state
                    State state = stateMachine.States[stateIndex];

                    headerFile.AppendLine($"#define EFSM_{stateMachine.Name.FixDefineName()}_{state.Name.FixDefineName()} {stateIndex}");
                }

                headerFile.AppendLine();
            }

            headerFile.Append(project.Model.GenerationOptions.HeaderFileFooter);

            File.WriteAllText(project.Model.GenerationOptions.HeaderFilePath, headerFile.ToString());
            File.WriteAllText(project.Model.GenerationOptions.CodeFilePath, codeFile.ToString());
        }
    }
}