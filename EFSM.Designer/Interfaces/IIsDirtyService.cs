using System.ComponentModel;

namespace EFSM.Designer.Interfaces
{
    public interface IIsDirtyService : INotifyPropertyChanged
    {
        bool IsDirty { get; }

        void MarkClean();

        void MarkDirty();
    }
}
