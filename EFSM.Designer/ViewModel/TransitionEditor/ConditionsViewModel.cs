using System;
using System.Collections.ObjectModel;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class ConditionsViewModel : ObservableCollection<ConditionViewModelBase>
    {
        private readonly ConditionViewModelBase _parent;

        public ConditionsViewModel(ConditionViewModelBase parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        protected override void InsertItem(int index, ConditionViewModelBase item)
        {
            item.ParentCollection = this;

            base.InsertItem(index, item);

            Parent.DirtyService.MarkDirty();
        }

        protected override void SetItem(int index, ConditionViewModelBase item)
        {
            item.ParentCollection = this;

            base.SetItem(index, item);

            Parent.DirtyService.MarkDirty();
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];

            item.ParentCollection = null;

            base.RemoveItem(index);

            Parent.DirtyService.MarkDirty();
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            Parent.DirtyService.MarkDirty();
        }

        internal ConditionViewModelBase Parent => _parent;
    }
}
