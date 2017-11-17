using EFSM.Domain;
using System;

namespace EFSM.Generator.Model
{
    internal class InputGenerationModel
    {
        public InputGenerationModel(StateMachine parent, Guid id, string name)            
        {
            Parent = parent;
            Id = id;
            Name = name;
        }

        public StateMachine Parent { get; }

        public Guid Id { get; }

        public int? FunctionReferenceIndex { get; set; }

        public string Name { get; }

        //public string FunctionName => $"EFSM_{Parent.Name.FixFunctionName()}_Input_{Model.Name.FixFunctionName()}";

        public string FunctionPrototype => $"extern unsigned char {Name}();";

        //public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_INPUT_{Model.Name.FixDefineName()}_INDEX";

        public string IndexDefine = "input index define";
    }
}
