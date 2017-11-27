using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class NotConditionViewModel : ConditionViewModel
    {
        public NotConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner) : base(model, owner)
        {
        }

        public override string ConditionErrorMessage => "Has to have one and only one child";

        public override bool CanAddCondition => Conditions.Count == 0;

        public override bool CanSelectInput => false;

        protected override bool IsConditionValid() => Conditions.Count == 1;


        protected override void Fix()
        {
            InputId = null;

            while (Conditions.Count > 1)
            {
                Conditions.RemoveAt(Conditions.Count - 1);
            }
        }
    }
}
