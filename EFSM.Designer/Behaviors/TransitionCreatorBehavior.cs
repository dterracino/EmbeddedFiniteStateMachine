using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Designer.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace EFSM.Designer.Behaviors
{
    public class TransitionCreatorBehavior : Behavior<FrameworkElement>
    {
        private BehaviorState _state;
        private Point _startingPosition;

        public static readonly DependencyProperty TransitionCreatorProperty =
            DependencyProperty.Register("TransitionCreator", typeof(ITransitionCreator), typeof(TransitionCreatorBehavior), new FrameworkPropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TransitionCreatorBehavior), new FrameworkPropertyMetadata(true, PropertyChanged));

        private enum BehaviorState
        {
            Idle,

            MouseDown,

            Dragging
        }

        public ITransitionCreator TransitionCreator
        {
            get { return (ITransitionCreator)GetValue(TransitionCreatorProperty); }
            set { SetValue(TransitionCreatorProperty, value); }
        }

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        private static void PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var manager = obj as TransitionCreatorBehavior;

            manager?.Reset();
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            AssociatedObject.MouseMove += Element_MouseMove;
            AssociatedObject.MouseLeftButtonUp += ElementOnMouseLeftButtonUp;

            Reset();

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
            AssociatedObject.MouseMove -= Element_MouseMove;
            AssociatedObject.MouseLeftButtonUp -= ElementOnMouseLeftButtonUp;

            Reset();
        }

        private Point GetMousePosition(MouseEventArgs e)
        {
            //This is pretty 
            var parent = GraphicsUtil.FindVisualParent<Canvas>(AssociatedObject);

            //Get the position relative to the layout host
            var position = e.GetPosition(parent);

            return position;
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabled)
                return;

            if (_state == BehaviorState.Idle)
            {
                _startingPosition = GetMousePosition(e);
                _state = BehaviorState.MouseDown;

                AssociatedObject.Focus();
                AssociatedObject.CaptureMouse();

                e.Handled = true;
            }
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsEnabled)
                return;

            var position = new Lazy<Point>(() => GetMousePosition(e));

            if (_state == BehaviorState.MouseDown)
            {
                //Check to see if we're out of the dragging min 
                //http://stackoverflow.com/questions/2068106/wpf-drag-distance-threshold

                var distanceMoved = _startingPosition - position.Value;

                if (Math.Abs(distanceMoved.X) >= SystemParameters.MinimumHorizontalDragDistance &&
                    Math.Abs(distanceMoved.Y) >= SystemParameters.MinimumVerticalDragDistance)
                {
                    //Focus
                    AssociatedObject.Focus();

                    e.Handled = true;

                    var sourceState = AssociatedObject.DataContext as StateViewModel;

                    if (sourceState != null)
                    {
                        TransitionCreator?.StartCreatingTransition(sourceState);

                        _state = BehaviorState.Dragging;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
            else if (_state == BehaviorState.Dragging)
            {
                //Drag
                TransitionCreator?.ContinueCreatingTransition(position.Value);

                e.Handled = true;
            }
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsEnabled)
                return;

            var position = new Lazy<Point>(() => _startingPosition = GetMousePosition(e));

            switch (_state)
            {
                case BehaviorState.Dragging:

                    TransitionCreator?.FinishCreatingTransition(position.Value);

                    break;
            }

            e.Handled = true;

            Reset();
        }

        private void Reset()
        {
            //Release the mouse capture
            AssociatedObject?.ReleaseMouseCapture();

            _state = BehaviorState.Idle;
        }
    }
}
