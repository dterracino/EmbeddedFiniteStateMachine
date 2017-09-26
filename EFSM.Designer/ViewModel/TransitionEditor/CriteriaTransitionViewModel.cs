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
        private readonly TransitionEditorViewModel _owner;
        private ConditionViewModel _selectedCondition;
        private ConditionViewModel _rootCondition;

        public CriteriaTransitionViewModel(TransitionEditorViewModel owner)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            RootCondition = _owner.Transition.Condition.GetModel().ToViewModel(owner);

            AddConditionCommand = new RelayCommand(AddCondition, CanAddCondition);
        }

        public ICommand AddConditionCommand { get; }

        public ObservableCollection<StateMachineInputViewModel> Inputs => _owner.Inputs;

        public ConditionViewModel RootCondition
        {
            get { return _rootCondition; }
            set
            {
                _rootCondition = value;
                RaisePropertyChanged();
                _owner.DirtyService.MarkDirty();
            }
        }

        private void AddCondition()
        {
            RootCondition = new ConditionViewModel(new StateMachineCondition(), _owner);
        }

        private bool CanAddCondition() => RootCondition == null;

       
        internal StateMachineCondition GetModel()
        {
            return RootCondition?.GetModel();
        }
    }
}
