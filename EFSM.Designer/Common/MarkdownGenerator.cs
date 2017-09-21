using EFSM.Designer.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EFSM.Designer.Common
{
    public class MarkdownGenerator
    {
        private StateMachineViewModel _stateMachine;

        private List<Guid> _conditionGuids = new List<Guid>();

        public void Generate(StateMachineViewModel stateMachine, string imageFullPath)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));

            var s = $"## State Machine Name: {stateMachine.Name}\n";
            s += AddStatesList();

            s += AddInfoForEachState();

            s += AddTransitionsList();

            s += AddInfoForEachTransition();

            s += AddImage(imageFullPath);

            // File.WriteAllText(@"C:\GIT\EmbeddedFiniteStateMachine\EFSM.Designer\readme.md", s);
            var path = imageFullPath.Remove(imageFullPath.LastIndexOf('.')) + ".md";
            File.WriteAllText(path, s);
        }

        private string AddInfoForEachState()
        {
            string s = string.Empty;

            foreach (var item in _stateMachine.States)
            {
                s += $"#### State: {item.Name}";
                s += Environment.NewLine;
                s += AddEntryFunctions(item);
                s += Environment.NewLine;
                s += AddExitFunctions(item);
                s += Environment.NewLine;
            }

            return s;
        }

        private string AddInfoForEachTransition()
        {
            string s = string.Empty;

            foreach (var item in _stateMachine.Transitions)
            {
                s += $"#### Transition: {item.Name}";
                s += Environment.NewLine;
                s += AddOutputActions(item);
                s += Environment.NewLine;
                s += AddConditions(item);
                s += Environment.NewLine;
            }

            return s;
        }

        private string AddOutputActions(TransitionViewModel transition)
        {
            var s = $"##### No Output Actions";

            if (transition.Actions != null && transition.Actions.Any())
            {
                s = $"##### Output Actions";
                s += Environment.NewLine;

                foreach (var item in transition.Actions)
                {
                    s += $"* {(_stateMachine.Outputs.First(x => x.Id == item).Name)}";
                }
            }

            return s;
        }

        private string AddConditions(TransitionViewModel transition)
        {
            var s = $"##### No Condition";

            if (transition.Condition != null)
            {
                s = $"##### Condition";
                s += Environment.NewLine;

                s += AddCondition(transition.Condition);
            }

            s = RenameConditionsGuid(s);

            _conditionGuids.Clear();

            return s;
        }

        private string RenameConditionsGuid(string s)
        {
            char letter = 'A';

            for (int i = 0 + letter; i < _conditionGuids.Count + letter; i++)
            {
                Guid item = _conditionGuids[i - letter];
                string replaceLetter = ((char)i).ToString();
                s = s.Replace(item.ToString(), replaceLetter);
            }

            return s;
        }

        private string AddCondition(StateMachineConditionViewModel condition)
        {
            string s = string.Empty;

            if (condition.CompoundConditionType == null)
            {
                if (condition.SourceInputId != null)
                {
                    if (!_conditionGuids.Contains(condition.SourceInputId.Value))
                    {
                        _conditionGuids.Add(condition.SourceInputId.Value);
                    }

                    s += $"{condition.SourceInputId.Value}== {condition.Value}";
                }
            }
            else
            {
                s += " ( ";
                string operation = condition.CompoundConditionType == Domain.CompoundConditionType.And ? " AND " : " OR ";

                for (int i = 0; i < condition.Conditions.Count; i++)
                {
                    var item = condition.Conditions[i];

                    if (i > 0)
                    {
                        s += operation;
                    }

                    s += AddCondition(item);
                }

                foreach (var item in condition.Conditions)
                {
                    s += AddCondition(item);
                }

                s += " ) ";
            }

            return s;
        }

        private string AddTransitionsList()
        {
            string s = $"### Transitions:";
            s += Environment.NewLine;

            foreach (var item in _stateMachine.Transitions)
            {
                s += $"* {item.Name}\n";
            }
            return s;
        }

        private string AddStatesList()
        {

            string s = $"### States:";
            s += Environment.NewLine;
            foreach (var item in _stateMachine.States)
            {
                s += $"* {item.Name}\n";
            }

            return s;
        }

        private string AddImage(string imagePath)
        {
            string saving = imagePath.Remove(0, imagePath.IndexOf(@":\") + 2);
            saving = saving.Replace(" ", "%20");
            return $"![image](/{saving})";
        }

        private string AddExitFunctions(StateViewModel state)
        {
            string s = string.Empty;

            if (state.ExitActions != null && state.ExitActions.Any())
            {
                s = $"##### Exit Actions";
                s += Environment.NewLine;

                foreach (var item in state.ExitActions)
                {
                    s += $"* {(_stateMachine.Outputs.First(x => x.Id == item).Name)}\n";
                }
            }
            else
            {
                s = $"##### No Exit Actions";
            }

            return s;
        }

        private string AddEntryFunctions(StateViewModel state)
        {
            string s = string.Empty;

            if (state.EntryActions != null && state.EntryActions.Any())
            {
                s = $"##### Entry Actions";
                s += Environment.NewLine;

                foreach (var item in state.EntryActions)
                {
                    s += $"* {(_stateMachine.Outputs.First(x => x.Id == item).Name)}\n";
                }
            }
            else
            {
                s = $"##### No Entry Actions";
            }

            return s;
        }
    }
}
