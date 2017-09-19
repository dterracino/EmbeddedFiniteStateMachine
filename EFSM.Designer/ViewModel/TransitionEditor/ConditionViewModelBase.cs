using Cas.Common.WPF.Interfaces;
using GalaSoft.MvvmLight;
using System;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public abstract class ConditionViewModelBase : ViewModelBase
    {
        private readonly TransitionEditorViewModel _owner;

        protected ConditionViewModelBase(TransitionEditorViewModel owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        public ConditionsViewModel ParentCollection { get; internal set; }

        public IDirtyService DirtyService => _owner.DirtyService;

        public TransitionEditorViewModel Owner => _owner;
    }
}
