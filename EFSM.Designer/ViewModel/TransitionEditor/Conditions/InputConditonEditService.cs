using EFSM.Domain;
using System.Linq;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public class InputConditonEditService : IConditionEditService
    {
        public ConditionType Type => ConditionType.Input;

        public int? MaximumNumberOfChildren => 0;

        public int? MinimumNumberOfChildren => null;

        public bool CanSelectInput => true;

        public string ErrorMessage => "Cannot have any children";

        public void Fix(ConditionViewModel conditionViewModel)
        {
            conditionViewModel.Conditions.Clear();

            if (conditionViewModel.InputId == null)
            {
                conditionViewModel.InputId = conditionViewModel.Owner.Inputs.FirstOrDefault()?.Id;
            }
        }
    }
}
