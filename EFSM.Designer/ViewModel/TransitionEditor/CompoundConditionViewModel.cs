using EFSM.Domain;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class CompoundConditionViewModel : ConditionViewModelBase
    {
        private readonly ConditionsViewModel _children;
        private CompoundConditionType _conditionType;

        public CompoundConditionViewModel(TransitionEditorViewModel owner)
            : base(owner)
        {
            CommandsInitialize();

            _children = new ConditionsViewModel(this);
        }

        private void CommandsInitialize()
        {
            AddSimpleConditionCommand = new RelayCommand(AddSimpleCondition, CanAddSimpleCondition);
            AddCompoundConditionCommand = new RelayCommand(AddCompoundCondition);
            MakeOrCommand = new RelayCommand(MakeOr, CanMakeOr);
            MakeAndCommand = new RelayCommand(MakeAnd, CanMakeAnd);
        }

        public ICommand AddSimpleConditionCommand { get; private set; }

        public ICommand AddCompoundConditionCommand { get; private set; }

        public ICommand MakeOrCommand { get; private set; }

        public ICommand MakeAndCommand { get; private set; }

        private void MakeOr()
        {
            ConditionType = CompoundConditionType.Or;
        }

        private void MakeAnd()
        {
            ConditionType = CompoundConditionType.And;
        }

        private bool CanMakeOr()
        {
            return ConditionType != CompoundConditionType.Or;
        }

        private bool CanMakeAnd()
        {
            return ConditionType != CompoundConditionType.And;
        }

        private void AddSimpleCondition()
        {
            Children.Add(new SimpleConditionViewModel(Owner)
            {
                SourceInputId = Owner.Inputs[0].Id
            });
        }

        private bool CanAddSimpleCondition()
        {
            return Owner.Inputs.Count > 0;
        }

        private void AddCompoundCondition()
        {
            Children.Add(new CompoundConditionViewModel(Owner));
        }

        public ObservableCollection<ConditionViewModelBase> Children => _children;

        public CompoundConditionType ConditionType
        {
            get { return _conditionType; }
            set
            {
                _conditionType = value;
                RaisePropertyChanged();
                DirtyService.MarkDirty();
            }
        }


        internal override StateMachineCondition GetModel()
        {
            return new StateMachineCondition()
            {
                CompoundConditionType = ConditionType,
                Conditions = Children.Select(c => c.GetModel()).ToList()
            };
        }
    }
}
