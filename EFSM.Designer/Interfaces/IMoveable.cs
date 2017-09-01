using System.Windows;

namespace EFSM.Designer.Interfaces
{
    public interface IMoveable
    {
        void StartMove();

        void ContinueMove(Vector vector);

        void CancelMove();

        void CompleteMove(Vector vector);
    }
}
