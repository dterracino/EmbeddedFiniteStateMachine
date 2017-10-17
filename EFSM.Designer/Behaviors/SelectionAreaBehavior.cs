using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
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
            var parent = GraphicsUtil.FindVisualParent<FrameworkElement>(AssociatedObject, "DesignRoot");

            return e.GetPosition(AssociatedObject);
        }

        protected override void StartDrag(Point position)
        {
            _startPosition = position;

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
