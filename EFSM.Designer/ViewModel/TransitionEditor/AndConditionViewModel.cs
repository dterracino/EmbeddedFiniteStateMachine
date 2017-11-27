using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class AndConditionViewModel : ConditionViewModel
    {
        public AndConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner) : base(model, owner)
        {
        }

        public override string ConditionErrorMessage => "Has to have at least one child";

        public override bool CanAddCondition => true;

        protected override void Fix()
        {
            InputId = null;
        }

        protected override bool IsConditionValid()
        {
            return (Conditions.Count >= 1);
        }

        public override bool CanSelectInput => false;

    }
}
