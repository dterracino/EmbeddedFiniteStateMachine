using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class ConditionFactory
    {
        public static ConditionViewModel Create(StateMachineCondition model, TransitionEditorViewModel owner)
        {
            switch (model.ConditionType)
            {
                case ConditionType.And: return new AndConditionViewModel(model, owner);
                case ConditionType.Input: return new InputConditionViewModel(model, owner);
                case ConditionType.Not: return new NotConditionViewModel(model, owner);
                case ConditionType.Or: return new OrConditionViewModel(model, owner);
                default: return null;
            }
        }
    }
}
