using EFSM.Domain;
using System.Linq;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class InputConditionViewModel : ConditionViewModel
    {
        public InputConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner) : base(model, owner)
        {
        }

        public override string ConditionErrorMessage => "Cannot have any children";

        public override bool CanAddCondition => false;

        public override bool CanSelectInput => true;


        protected override bool IsConditionValid() => !Conditions.Any();

        protected override void Fix()
        {
            Conditions.Clear();

            if (InputId == null)
            {

                //Select the first item
                InputId = _owner.Inputs
                    .FirstOrDefault()?.Id;
            }
        }
    }
}
