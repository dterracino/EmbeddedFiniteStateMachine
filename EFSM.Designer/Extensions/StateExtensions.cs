using EFSM.Designer.ViewModel;
using System.Windows.Media;

namespace EFSM.Designer.Extensions
{
    public static class StateExtensions
    {
        public static Geometry GetStateGeometry(this StateViewModel state)
        {
            return new EllipseGeometry(state.Location, state.Width / 2, state.Height / 2);
        }
    }
}
