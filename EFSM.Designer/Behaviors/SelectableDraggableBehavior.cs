using System.Windows;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;

namespace EFSM.Designer.Behaviors
{
    public class SelectableDraggableBehavior : SimpleMoveableBehavior
    {
        public static readonly DependencyProperty SelectionServiceProperty =
            DependencyProperty.Register("SelectionService", typeof(ISelectionService), typeof(SelectableDraggableBehavior));

        public ISelectionService SelectionService
        {
            get { return (ISelectionService)GetValue(SelectionServiceProperty); }
            set { SetValue(SelectionServiceProperty, value); }
        }

        private ISelectable Selectable
        {
            get { return AssociatedObject.DataContext as ISelectable; }
        }

        protected override void Clicked(Point position)
        {
            SelectionService.SelectWithKeyboardModifiers(Selectable);
        }
    }
}