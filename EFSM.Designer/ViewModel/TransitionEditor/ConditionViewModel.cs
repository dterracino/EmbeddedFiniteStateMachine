using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.ViewModel.TransitionEditor.Conditions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class ConditionViewModel : ViewModelBase
    {
        public event EventHandler ConditionChanged;

        private bool _isValid = true;
        private readonly StateMachineCondition _model;
        private readonly TransitionEditorViewModel _owner;
        private string _errorMessage;
        private bool _areChildrenValid = true;

        private readonly ConditionEditServiceManager _serviceManager;

        private IConditionEditService _editService;

        public ConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner, ConditionEditServiceManager serviceManager)
        {
            _model = model;
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));

            Conditions = new ConditionsViewModel(this);

            if (model.Conditions != null)
            {
                foreach (var childCondition in model.Conditions)
                {
                    Conditions.Add(new ConditionViewModel(childCondition, owner, serviceManager));
                }
            }

            //Get the current editor
            _editService = _serviceManager[model.ConditionType];

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
                }

                OnConditionChanged(this);
            }
        }

        private void Validate()
        {
            //Check the minimum number of children
            if (_editService.MinimumNumberOfChildren != null &&
                Conditions.Count < _editService.MinimumNumberOfChildren.Value)
            {
                IsValid = false;
            }
            //Check the maximum number of children
            else if (_editService.MaximumNumberOfChildren != null &&
                Conditions.Count > _editService.MaximumNumberOfChildren.Value)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }

            ValidateChildren();
        }

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
            ErrorMessage = _editService.ErrorMessage;
        }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

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

            var condition = new ConditionViewModel(model, _owner, _serviceManager);

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
                return _editService.MaximumNumberOfChildren != null &&
                       _editService.MaximumNumberOfChildren.Value > Conditions.Count;
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
            _editService.Fix(this);
        }

        public TransitionEditorViewModel Owner => _owner;

        public ConditionType ConditionType
        {
            get => _model.ConditionType;
            set
            {
                //Get the current editor
                _editService = _serviceManager[value];

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
