using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedOutput : IndexedBase<StateMachineOutputAction>
    {
        public GeneratedOutput(StateMachineOutputAction model, int index, StateMachine parent) 
            : base(model, index)
        {
            Parent = parent;
        }

        public StateMachine Parent { get; }

        public string FunctionName => $"EFSM_{Parent.Name.FixFunctionName()}_Output_{Model.Name.FixFunctionName()}";

        public string FunctionPrototype => $"extern void {FunctionName}();";

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_OUTPUT_{Model.Name.FixDefineName()}_INDEX";
    }
}