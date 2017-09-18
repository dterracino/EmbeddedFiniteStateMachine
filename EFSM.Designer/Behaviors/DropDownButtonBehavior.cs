using System.Windows.Controls;
using System.Windows.Interactivity;

namespace EFSM.Designer.Behaviors
{
    public class DropDownButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AssociatedObject.ContextMenu == null)
                return;

            AssociatedObject.ContextMenu.IsEnabled = true;
            AssociatedObject.ContextMenu.PlacementTarget = AssociatedObject;
            AssociatedObject.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            AssociatedObject.ContextMenu.IsOpen = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }
    }
}
