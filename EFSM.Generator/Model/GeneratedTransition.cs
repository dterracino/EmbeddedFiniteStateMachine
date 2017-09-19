using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class GeneratedTransition : IndexedBase<StateMachineTransition>
    {
        public StateMachine Parent { get; }

        public GeneratedState Target { get; }

        public GeneratedActionReference[] Actions { get; }

        public GeneratedTransition(
            StateMachineTransition model, 
            int index, 
            StateMachine parent, 
            GeneratedState target,
            GeneratedActionReference[] actions) 
            : base(model, index)
        {
            Parent = parent;
            Target = target;
            Actions = actions;
        }

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_{Model.Name.FixDefineName()}_INDEX";
    }
}