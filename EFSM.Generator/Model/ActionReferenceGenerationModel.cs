using System;

namespace EFSM.Generator.Model
{
    internal class ActionReferenceGenerationModel 
    {
        public ActionReferenceGenerationModel(Guid id, int functionReferenceIndex, string name)            
        {
            Id = id;
            FunctionReferenceIndex = functionReferenceIndex;
            Name = name;
        }

        public Guid Id { get; }
        public int FunctionReferenceIndex { get; }
        public string Name { get; }
    }
}