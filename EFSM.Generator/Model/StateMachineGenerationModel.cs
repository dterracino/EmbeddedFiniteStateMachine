using EFSM.Domain;
using System.Collections.Generic;

namespace EFSM.Generator.Model
{
    internal class StateMachineGenerationModel : IndexedBase<StateMachine>
    {
        public StateMachineGenerationModel(StateMachine model, int index):base(model, index)
        {
            List<StateGenerationModel> states = new List<StateGenerationModel>();
            States = states;
            SourceStateMachine = model;
        }

        public override string IndexDefineName =>  $"not initialized";

        /**/
        public List<StateGenerationModel> States { get; }

        private StateMachine SourceStateMachine { get; }
        //public StateMachineGenerationModel(
        //    StateMachine model, 
        //    StateGenerationModel[] states, 
        //    InputGenerationModel[] inputs, 
        //    OutputGenerationModel[] outputs,
        //    int index) : base(model, index)
        //{
        //    States = states;
        //    Inputs = inputs;
        //    Outputs = outputs;
        //}

        ///// <summary>
        ///// The states
        ///// </summary>
        //public StateGenerationModel[] States { get; }

        public InputGenerationModel[] Inputs { get; }

        public OutputGenerationModel[] Outputs { get; }

        //public override string IndexDefineName => $"EFSM_{Model.Name.FixDefineName()}_INDEX";

        public string NumStatesDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_STATES";

        public string NumStatesDefine => $"#define {NumStatesDefineName} {States.Count}";

        public string NumInputsDefineName => $"EFSM_{SourceStateMachine.Name}_NUM_INPUTS";

        //public string NumInputsDefine => $"#define {NumInputsDefineName} {Inputs.Length}";
        public string NumInputsDefine => $"#define TEST_NAME 5";

        public string NumOutputsDefineName => $"EFSM_{SourceStateMachine.Name}_NUM_OUTPUTS";

        //public string NumOutputsDefine => $"#define {NumOutputsDefineName} {Outputs.Length}";
        public string NumOutputsDefine => $"#define TEST_NAME 6";

        public string LocalBinaryVariableName => $"efsm_{SourceStateMachine.Name}_binary_{Index}";
    }
}