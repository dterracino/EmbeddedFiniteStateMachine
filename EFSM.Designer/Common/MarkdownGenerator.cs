using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.View;
using EFSM.Designer.ViewModel;
using EFSM.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly List<Guid> _conditionGuids = new List<Guid>();

        public void Generate(List<StateMachine> stateMachines, string mdFullPath)
        {
            string s = string.Empty;

            for (int i = 0; i < stateMachines.Count; i++)
            {
                var stateMachine = stateMachines[i];

                s += AddStateMachine(stateMachine);

                string pngPath = mdFullPath.Remove(mdFullPath.LastIndexOf(@"\")) + $@"\StateMachine_{i}.png";

                SaveImage(stateMachine, pngPath);

                s += AddImage(pngPath);

                s += Environment.NewLine;
            }

            File.WriteAllText(mdFullPath, s);
        }

        private void SaveImage(StateMachine stateMachine, string pngPath)
        {
            var stateMachineViewModel = new StateMachineViewModel(stateMachine, ApplicationContainer.Container.Resolve<IViewService>(), null, null, true);

            //Render using this approach
            // https://stackoverflow.com/a/5189179/232566
            StateMachineView control = new StateMachineView
            {
                DataContext = stateMachineViewModel
            };
            
            const int temporarySize = 800;

            control.Measure(new System.Windows.Size(temporarySize, temporarySize));
            control.Arrange(new Rect(new System.Windows.Size(temporarySize, temporarySize)));
            control.UpdateLayout();

            //Calculate the bounds of the state machine
            var bounds = VisualTreeHelper.GetDescendantBounds(control.RenderRoot);

            int width = (int)bounds.Width;
            int height = (int)bounds.Height;

            //There are errors if these are 0.
            if (width > 0 && height > 0)
            {
                //Got the render transform idea here:
                // https://stackoverflow.com/a/224492/232566
                DrawingVisual visual = new DrawingVisual();

                using (DrawingContext context = visual.RenderOpen())
                {
                    //Create a brush that references the visual representation of the state machine
                    VisualBrush brush = new VisualBrush(control.RenderRoot);

                    //Fill the visual with our background
                    context.DrawRectangle(System.Windows.Media.Brushes.White, null, bounds);

                    //Draw the system onto the visual
                    context.DrawRectangle(brush,
                        null,
                        bounds);
                }

                //Apply a transform that positions the system near (0, 0)
                visual.Transform = new TranslateTransform(bounds.X * -1, bounds.Y * -1);

                //Create the render target
                RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

                //Render it
                bmp.Render(visual);

                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder2 = new BmpBitmapEncoder();

                    encoder2.Frames.Add(BitmapFrame.Create(bmp));
                    encoder2.Save(stream);

                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        //Save it
                        bitmap.Save(pngPath);
                    }
                };
            }
        }

        private string AddStateMachine(StateMachine stateMachine)
        {
            var s = $"## State Machine Name: {stateMachine.Name}\n";

            s += AddStatesList(stateMachine.States);

            s += AddInfoForEachState(stateMachine.States, stateMachine.Actions);

            s += AddTransitionsList(stateMachine.Transitions);

            s += AddInfoForEachTransition(stateMachine.Transitions, stateMachine.Actions, stateMachine.Inputs);

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

        private string AddInfoForEachTransition(StateMachineTransition[] transitions, StateMachineOutputAction[] actions, StateMachineInput[] inputs)
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
                    s += AddConditions(item, inputs);
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

        private string AddConditions(StateMachineTransition transition, StateMachineInput[] inputs)
        {
            var s = $"##### No Condition";

            if (transition.Condition != null)
            {
                s = $"##### Condition";
                s += Environment.NewLine;

                s += AddCondition(transition.Condition, inputs);
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

        private string AddCondition(StateMachineCondition condition, StateMachineInput[] inputs)
        {
            string s = string.Empty;

            if ((condition.ConditionType == ConditionType.Input || condition.ConditionType == ConditionType.Not)
                && inputs != null)
            {
                Guid conditionGuid = condition.ConditionType == ConditionType.Input ? condition.SourceInputId.Value : condition.Conditions[0].SourceInputId.Value;
                StateMachineInput input = inputs.FirstOrDefault(i => i.Id == conditionGuid);

                if (input != null)
                {
                    string name = input.Name;

                    if (!_conditionGuids.Contains(conditionGuid))
                    {
                        _conditionGuids.Add(conditionGuid);
                    }

                    string equalityOperator = condition.ConditionType == ConditionType.Input ? string.Empty : "!";
                    s += $" {equalityOperator}{name}";
                }
            }
            else if (condition.ConditionType == ConditionType.And || condition.ConditionType == ConditionType.Or)
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

                    s += AddCondition(item, inputs);
                }

                //foreach (var item in condition.Conditions)
                //{
                //    s += AddCondition(item, inputs);
                //}

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
