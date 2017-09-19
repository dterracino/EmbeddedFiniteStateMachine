using EFSM.Designer.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EFSM.Domain;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class CriteriaTransitionViewModel : ViewModelBase
    {
        private TransitionEditorViewModel _owner;
        private ConditionViewModelBase _selectedCondition;
        private ConditionViewModelBase _rootCondition;

        public ICommand SetSimpleRootConditionCommand { get; private set; }
        public ICommand SetCompoundRootConditionCommand { get; private set; }
        public ICommand DeleteConditionCommand { get; private set; }

        public CriteriaTransitionViewModel(TransitionEditorViewModel owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            RootCondition = _owner.Transition.Condition.GetModel().ToViewModel(owner);
            CommandsInitialization();
        }

        public ConditionViewModelBase SelectedCondition
        {
            get { return _selectedCondition; }
            set
            {
                _selectedCondition = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<StateMachineInputViewModel> Inputs => _owner.Inputs;

        public ConditionViewModelBase RootCondition
        {
            get { return _rootCondition; }
            set
            {
                _rootCondition = value;
                RaisePropertyChanged();
                _owner.DirtyService.MarkDirty();
            }
        }

        private void CommandsInitialization()
        {
            SetSimpleRootConditionCommand = new RelayCommand(SetSimpleRootCondition, CanSetSimpleRootCondition);
            SetCompoundRootConditionCommand = new RelayCommand(SetCompoundRootCondition, CanSetCompoundRootCondition);
            DeleteConditionCommand = new RelayCommand(DeleteCondition, CanDeleteCondition);
        }

        private void SetCompoundRootCondition()
        {
            RootCondition = new CompoundConditionViewModel(_owner);
        }

        private bool CanSetCompoundRootCondition() => RootCondition == null;

        private void SetSimpleRootCondition()
        {
            RootCondition = new SimpleConditionViewModel(_owner) { SourceInputId = _owner.Inputs[0].Id };
        }

        private bool CanSetSimpleRootCondition() => _owner.Inputs.Count > 0 && RootCondition == null;

        private void DeleteCondition()
        {
            if (SelectedCondition == RootCondition)
            {
                RootCondition = null;
                return;
            }

            if (SelectedCondition.ParentCollection == null)
            {
                //This truly shouldn't happen
                return;
            }

            //Remove the item from its parent collection
            SelectedCondition.ParentCollection.Remove(SelectedCondition);

            //Mark it dirty
            _owner.DirtyService.MarkDirty();
        }

        private bool CanDeleteCondition() => SelectedCondition != null;

        internal StateMachineCondition GetModel()
        {
            return RootCondition?.GetModel();
        }
    }
}
