using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class StateMachineGenerationModel : IndexedBase<StateMachine>
    {
        public StateMachineGenerationModel(
            StateMachine model, 
            StateGenerationModel[] states, 
            InputGenerationModel[] inputs, 
            OutputGenerationModel[] outputs,
            int index) : base(model, index)
        {
            States = states;
            Inputs = inputs;
            Outputs = outputs;
        }

        /// <summary>
        /// The states
        /// </summary>
        public StateGenerationModel[] States { get; }

        public InputGenerationModel[] Inputs { get; }

        public OutputGenerationModel[] Outputs { get; }

        public override string IndexDefineName => $"EFSM_{Model.Name.FixDefineName()}_INDEX";

        public string NumStatesDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_STATES";

        public string NumStatesDefine => $"#define {NumStatesDefineName} {States.Length}";

        public string NumInputsDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_INPUTS";

        public string NumInputsDefine => $"#define {NumInputsDefineName} {Inputs.Length}";

        public string NumOutputsDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_OUTPUTS";

        public string NumOutputsDefine => $"#define {NumOutputsDefineName} {Outputs.Length}";

        public string LocalBinaryVariableName => $"efsm_stateMachineDefinition_{Index}";
    }
}