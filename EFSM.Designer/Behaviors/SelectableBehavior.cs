using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Extensions;
using EFSM.Designer.Interfaces;
using System.Windows;

namespace EFSM.Designer.Behaviors
{
    /// <summary>
    /// Simple bevhavior - just selects the clicked on item.
    /// </summary>
    public class SelectableBehavior : DraggableBehaviorBase
    {
        public static readonly DependencyProperty SelectionServiceProperty =
            DependencyProperty.Register("SelectionService", typeof(ISelectionService), typeof(SelectableBehavior));

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
