using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using EFSM.Designer.ViewModel.TransitionEditor.Conditions;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class ConditionViewModel : ViewModelBase
    {
        private const string InputErrorMessage = "Cannot have any children";
        private const string AndErrorMessage = "Has to have at least one child";
        private const string OrErrorMessage = "Has to have at least one child";
        private const string NotErrorMessage = "Has to have one and only one child";

        public event EventHandler ConditionChanged;

        private bool _isValid = true;
        private readonly StateMachineCondition _model;
        private readonly TransitionEditorViewModel _owner;
        private string _errorMessage;
        private bool _areChildrenValid = true;

        ////TODO: Inject this so there is a single global instance
        //private readonly ConditionEditServiceManager _serviceManager = new ConditionEditServiceManager();

        //private IConditionEditService _editService;

        public ConditionViewModel(StateMachineCondition model, TransitionEditorViewModel owner)
        {
            _model = model;
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));

            Conditions = new ConditionsViewModel(this);

            if (model.Conditions != null)
            {
                foreach (var childCondition in model.Conditions)
                {
                    Conditions.Add(new ConditionViewModel(childCondition, owner));
                }
            }

            //Get the current editor
            //_editService = _serviceManager[model.ConditionType];

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
            ////Check the minimum number of children
            //if (_editService.MinimumNumberOfChildren != null &&
            //    Conditions.Count < _editService.MinimumNumberOfChildren.Value)
            //{
            //    IsValid = false;
            //}
            ////Check the maximum number of children
            //else if (_editService.MaximumNumberOfChildren != null &&
            //    Conditions.Count > _editService.MaximumNumberOfChildren.Value)
            //{
            //    IsValid = false;
            //}
            //else
            //{
            //    IsValid = true;
            //}

            switch (ConditionType)
            {
                case ConditionType.Input:
                    IsValid = !Conditions.Any();
                    break;
                case ConditionType.And:
                    IsValid = (Conditions.Count >= 1);
                    break;
                case ConditionType.Or:
                    IsValid = (Conditions.Count >= 1);
                    break;
                case ConditionType.Not:
                    IsValid = (Conditions.Count == 1);
                    break;
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
            //ErrorMessage = _editService.ErrorMessage;

            switch (ConditionType)
            {
                case ConditionType.Input:
                    ErrorMessage = InputErrorMessage;
                    break;
                case ConditionType.And:
                    ErrorMessage = AndErrorMessage;
                    break;
                case ConditionType.Or:
                    ErrorMessage = OrErrorMessage;
                    break;
                case ConditionType.Not:
                    ErrorMessage = NotErrorMessage;
                    break;
            }
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
                //return _editService.MaximumNumberOfChildren != null &&
                //       _editService.MaximumNumberOfChildren.Value < Conditions.Count;

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
                //Get the current editor
                //_editService = _serviceManager[value];

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
