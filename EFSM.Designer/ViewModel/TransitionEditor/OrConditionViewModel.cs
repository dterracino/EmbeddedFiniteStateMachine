using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class OrConditionViewModel : ConditionViewModel
    {
        public OrConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner) : base(model, owner)
        {
        }

        public override string ConditionErrorMessage => "Has to have at least one child";

        protected override bool IsConditionValid() => (Conditions.Count >= 1);

        public override bool CanAddCondition => true;

        public override bool CanSelectInput => false;


        protected override void Fix()
        {
        }
    }
}
