using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.View;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EFSM.Designer.Common
{
    public class MarkdownGenerator
    {
        private List<Guid> _conditionGuids = new List<Guid>();

        private List<StateMachine> _stateMachines;

        public void Generate(List<StateMachine> stateMachines, string mdFullPath)
        {
            _stateMachines = stateMachines;

            string s = string.Empty;

            for (int i = 0; i < stateMachines.Count; i++)
            {
                var stateMachine = stateMachines[i];

                s += AddStateMachine(stateMachine);

                string pngPath = mdFullPath.Remove(mdFullPath.LastIndexOf(@"\")) + $@"\StateMachine_{i}.png";

                SaveImage(stateMachine, pngPath);

                s += AddImage(pngPath);
            }

            File.WriteAllText(mdFullPath, s);
        }

        private void SaveImage(StateMachine stateMachine, string pngPath)
        {
            var stateMachineViewModel = new StateMachineViewModel(stateMachine, ApplicationContainer.Container.Resolve<IViewService>(), null, null, true);
            UserControl control = new StateMachineView() { Background = Brushes.White };
            control.DataContext = stateMachineViewModel;

            int size = 800;

            control.Measure(new Size(size, size));
            control.Arrange(new Rect(new Size(size, size)));
            control.UpdateLayout();

            RenderTargetBitmap bmp = new RenderTargetBitmap(size, size, 96, 96, PixelFormats.Pbgra32);

            bmp.Render(control);

            var encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stm = File.Create(pngPath))
            {
                encoder.Save(stm);
            }
        }

        private string AddStateMachine(StateMachine stateMachine)
        {
            var s = $"## State Machine Name: {stateMachine.Name}\n";

            s += AddStatesList(stateMachine.States);

            s += AddInfoForEachState(stateMachine.States, stateMachine.Actions);

            s += AddTransitionsList(stateMachine.Transitions);

            s += AddInfoForEachTransition(stateMachine.Transitions, stateMachine.Actions);

            return s;
        }

        private string AddInfoForEachState(State[] states, StateMachineOutputAction[] actions)
        {
            string s = string.Empty;

            if (states != null || states.Any())
            {
                foreach (var item in states)
                {
                    s += $"#### State: {item.Name}";
                    s += Environment.NewLine;
                    s += AddEntryFunctions(item, actions);
                    s += Environment.NewLine;
                    s += AddExitFunctions(item, actions);
                    s += Environment.NewLine;
                }
            }

            return s;
        }

        private string AddInfoForEachTransition(StateMachineTransition[] transitions, StateMachineOutputAction[] actions)
        {
            string s = string.Empty;

            if (transitions != null)
            {
                foreach (var item in transitions)
                {
                    s += $"#### Transition: {item.Name}";
                    s += Environment.NewLine;
                    s += AddOutputActions(item, actions);
                    s += Environment.NewLine;
                    s += AddConditions(item);
                    s += Environment.NewLine;
                }
            }

            return s;
        }

        private string AddOutputActions(StateMachineTransition transition, StateMachineOutputAction[] actions)
        {
            var s = $"##### No Output Actions";

            if (transition.TransitionActions != null && transition.TransitionActions.Any())
            {
                s = $"##### Output Actions";
                s += Environment.NewLine;

                foreach (var item in transition.TransitionActions)
                {
                    s += $"* {(actions.First(x => x.Id == item).Name)}";
                }
            }

            return s;
        }

        private string AddConditions(StateMachineTransition transition)
        {
            var s = $"##### No Condition";

            if (transition.Condition != null)
            {
                s = $"##### Condition";
                s += Environment.NewLine;

                // s += AddCondition(transition.Condition);
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

        private string AddCondition(StateMachineCondition condition)
        {
            string s = string.Empty;

            if (condition.ConditionType == ConditionType.Not)
            {
                if (condition.SourceInputId != null)
                {
                    if (!_conditionGuids.Contains(condition.SourceInputId.Value))
                    {
                        _conditionGuids.Add(condition.SourceInputId.Value);
                    }

                    s += $"{condition.SourceInputId.Value}== {condition.SourceInputId}";
                }
            }
            else
            {
                s += " ( ";
                string operation = condition.ConditionType == ConditionType.And ? " AND " : " OR ";

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

        private string AddTransitionsList(StateMachineTransition[] transitions)
        {
            string s = $"### Transitions:";
            s += Environment.NewLine;

            if (transitions == null || !transitions.Any())
            {

            }
            else
            {
                foreach (var transition in transitions)
                {
                    s += $"* {transition.Name}\n";
                }
            }

            return s;
        }

        private string AddStatesList(State[] states)
        {
            string s = $"### States:";
            s += Environment.NewLine;

            if (states == null || !states.Any())
            {
                s += "No states";
            }
            else
            {
                foreach (var item in states)
                {
                    s += $"* {item.Name}\n";
                }
            }

            return s;
        }

        private string AddImage(string imagePath)
        {
            string saving = imagePath.Remove(0, imagePath.IndexOf(@":\") + 2);
            saving = saving.Replace(" ", "%20");
            return $"![image](/{saving})";
        }

        private string AddExitFunctions(State state, StateMachineOutputAction[] actions)
        {
            string s = string.Empty;

            if (state.ExitActions != null && state.ExitActions.Any())
            {
                s = $"##### Exit Actions";
                s += Environment.NewLine;

                foreach (var item in state.ExitActions)
                {
                    s += $"* {(actions.First(x => x.Id == item).Name)}\n";
                }
            }
            else
            {
                s = $"##### No Exit Actions";
            }

            return s;
        }

        private string AddEntryFunctions(State state, StateMachineOutputAction[] actions)
        {
            string s = string.Empty;

            if (state.EntryActions != null && state.EntryActions.Any())
            {
                s = $"##### Entry Actions";
                s += Environment.NewLine;

                foreach (var item in state.EntryActions)
                {
                    s += $"* {(actions.First(x => x.Id == item).Name)}\n";
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
