using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public class NotConditionEditService : IConditionEditService
    {
        public ConditionType Type => ConditionType.Not;

        public int? MaximumNumberOfChildren => 1;

        public int? MinimumNumberOfChildren => 1;

        public bool CanSelectInput => false;

        public string ErrorMessage => "Has to have one and only one child";

        public void Fix(ConditionViewModel conditionViewModel)
        {
            conditionViewModel.InputId = null;

            while (conditionViewModel.Conditions.Count > 1)
            {
                conditionViewModel.Conditions.RemoveAt(conditionViewModel.Conditions.Count - 1);
            }
        }
    }
}
