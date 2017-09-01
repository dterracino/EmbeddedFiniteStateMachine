using EFSM.Designer.ViewModel;
using System.ComponentModel;

namespace EFSM.Designer.Model
{
    [DisplayName("State")]
    public class StatePropertyGridSource : INotifyPropertyChanged
    {
        private readonly StateViewModel _state;

        public StatePropertyGridSource(StateViewModel state)
        {
            _state = state;

            state.PropertyChanged += State_PropertyChanged;

        }

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        [DisplayName("Name")]
        [Description("The name of the state.")]
        public string Name
        {
            get { return _state.Name; }
            set { _state.Name = value; }
        }

        //[Category("Layout")]
        //[Description("X coordinate of the item within its container.")]
        //public double X
        //{
        //    get { return _state.Location.X; }
        //    set { _state.Location.X = value; }
        //}

        //[Category("Layout")]
        //[Description("X coordinate of the item within its container.")]
        //public double Y
        //{
        //    get { return _state.Y; }
        //    set { _state.Y = value; }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
