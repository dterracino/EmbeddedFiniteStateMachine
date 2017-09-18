using EFSM.Designer.ViewModel;
using EFSM.Designer.ViewModel.TransitionEditor;
using EFSM.Domain;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.Designer.Extensions
{
    public static class ConditionExtensions
    {
        public static ConditionViewModelBase ToViewModel(this StateMachineCondition conditionMetadata, TransitionEditorViewModel owner)
        {
            if (conditionMetadata == null)
                return null;

            //Treat this as a compound condition
            if (conditionMetadata.CompoundConditionType.HasValue)
            {
                var compoundCondition = new CompoundConditionViewModel(owner)
                {
                    ConditionType = conditionMetadata.CompoundConditionType.Value
                };

                if (conditionMetadata.Conditions != null)
                {
                    foreach (var childConditionMetadata in conditionMetadata.Conditions)
                    {
                        var temporaryCondition = childConditionMetadata.ToViewModel(owner);

                        if (temporaryCondition != null)
                        {
                            compoundCondition.Children.Add(temporaryCondition);
                        }
                    }
                }

                return compoundCondition;

            }

            //We should have a warning about this at some point
            if (conditionMetadata.SourceInputId == null || conditionMetadata.Value == null)
                return null;

            return new SimpleConditionViewModel(owner)
            {
                SourceInputId = conditionMetadata.SourceInputId.Value,
                Value = conditionMetadata.Value.Value
            };
        }

        public static StateMachineCondition ToMetadata(this ConditionViewModelBase conditionViewModel)
        {
            var simple = conditionViewModel as SimpleConditionViewModel;

            if (simple != null)
            {
                return new StateMachineCondition()
                {
                    SourceInputId = simple.SourceInputId,
                    Value = simple.Value
                };
            }

            var compound = conditionViewModel as CompoundConditionViewModel;

            if (compound != null)
            {
                return new StateMachineCondition()
                {
                    CompoundConditionType = compound.ConditionType,
                    Conditions = compound.Children
                        .Select(c => c.ToMetadata())
                        .ToList()
                };
            }

            return null;
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
