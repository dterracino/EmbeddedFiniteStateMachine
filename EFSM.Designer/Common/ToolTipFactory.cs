using EFSM.Designer.ViewModel;

namespace EFSM.Designer.Common
{
    public static class ToolTipFactory
    {
        public static TransitionToolTipViewModel CreateToolTip(this TransitionViewModel transition)
        {
            return new TransitionToolTipViewModel()
            {
                // Condition = ToToolTip(transition.Condition, transition.Parent.Inputs.Items),
                // Actions = ToToolTip(transition.Actions, transition.Parent.Outputs.Items)
            };
        }

        //public static StateToolTipViewModel CreateToolTip(this StateViewModel state)
        //{
        //    if (state == null)
        //        return null;

        //    return new StateToolTipViewModel()
        //    {
        //        EntryActions = ToToolTip(state.EntryActions, state.Parent.Outputs.Items),
        //        ExitActions = ToToolTip(state.ExitActions, state.Parent.Outputs.Items),
        //    };
        //}

        //private static string ToToolTip(IList<StateMachineActionMetadata> actions,
        //    ICollection<StateMachineConnectorViewModel> outputs)
        //{
        //    if (actions == null)
        //        return null;

        //    var fragments = actions
        //        .Select(a => $"{outputs.GetConnectorName(a.OutputId)} = {a.Value}");

        //    return string.Join(Environment.NewLine, fragments);
        //}

        //private static string ToToolTip(ConditionMetadata condition, ICollection<StateMachineConnectorViewModel> inputs)
        //{
        //    if (condition == null)
        //        return null;

        //    if (condition.ConditionType == null)
        //        return $"({inputs.GetConnectorName(condition.SourceInputId)} = {condition.Value})";

        //    if (condition.Conditions != null)
        //    {
        //        string result = "(";

        //        for (int index = 0; index < condition.Conditions.Count; index++)
        //        {
        //            if (index > 0)
        //            {
        //                result += $" {condition.ConditionType} ";

        //                result += ToToolTip(condition.Conditions[index], inputs);

        //            }
        //        }

        //        result += ")";

        //        return result;

        //    }

        //    return null;

        //}

        //private static string GetConnectorName(this IEnumerable<StateMachineConnectorViewModel> inputs, Guid? id)
        //{
        //    if (id == null)
        //        return null;

        //    return inputs
        //        .FirstOrDefault(i => i.Id == id)
        //        ?.Name;

        //}
    }
}
