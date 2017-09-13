using Cas.Common.WPF.Interfaces;
using EFSM.Domain;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class SimulationStateMachineViewModel : StateMachineViewModel
    {
        public SimulationStateMachineViewModel(StateMachine model, IViewService viewService) : base(model, viewService, null, null, true)
        {
            this.IsPointerMode = false;
        }

        public override void OnDrop(DragEventArgs e)
        {
        }

        public override void OnMouseLeftButtonDown(MouseEventArgs e)
        {
        }
    }
}
