using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public abstract class ConditionViewModel : ViewModelBase
    {
        public event EventHandler ConditionChanged;
        public event EventHandler ConditionTypeChanged;

        private bool _isValid = true;
        private readonly StateMachineCondition _model;
        protected readonly TransitionEditorViewModel _owner;

        public ConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner)
        {
            _model = model;
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));

            Conditions = new ConditionsViewModel(this);

            if (model.Conditions != null)
            {
                foreach (var childCondition in model.Conditions)
                {
                    Conditions.Add(ConditionFactory.Create(childCondition, owner));
                }
            }

            Fix();

            AddConditionCommand = new RelayCommand(AddCondition, () => CanAddCondition);
            DeleteCommand = new RelayCommand(Delete);
            Conditions.CollectionChanged += ConditionsCollectionChanged;
        }

        private void ConditionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                OnConditionChanged(this);
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.OfType<ConditionViewModel>())
                {
                    item.ConditionChanged += ItemConditionChanged;
                    item.ConditionTypeChanged += ItemConditionTypeChanged;
                }

                OnConditionChanged(this);
            }
        }

        public void ItemConditionTypeChanged(object sender, EventArgs e)
        {

        }

        private void Validate()
        {
            IsValid = IsConditionValid();

            ValidateChildren();
        }

        protected abstract bool IsConditionValid();

        private void ItemConditionChanged(object sender, EventArgs e)
        {
            OnConditionChanged(sender);
        }

        public ICommand AddConditionCommand { get; }
        public ICommand DeleteCommand { get; }

        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    RaisePropertyChanged();
                    ValidateChildren();
                }
            }
        }

        private void SetErrorMessage()
        {
            ErrorMessage = ConditionErrorMessage;
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        public abstract string ConditionErrorMessage { get; }

        public void ValidateChildren()
        {
            if (Conditions.Any(c => c.IsValid == false) || IsValid == false)
            {
                AreChildrenValid = false;
            }
            else
            {
                AreChildrenValid = true;
            }
        }

        private bool _areChildrenValid = true;
        public bool AreChildrenValid
        {
            get { return _areChildrenValid; }
            set
            {
                if (_areChildrenValid != value)
                {
                    _areChildrenValid = value;
                    SetErrorMessage();
                    RaisePropertyChanged();
                }
            }
        }

        private void AddCondition()
        {
            if (!CanAddCondition)
                return;

            var model = new StateMachineCondition()
            {
                ConditionType = ConditionType.Input
            };

            var condition = ConditionFactory.Create(model, _owner);

            Conditions.Add(condition);
        }

        public abstract bool CanSelectInput { get; }

        public abstract bool CanAddCondition { get; }

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
        protected abstract void Fix();

        //public ConditionType ConditionType
        //{
        //    get => _model.ConditionType;
        //    set
        //    {
        //        _model.ConditionType = value;
        //        RaisePropertyChanged();
        //        RaisePropertyChanged(() => CanSelectInput);
        //        RaisePropertyChanged(() => CanAddCondition);
        //        Fix();
        //        _owner.DirtyService.MarkDirty();
        //        OnConditionChanged(this);
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
                RaisePropertyChanged(() => CanAddCondition);
                Fix();
                _owner.DirtyService.MarkDirty();
                OnConditionChanged(this);
            }
        }

        public Guid? InputId
        {
            get { return _model.SourceInputId; }
            set
            {
                if (_model.SourceInputId != value)
                {
                    _model.SourceInputId = value;
                    RaisePropertyChanged();
                    _owner.DirtyService.MarkDirty();
                    OnConditionChanged(this);
                }
            }
        }

        protected virtual void OnConditionChanged(object sender)
        {
            Validate();
            ConditionChanged?.Invoke(sender, EventArgs.Empty);
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
