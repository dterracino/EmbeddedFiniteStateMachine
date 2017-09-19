using System;

namespace EFSM.Generator.Model
{
    internal class GeneratedActionReference : IndexedBase<GeneratedOutput>
    {
        public GeneratedActionReference(GeneratedOutput model, int index) 
            : base(model, index)
        {
        }

        public override string IndexDefineName => throw new NotSupportedException();
    }
}