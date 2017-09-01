using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.Common
{
    public class IsDirtyService : ViewModelBase, IIsDirtyService
    {
        private bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            private set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    RaisePropertyChanged();
                }
            }
        }

        public void MarkClean()
        {
            IsDirty = false;
        }

        public void MarkDirty()
        {
            IsDirty = true;
        }
    }
}
