using EFSM.Designer.ViewModel;
using System.Linq;
using System.Windows;

namespace EFSM.Designer.Behaviors
{
    public class StateDragBehavior : DesignerDragBehavior
    {
        public StateDragBehavior()
        {

        }

        protected override void StartDrag(Point position)
        {
            //Ensure that the connections are also selected.
            var component = AssociatedObject.DataContext as StateViewModel;

            if (component != null)
            {
                //Get the selected components
                var selectedComponents = component.Parent.SelectionService.GetSelected()
                    .OfType<StateViewModel>()
                    .ToArray();

                //Get the connections
                //var connections = selectedComponents.GetCommonConnections();

                foreach (var connection in selectedComponents)
                {
                    component.Parent.SelectionService.Select(connection);
                }

            }

            base.StartDrag(position);
        }
    }
}
