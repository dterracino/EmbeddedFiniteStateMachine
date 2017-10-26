using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.Behaviors
{
    public class SelectionAreaBehavior : DraggableBehaviorBase
    {
        public static readonly DependencyProperty AreaSelectorProperty
            = DependencyProperty.Register(nameof(AreaSelector), typeof(IAreaSelector), typeof(SelectionAreaBehavior));

        public IAreaSelector AreaSelector
        {
            get { return (IAreaSelector)GetValue(AreaSelectorProperty); }
            set { SetValue(AreaSelectorProperty, value); }
        }

        private Point _startPosition;

        protected override void Clicked(Point position)
        {

        }

        protected override Point GetPositionFromMouse(MouseEventArgs e)
        {
            return e.GetPosition(AssociatedObject);
        }

        protected override void StartDrag(Point position)
        {
            _startPosition = position;

            Console.WriteLine($"{position.X}; {position.Y}");

            AreaSelector?.BeginBoxSelection(new Rect(position, position));
        }

        protected override void ContinueDrag(Point position)
        {
            AreaSelector?.ContinueBoxSelection(new Rect(_startPosition, position));
        }

        protected override void CancelDrag()
        {
            AreaSelector?.CancelBoxSelection();
        }

        protected override void FinishDrag(Point position)
        {
            AreaSelector?.CompleteBoxSelection(new Rect(_startPosition, position));
        }
    }
}
