using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor.Conditions
{
    public interface IConditionEditService
    {
        ConditionType Type { get; }

        int? MaximumNumberOfChildren { get; }

        int? MinimumNumberOfChildren { get; }

        bool CanSelectInput { get; }

        string ErrorMessage { get; }

        void Fix(ConditionViewModel conditionViewModel);
    }
}