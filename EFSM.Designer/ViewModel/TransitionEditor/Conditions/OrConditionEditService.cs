using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public class OrConditionEditService : IConditionEditService
    {
        public ConditionType Type => ConditionType.Or;

        public int? MaximumNumberOfChildren => int.MaxValue;

        public int? MinimumNumberOfChildren => 1;

        public bool CanSelectInput => false;

        public string ErrorMessage => "Has to have at least one child";

        public void Fix(ConditionViewModel conditionViewModel)
        {
        }
    }
}
