using Cas.Common.WPF.Interfaces;
using EFSM.Domain;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class SimulationStateMachineViewModel : StateMachineViewModel
    {
        public SimulationStateMachineViewModel(StateMachine model, IViewService viewService, IMessageBoxService messageBoxService) : 
            base(model, viewService, null, null, messageBoxService, true)
        {
            this.IsPointerMode = false;
        }

        public override void OnDrop(DragEventArgs e)
        {
        }

        public override void OnPreviewMouseLeftButtonUp(MouseEventArgs e)
        {
        }
    }
}
