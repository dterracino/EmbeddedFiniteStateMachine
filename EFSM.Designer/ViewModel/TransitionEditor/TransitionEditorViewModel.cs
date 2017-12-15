using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class TransitionEditorViewModel : ViewModelBase, ICloseableViewModel
    {
        private CriteriaTransitionViewModel _criteria;
        private OrderedListDesigner<StateMachineOutputActionViewModel> _actions;
        private readonly IDirtyService _dirtyService = new DirtyService();
        private readonly TransitionViewModel _transition;
        private readonly StateMachineTransition _model;
        private readonly Action<StateMachineTransition> _updateParentModel;
        private readonly StateMachineViewModel _parent;
        private string _conditionText;
        private IMessageBoxService _messageBoxService;
        private MarkdownConditionGenerator _markdownConditionGenerator = new MarkdownConditionGenerator();

        public TransitionEditorViewModel(
            StateMachineTransition model,
            StateMachineViewModel parent,
            Action<StateMachineTransition> updateParentModel,
            IMessageBoxService messageBoxService)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _updateParentModel = updateParentModel;
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));

            Inputs = new ObservableCollection<StateMachineInputViewModel>(parent.Inputs);
            Outputs = new ObservableCollection<StateMachineOutputActionViewModel>(parent.Outputs);
            _transition = ApplicationContainer.Container.Resolve<TransitionViewModel>(
                new TypedParameter(typeof(StateMachineViewModel), parent),
                new TypedParameter(typeof(StateMachineTransition), model)
                );

            Actions = new OrderedListDesigner<StateMachineOutputActionViewModel>(NewFactory, parent.Outputs.Where(o => _transition.Actions.Contains(o.Id)));
            _actions.ListChanged += ActionsOnListChanged;

            Name = Transition.Name;

            Criteria = new CriteriaTransitionViewModel(this, _messageBoxService);

            OkCommand = new RelayCommand(Ok);
            _dirtyService.MarkClean();


            RecalculateConditionText();

            Criteria.PropertyChanged += CriteriaPropertyChanged;

            if (Criteria.RootCondition != null)
            {
                Criteria.RootCondition.ConditionChanged += RootConditionChanged;
            }
        }

        private void RootConditionChanged(object sender, EventArgs e)
        {
            RecalculateConditionText();
        }

        private void CriteriaPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Criteria.RootCondition))
            {
                RecalculateConditionText();

                if (Criteria.RootCondition != null)
                {
                    Criteria.RootCondition.ConditionChanged += RootConditionChanged;
                }
            }
        }

        private bool _isRecalculationConditionText = false;

        private void RecalculateConditionText()
        {
            if (!_isRecalculationConditionText & Criteria.RootCondition != null && Inputs.Any())
            {
                _isRecalculationConditionText = true;

                if (Criteria.RootCondition.AreChildrenValid)
                {
                    var model = GetModel();
                    ConditionText = _markdownConditionGenerator.Generate(model, Inputs.Select(i => i.GetModel()).ToArray()).ToString();
                }
                else
                {
                    ConditionText = string.Empty;
                }
            }

            _isRecalculationConditionText = false;
        }

        public CriteriaTransitionViewModel Criteria
        {
            get { return _criteria; }
            private set
            {
                _criteria = value;
                RaisePropertyChanged();
            }
        }

        public string ConditionText
        {
            get { return _conditionText; }
            set { _conditionText = value; RaisePropertyChanged(); }
        }

        public event EventHandler<CloseEventArgs> Close;

        public ObservableCollection<StateMachineInputViewModel> Inputs { get; }
        public ObservableCollection<StateMachineOutputActionViewModel> Outputs { get; }

        public TransitionViewModel Transition => _transition;

        public ICommand OkCommand { get; }

        public IDirtyService DirtyService => _dirtyService;

        public StateMachineTransition GetModel()
        {
            var model = _model.Clone();
            model.Condition = Criteria.GetModel();
            model.TransitionActions = Actions.Items.Where(i => Outputs.Select(o => o.Id).Contains(i.Id)).Select(i => i.Id).ToArray();
            return model;
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        public OrderedListDesigner<StateMachineOutputActionViewModel> Actions
        {
            get { return _actions; }
            private set
            {
                _actions = value;
                RaisePropertyChanged();
            }
        }

        private void ActionsOnListChanged(object sender, EventArgs e)
        {
            _dirtyService.MarkDirty();
        }

        private StateMachineOutputActionViewModel NewFactory() => new StateMachineOutputActionViewModel();

        private void Ok()
        {
            try
            {
                if (_dirtyService.IsDirty)
                {
                    Save();
                }
                Close?.Invoke(this, new CloseEventArgs(_dirtyService.IsDirty));
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
            }
        }

        private void Save()
        {
            _dirtyService.MarkClean();
            _parent.DirtyService.MarkDirty();
            _updateParentModel(GetModel());
        }

        public bool CanClose() => true;

        public void Closed()
        {
        }
    }
}
