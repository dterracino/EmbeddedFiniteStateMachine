using System;

namespace EFSM.Generator.Model
{
    internal class ActionReferenceGenerationModel 
    {
        public ActionReferenceGenerationModel(Guid id, int functionReferenceIndex, string name, string parentStateMachineName)            
        {
            Id = id;
            FunctionReferenceIndex = functionReferenceIndex;
            Name = name;
            FunctionNamePrefix = "EFSM";
            ParentStateMachineName = parentStateMachineName;
        }

        public Guid Id { get; }
        public int FunctionReferenceIndex { get; }
        public string Name { get; }
        public string ParentStateMachineName { get; }
        public string FunctionNamePrefix { get; }
        public string FunctionName
        {
            get
            {
                return $"{FunctionNamePrefix}_{ParentStateMachineName}_{Name}";
            }
        }
    }
}