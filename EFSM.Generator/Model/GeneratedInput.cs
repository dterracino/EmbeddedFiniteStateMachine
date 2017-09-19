using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedInput : IndexedBase<StateMachineInput>
    {
        public GeneratedInput(StateMachineInput model, int index, StateMachine parent) 
            : base(model, index)
        {
            Parent = parent;
        }

        public StateMachine Parent { get; }

        public string FunctionName => $"EFSM_{Parent.Name.FixFunctionName()}_Input_{Model.Name.FixFunctionName()}";

        public string FunctionPrototype => $"extern unsigned char {FunctionName}();";

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_INPUT_{Model.Name.FixDefineName()}_INDEX";
    }
}