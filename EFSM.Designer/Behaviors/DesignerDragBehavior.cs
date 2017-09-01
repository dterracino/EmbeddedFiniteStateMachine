using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.Behaviors
{
    public class DesignerDragBehavior : DraggableBehaviorBase
    {
        public static readonly DependencyProperty SelectionServiceProperty =
            DependencyProperty.Register("SelectionService", typeof(ISelectionService), typeof(DesignerDragBehavior), new FrameworkPropertyMetadata(PropertyChanged));

        public static readonly DependencyProperty UndoProviderProperty =
            DependencyProperty.Register("UndoProvider", typeof(IUndoProvider), typeof(DesignerDragBehavior), new FrameworkPropertyMetadata(PropertyChanged));

        private IMoveable[] _moveables;

        private static void PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as DesignerDragBehavior)?.Reset();
        }

        public ISelectionService SelectionService
        {
            get { return (ISelectionService)GetValue(SelectionServiceProperty); }
            set { SetValue(SelectionServiceProperty, value); }
        }

        public IUndoProvider UndoProvider
        {
            get { return (IUndoProvider)GetValue(UndoProviderProperty); }
            set { SetValue(UndoProviderProperty, value); }
        }

        private ISelectable Selectable
        {
            get { return AssociatedObject.DataContext as ISelectable; }
        }

        protected override Point GetPositionFromMouse(MouseEventArgs e)
        {
            var root = GraphicsUtil.FindVisualParent<FrameworkElement>(AssociatedObject, "DesignRoot");

            if (root == null)
                return e.GetPosition(null);

            return e.GetPosition(root);
        }

        private void ApplyToMoveables(Action<IMoveable> action)
        {
            var moveables = _moveables;

            if (moveables == null)
                return;

            foreach (var moveable in moveables)
            {
                action(moveable);
            }
        }

        protected override void StartDrag(Point position)
        {
            SelectionService?.EnsureSelected(Selectable);

            _moveables = SelectionService?.GetSelected()
                 .OfType<IMoveable>()
                 .ToArray();

            ApplyToMoveables(m => m.StartMove());
        }

        protected override void ContinueDrag(Point position)
        {
            ApplyToMoveables(m => m.ContinueMove(position - StartPosition));
        }

        protected override void FinishDrag(Point position)
        {
            ApplyToMoveables(m => m.CompleteMove(position - StartPosition));

            UndoProvider?.SaveUndoState();
        }

        protected override void CancelDrag()
        {
            ApplyToMoveables(m => m.CancelMove());
        }

        protected override void Clicked(Point position)
        {
            SelectionService?.SelectWithKeyboardModifiers(Selectable);
        }

        protected override void Reset()
        {
            base.Reset();

            _moveables = null;

        }
    }
}
