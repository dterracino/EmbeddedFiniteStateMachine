using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;
using System;
using Cas.Common.WPF.Interfaces;

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

        public IDirtyService DirtyService
        {
            get { return _owner.DirtyService; }
        }

        public TransitionEditorViewModel Owner
        {
            get { return _owner; }
        }
    }
}
