using System;

namespace EFSM.Generator.Model
{
    internal class ActionReferenceGenerationModel : IndexedBase<OutputGenerationModel>
    {
        public ActionReferenceGenerationModel(OutputGenerationModel model, int index) 
            : base(model, index)
        {
        }

        public override string IndexDefineName => throw new NotSupportedException();
    }
}