using Cas.Common.WPF.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Windows.Input;
using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight.CommandWpf;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class ConditionViewModel : ViewModelBase
    {
        private readonly StateMachineCondition _model;
        private readonly TransitionEditorViewModel _owner;

        public ConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner)
        {
            _model = model;
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));

            Conditions  = new ConditionsViewModel(this);

            if (model.Conditions != null)
            {
                foreach (var childCondition in model.Conditions)
                {
                    Conditions.Add(new ConditionViewModel(childCondition, owner));
                }
            }

            Fix();

            AddConditionCommand = new RelayCommand(AddCondition, () => CanAddCondition);
            DeleteCommand = new RelayCommand(Delete);
        }

        public ICommand AddConditionCommand { get; }
        public ICommand DeleteCommand { get; }

        private void AddCondition()
        {
            if (!CanAddCondition)
                return;

            var model = new StateMachineCondition()
            {
                ConditionType = ConditionType.Input
            };

            var condition = new ConditionViewModel(model, _owner);

            Conditions.Add(condition);
        }

        public bool CanSelectInput
        {
            get { return ConditionType == ConditionType.Input; }
        }

        public bool CanAddCondition
        {
            get
            {
                switch (_model.ConditionType)
                {
                    case ConditionType.Or:
                    case ConditionType.And:
                        return true;

                    case ConditionType.Not:

                        return Conditions.Count == 0;

                    default:
                        return false;
                }
            }
        }

        private void Delete()
        {
            if (ParentCollection?.Parent == null)
            {
                _owner.Criteria.RootCondition = null;
            }
            else
            {
                ParentCollection.Remove(this);
            }
        }

        /// <summary>
        /// Ensures that the number of sub conditions / the input is correct given its 
        /// </summary>
        private void Fix()
        {
            switch (ConditionType)
            {
                case ConditionType.Input:
                    Conditions.Clear();

                    if (InputId == null)
                    {
                        //Select the first item
                        InputId = _owner.Inputs
                            .FirstOrDefault()?.Id;
                    }

                    break;

                case ConditionType.Or:
                case ConditionType.And:
                    InputId = null;
                    break;

                case ConditionType.Not:

                    InputId = null;

                    while (Conditions.Count > 1)
                    {
                        Conditions.RemoveAt(Conditions.Count - 1);
                    }

                    break;

                default:
                    throw new NotSupportedException($"Unexpected value '{ConditionType}'.");
            }
        }

        public ConditionType ConditionType
        {
            get => _model.ConditionType;
            set
            {
                _model.ConditionType = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => CanSelectInput);
                RaisePropertyChanged(() => CanAddCondition);
                Fix();
                _owner.DirtyService.MarkDirty();
            }
        }

        public Guid? InputId
        {
            get { return _model.SourceInputId; }
            set
            {
                _model.SourceInputId = value;
                RaisePropertyChanged();
                _owner.DirtyService.MarkDirty();
            }
        }

        public ConditionsViewModel ParentCollection { get; internal set; }

        public ConditionsViewModel Conditions { get; }

        public IDirtyService DirtyService => _owner.DirtyService;

        internal StateMachineCondition GetModel()
        {
            Fix();

            _model.Conditions = Conditions
                .Select(c => c.GetModel())
                .ToList();

            return _model.Clone();
        }
    }
}
