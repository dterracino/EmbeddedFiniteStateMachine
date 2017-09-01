using EFSM.Designer.ViewModel;
using System.Windows;

namespace EFSM.Designer.Interfaces
{
    public interface ITransitionCreator
    {
        void StartCreatingTransition(StateViewModel source);

        void ContinueCreatingTransition(Point point);

        void FinishCreatingTransition(Point point);

        void CancelCreatingTransition();
    }
}
