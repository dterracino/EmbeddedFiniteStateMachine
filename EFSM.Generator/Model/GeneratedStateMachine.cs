using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedStateMachine : IndexedBase<StateMachine>
    {
        public GeneratedStateMachine(
            StateMachine model, 
            GeneratedState[] states, 
            GeneratedInput[] inputs, 
            GeneratedOutput[] outputs,
            int index) : base(model, index)
        {
            States = states;
            Inputs = inputs;
            Outputs = outputs;
        }

        /// <summary>
        /// The states
        /// </summary>
        public GeneratedState[] States { get; }

        public GeneratedInput[] Inputs { get; }

        public GeneratedOutput[] Outputs { get; }

        public string IndexDefineName => $"EFSM_{Model.Name.FixDefineName()}_INDEX";

        public string IndexDefine => $"#define {IndexDefineName} {Index}";

        public string NumStatesDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_STATES";

        public string NumStatesDefine => $"#define {NumStatesDefineName} {States.Length}";

        public string NumInputsDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_INPUTS";

        public string NumInputsDefine => $"#define {NumInputsDefineName} {Inputs.Length}";

        public string NumOutputsDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_OUTPUTS";

        public string NumOutputsDefine => $"#define {NumOutputsDefineName} {Outputs.Length}";
    }
}