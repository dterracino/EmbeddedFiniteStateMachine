using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
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

        public TransitionEditorViewModel(StateMachineTransition model, StateMachineViewModel parent, Action<StateMachineTransition> updateParentModel)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _updateParentModel = updateParentModel;
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));

            Inputs = new ObservableCollection<StateMachineInputViewModel>(parent.Inputs);
            Outputs = new ObservableCollection<StateMachineOutputActionViewModel>(parent.Outputs);
            _transition = ApplicationContainer.Container.Resolve<TransitionViewModel>(
                new TypedParameter(typeof(StateMachineViewModel), parent),
                new TypedParameter(typeof(StateMachineTransition), model)
                );

            Actions = new OrderedListDesigner<StateMachineOutputActionViewModel>(NewFactory, parent.Outputs.Where(o => _transition.Actions.Contains(o.Id)));
            _actions.ListChanged += ActionsOnListChanged;

            Name = Transition.Name;

            Criteria = new CriteriaTransitionViewModel(this);

            OkCommand = new RelayCommand(Ok);
            _dirtyService.MarkClean();
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
            if (_dirtyService.IsDirty)
            {
                Save();
            }
            Close?.Invoke(this, new CloseEventArgs(_dirtyService.IsDirty));
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
