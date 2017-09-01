using EFSM.Designer.ViewModel;
using System;
using System.ComponentModel;

namespace EFSM.Designer.Model
{
    [DisplayName("State Machine")]
    public class StateMachinePropertyGridSource : INotifyPropertyChanged
    {
        private readonly StateMachineViewModel _stateMachine;

        public event PropertyChangedEventHandler PropertyChanged;

        public StateMachinePropertyGridSource(StateMachineViewModel stateMachine)
        {
            if (stateMachine == null) throw new ArgumentNullException(nameof(stateMachine));

            _stateMachine = stateMachine;

            _stateMachine.PropertyChanged += StateMachinePropertyChanged;
        }

        private void StateMachinePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        [Category("Layout")]
        [Description("Width of the design surface.")]
        public double Width
        {
            get { return _stateMachine.Width; }
            set { _stateMachine.Width = value; }
        }

        [Category("Layout")]
        [Description("Height of the design surface.")]
        public double Height
        {
            get { return _stateMachine.Height; }
            set { _stateMachine.Height = value; }
        }
    }
}
