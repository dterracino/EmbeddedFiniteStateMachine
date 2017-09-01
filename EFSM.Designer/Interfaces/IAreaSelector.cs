using System.Windows;

namespace EFSM.Designer.Interfaces
{
    public interface IAreaSelector
    {
        void BeginBoxSelection(Rect rect);

        void ContinueBoxSelection(Rect rect);

        void CancelBoxSelection();

        void CompleteBoxSelection(Rect rect);
    }
}
