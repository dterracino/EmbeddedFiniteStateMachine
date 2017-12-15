using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.ViewModel;
using EFSM.Designer.ViewModel.TransitionEditor;
using EFSM.Designer.ViewModel.TransitionEditor.Conditions;
using EFSM.Domain;
using System.Collections.Generic;

namespace EFSM.Designer.Extensions
{
    public static class ConditionExtensions
    {
        public static ConditionViewModel ToViewModel(this StateMachineCondition conditionMetadata, TransitionEditorViewModel owner)
        {
            if (conditionMetadata == null)
                return null;

            return new ConditionViewModel(
                conditionMetadata,
                owner,
                ApplicationContainer.Container.Resolve<ConditionEditServiceManager>(),
                ApplicationContainer.Container.Resolve<IMessageBoxService>());
        }

        public static void DeleteInputFromConditions(this List<StateMachineConditionViewModel> conditions, StateMachineInputViewModel input)
        {
            conditions.RemoveAll(c => c.SourceInputId.HasValue && c.SourceInputId.Value == input.Id);

            foreach (var condition in conditions)
            {
                if (condition.Conditions != null)
                {
                    condition.Conditions.DeleteInputFromConditions(input);
                }
            }
        }
    }
}
