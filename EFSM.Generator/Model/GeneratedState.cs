using System.Collections.Generic;
using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedState : IndexedBase<State>
    {
        public GeneratedState(State model, int index, StateMachine parent) 
            : base(model, index)
        {
            Parent = parent;
        }

        public StateMachine Parent { get; }

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_INDEX";

        public List<GeneratedTransition> Transitions { get; } = new List<GeneratedTransition>();
    }
}