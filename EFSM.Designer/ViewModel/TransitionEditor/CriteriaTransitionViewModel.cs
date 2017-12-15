using Autofac;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Extensions;
using EFSM.Designer.ViewModel.TransitionEditor.Conditions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class CriteriaTransitionViewModel : ViewModelBase
    {
        private readonly TransitionEditorViewModel _owner;
        private ConditionViewModel _rootCondition;
        private string _addConditionToolTip;
        private const string DefaultToolTip = "Add Condition";
        private const string NoInputsToolTip = "It is needed to add input first";
        private IMessageBoxService _messageBoxService;

        public CriteriaTransitionViewModel(TransitionEditorViewModel owner, IMessageBoxService messageBoxService)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            RootCondition = _owner.Transition.Condition?.GetModel().ToViewModel(owner);

            AddConditionCommand = new RelayCommand(AddCondition, CanAddCondition);
            AddConditionToolTip = DefaultToolTip;
        }

        public ICommand AddConditionCommand { get; }

        public ObservableCollection<StateMachineInputViewModel> Inputs => _owner.Inputs;

        public ConditionViewModel RootCondition
        {
            get { return _rootCondition; }
            set
            {
                if (value != _rootCondition)
                {
                    _rootCondition = value;
                    RaisePropertyChanged();
                    _owner.DirtyService.MarkDirty();
                }
            }
        }

        public string AddConditionToolTip
        {
            get { return _addConditionToolTip; }
            set
            {
                if (_addConditionToolTip != value)
                {
                    _addConditionToolTip = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void AddCondition()
        {
            try
            {
                RootCondition = new ConditionViewModel(
                    new StateMachineCondition(),
                    _owner,
                    ApplicationContainer.Container.Resolve<ConditionEditServiceManager>(),
                    _messageBoxService);
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private bool CanAddCondition()
        {
            AddConditionToolTip = _owner.Inputs.Any() ? DefaultToolTip : NoInputsToolTip;
            return RootCondition == null && _owner.Inputs.Any();
        }

        internal StateMachineCondition GetModel()
        {
            return RootCondition?.GetModel();
        }
    }
}
