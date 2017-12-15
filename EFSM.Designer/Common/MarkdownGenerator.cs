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
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EFSM.Designer.Common
{
    public class MarkdownGenerator
    {
        public void Generate(List<StateMachine> stateMachines, string mdFullPath)
        {
            StringBuilder textFile = new StringBuilder();

            for (int i = 0; i < stateMachines.Count; i++)
            {
                var stateMachine = stateMachines[i];

                textFile.Append(AddStateMachine(stateMachine));

                string pngPath = mdFullPath.Remove(mdFullPath.LastIndexOf(@"\")) + $@"\StateMachine_{i}.png";

                SaveImage(stateMachine, pngPath);

                textFile.Append(AddImage(pngPath));

                textFile.Append(Environment.NewLine);
            }

            File.WriteAllText(mdFullPath, textFile.ToString());
        }

        private void SaveImage(StateMachine stateMachine, string pngPath)
        {
            var stateMachineViewModel = new StateMachineViewModel(
                stateMachine,
                ApplicationContainer.Container.Resolve<IViewService>(),
                null,
                null,
                ApplicationContainer.Container.Resolve<IMessageBoxService>(),
                true);

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

        private StringBuilder AddStateMachine(StateMachine stateMachine)
        {
            StringBuilder s = new StringBuilder($"## State Machine Name: {stateMachine.Name}\n");

            s.Append(AddStatesList(stateMachine.States));

            s.Append(AddInfoForEachState(stateMachine.States, stateMachine.Actions));

            s.Append(AddTransitionsList(stateMachine.Transitions));

            s.Append(AddInfoForEachTransition(stateMachine.Transitions, stateMachine.Actions, stateMachine.Inputs));

            return s;
        }

        private StringBuilder AddInfoForEachState(State[] states, StateMachineOutputAction[] actions)
        {
            StringBuilder s = new StringBuilder();

            if (states != null || states.Any())
            {
                foreach (var item in states)
                {
                    s.Append($"#### State: {item.Name}");
                    s.Append(Environment.NewLine);
                    s.Append(AddEntryFunctions(item, actions));
                    s.Append(Environment.NewLine);
                    s.Append(AddExitFunctions(item, actions));
                    s.Append(Environment.NewLine);
                }
            }

            return s;
        }

        private StringBuilder AddInfoForEachTransition(StateMachineTransition[] transitions, StateMachineOutputAction[] actions, StateMachineInput[] inputs)
        {
            StringBuilder s = new StringBuilder();

            if (transitions != null)
            {
                foreach (var item in transitions)
                {
                    s.Append($"#### Transition: {item.Name}");
                    s.Append(Environment.NewLine);
                    s.Append(AddOutputActions(item, actions));
                    s.Append(Environment.NewLine);

                    string conditionHeadline = item.Condition == null ? "##### No Condition" : "##### Condition" + Environment.NewLine;
                    s.Append(conditionHeadline);

                    s.Append(new MarkdownConditionGenerator().Generate(item, inputs));
                    s.Append(Environment.NewLine);
                }
            }

            return s;
        }

        private StringBuilder AddOutputActions(StateMachineTransition transition, StateMachineOutputAction[] actions)
        {
            StringBuilder s = new StringBuilder($"##### No Output Actions");

            if (transition.TransitionActions != null && transition.TransitionActions.Any())
            {
                s = new StringBuilder($"##### Output Actions");
                s.Append(Environment.NewLine);

                foreach (var item in transition.TransitionActions)
                {
                    s.Append($"* {(actions.First(x => x.Id == item).Name)}");
                }
            }

            return s;
        }

        private StringBuilder AddTransitionsList(StateMachineTransition[] transitions)
        {
            StringBuilder s = new StringBuilder($"### Transitions:");
            s.Append(Environment.NewLine);

            if (transitions == null || !transitions.Any())
            {

            }
            else
            {
                foreach (var transition in transitions)
                {
                    s.Append($"* {transition.Name}\n");
                }
            }

            return s;
        }

        private StringBuilder AddStatesList(State[] states)
        {
            StringBuilder s = new StringBuilder($"### States:");
            s.Append(Environment.NewLine);

            if (states == null || !states.Any())
            {
                s.Append("No states");
            }
            else
            {
                foreach (var item in states)
                {
                    s.Append($"* {item.Name}\n");
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

        private StringBuilder AddExitFunctions(State state, StateMachineOutputAction[] actions)
        {
            StringBuilder s = new StringBuilder();

            if (state.ExitActions != null && state.ExitActions.Any())
            {
                s = new StringBuilder($"##### Exit Actions");
                s.Append(Environment.NewLine);

                foreach (var item in state.ExitActions)
                {
                    s.Append($"* {(actions.First(x => x.Id == item).Name)}\n");
                }
            }
            else
            {
                s = new StringBuilder($"##### No Exit Actions");
            }

            return s;
        }

        private StringBuilder AddEntryFunctions(State state, StateMachineOutputAction[] actions)
        {
            StringBuilder s = new StringBuilder();

            if (state.EntryActions != null && state.EntryActions.Any())
            {
                s = new StringBuilder($"##### Entry Actions");
                s.Append(Environment.NewLine);

                foreach (var item in state.EntryActions)
                {
                    s.Append($"* {(actions.First(x => x.Id == item).Name)}\n");
                }
            }
            else
            {
                s = new StringBuilder($"##### No Entry Actions");
            }

            return s;
        }
    }
}
