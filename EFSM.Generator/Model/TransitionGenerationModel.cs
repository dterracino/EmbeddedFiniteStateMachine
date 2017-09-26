using EFSM.Domain;

namespace EFSM.Generator.Model
{
    internal class TransitionGenerationModel : IndexedBase<StateMachineTransition>
    {
        public StateMachine Parent { get; }

        public StateGenerationModel Target { get; }

        public ActionReferenceGenerationModel[] Actions { get; }

        public TransitionGenerationModel(
            StateMachineTransition model, 
            int index, 
            StateMachine parent, 
            StateGenerationModel target,
            ActionReferenceGenerationModel[] actions) 
            : base(model, index)
        {
            Parent = parent;
            Target = target;
            Actions = actions;
        }

        public override string IndexDefineName => $"EFSM_{Parent.Name.FixDefineName()}_{Model.Name.FixDefineName()}_INDEX";
    }
}