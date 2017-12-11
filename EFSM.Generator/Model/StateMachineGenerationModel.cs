using EFSM.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public override string IndexDefineName { get { return SourceStateMachine.Name; } }

        /**/
        public List<StateGenerationModel> States { get; }

        private StateMachine SourceStateMachine { get; }
        
        public InputGenerationModel[] Inputs
        {
            get
            {
                /*Aggregate inputs from states and return.*/
                var inputList = new List<InputGenerationModel>();

                foreach (var state in States)
                {
                    foreach (var input in state.inputList)
                    {
                        if (inputList.Exists(i => i.Id == input.Id) == false)
                            inputList.Add(input);
                    }
                }

                var returnValue = inputList.OrderBy(i => i.FunctionReferenceIndex).ToArray();

                return returnValue;
            }
        }



        public ActionReferenceGenerationModel[] Actions
        {
            get
            {
                var actionList = new List<ActionReferenceGenerationModel>();

                foreach (var st in States)
                {
                    foreach (var transition in st.transitionList)
                    {
                        foreach (var action in transition.Actions)
                        {
                            if (actionList.Exists(a => a.Id == action.Id) == false)
                            {
                                actionList.Add(action);
                            }
                        }
                    }
                }

                var returnValue = actionList.OrderBy(a => a.FunctionReferenceIndex).ToArray();

                return returnValue;
            }
        }

        //public override string IndexDefineName => $"EFSM_{Model.Name.FixDefineName()}_INDEX";

        public string NumStatesDefineName => $"EFSM_{Model.Name.FixDefineName()}_NUM_STATES";

        public string NumStatesDefine => $"#define {NumStatesDefineName} {States.Count}";

        public string NumInputsDefineName => $"EFSM_{SourceStateMachine.Name}_NUM_INPUTS";

        //public string NumInputsDefine => $"#define {NumInputsDefineName} {Inputs.Length}";
        public string NumInputsDefine => $"#define TEST_NAME 5";

        public string NumOutputsDefineName => $"EFSM_{SourceStateMachine.Name}_NUM_OUTPUTS";

        //public string NumOutputsDefine => $"#define {NumOutputsDefineName} {Outputs.Length}";
        public string NumOutputsDefine => $"#define TEST_NAME 6";

        public string LocalBinaryVariableName => $"efsm_{SourceStateMachine.Name}_binaryData";
        public string BinaryContainerName => $"{SourceStateMachine.Name.Replace(' ', '_')}_Binary";   
        public UInt16 BinaryContainerId => 0xabcd;
        public string NumberOfInputsDefineString => $"EFSM_{IndexDefineName.ToUpper()}_NUMBER_OF_INPUTS";

        public string NumberOfActionsDefineString => $"EFSM_{IndexDefineName.ToUpper()}_NUMBER_OF_OUTPUTS";
        public string InputReferenceArrayName => $"{IndexDefineName}_Inputs";
        public string InputReferenceArrayString => $"(*{IndexDefineName.Replace(' ', '_')}_Inputs[{NumberOfInputsDefineString.Replace(' ', '_')}])(uint8_t indexOnEfsmType)";
        public string ActionReferenceArrayName => $"{IndexDefineName}_OutputActions";
        public string ActionReferenceArrayString => $"(*{IndexDefineName.Replace(' ', '_')}_OutputActions[{NumberOfActionsDefineString.Replace(' ', '_')}])(uint8_t indexOnEfsmType)";
    }
}