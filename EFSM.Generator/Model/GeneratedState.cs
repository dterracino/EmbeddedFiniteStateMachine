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

        
    }
}