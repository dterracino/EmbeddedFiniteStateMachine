using Cas.Common.WPF.Interfaces;
using GalaSoft.MvvmLight;
using System;
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

            AddConditionCommand = new RelayCommand(AddCondition, CanAddCondition);
        }

        public ICommand AddConditionCommand { get; }

        private void AddCondition()
        {
            if (!CanAddCondition())
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

        private bool CanAddCondition()
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

        private void Fix()
        {
            switch (ConditionType)
            {
                case ConditionType.Input:
                    Conditions.Clear();
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

        //public string DisplayText
        //{
        //    get
        //    {
        //        switch (_model.ConditionType)
        //        {
        //            case ConditionType.Input:
        //                return "Input";

        //            case ConditionType.Or:
        //                return "Or";

        //            case ConditionType.And:
        //                return "And";

        //            case ConditionType.Not:
        //                return "Not";

        //            default:
        //                return _model.ConditionType.ToString();
        //        }
        //    }
        //}

        public ConditionType ConditionType
        {
            get => _model.ConditionType;
            set
            {
                _model.ConditionType = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => CanSelectInput);
                Fix();
            }
        }

        public Guid? InputId
        {
            get { return _model.SourceInputId; }
            set
            {
                _model.SourceInputId = value;
                RaisePropertyChanged();
            }
        }

        public ConditionsViewModel ParentCollection { get; internal set; }

        public ConditionsViewModel Conditions { get; }

        public IDirtyService DirtyService => _owner.DirtyService;

        public TransitionEditorViewModel Owner => _owner;

        internal StateMachineCondition GetModel()
        {
            

            return _model.Clone();
        }
    }
}
