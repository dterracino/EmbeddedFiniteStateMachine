using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.StateEditor
{
    public class StateEditorViewModel : ViewModelBase, ICloseableViewModel
    {
        public event EventHandler<CloseEventArgs> Close;
        private readonly IIsDirtyService _dirtyService = new IsDirtyService();
        public IIsDirtyService DirtyService => _dirtyService;

        private State _model;
        private StateMachineViewModel _parent;
        public ObservableCollection<StateMachineOutputActionViewModel> Outputs { get; private set; }
        public ICommand OkCommand { get; private set; }

        private OrderedListDesigner<StateMachineOutputActionViewModel> _entryActions;
        private OrderedListDesigner<StateMachineOutputActionViewModel> _exitActions;
        public OrderedListDesigner<StateMachineOutputActionViewModel> EntryActions
        {
            get { return _entryActions; }
            set { _entryActions = value; RaisePropertyChanged(); }
        }
        public OrderedListDesigner<StateMachineOutputActionViewModel> ExitActions
        {
            get { return _exitActions; }
            set { _exitActions = value; RaisePropertyChanged(); }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                DirtyService.MarkDirty();
            }
        }

        public StateEditorViewModel(StateMachineViewModel parent, State model, ObservableCollection<StateMachineOutputActionViewModel> actions)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            Outputs = actions ?? throw new ArgumentNullException(nameof(actions));

            InitializeActions();
            OkCommand = new RelayCommand(Ok);

            DirtyService.MarkClean();
        }

        private void InitializeActions()
        {
            var entryActions = new List<StateMachineOutputActionViewModel>();
            var exitActions = new List<StateMachineOutputActionViewModel>();

            if (_model.EntryActions != null)
            {
                foreach (var item in _model.EntryActions)
                {
                    var action = _parent.Outputs.FirstOrDefault(o => o.Id == item);

                    if (action != null)
                    {
                        entryActions.Add(action);
                    }
                }
            }

            if (_model.ExitActions != null)
            {
                foreach (var item in _model.ExitActions)
                {
                    var action = _parent.Outputs.FirstOrDefault(o => o.Id == item);

                    if (action != null)
                    {
                        exitActions.Add(action);
                    }
                }
            }

            EntryActions = new OrderedListDesigner<StateMachineOutputActionViewModel>(() => new StateMachineOutputActionViewModel(), entryActions);
            ExitActions = new OrderedListDesigner<StateMachineOutputActionViewModel>(() => new StateMachineOutputActionViewModel(), exitActions);
            _entryActions.ListChanged += ActionsListChanged;
            _exitActions.ListChanged += ActionsListChanged;
        }

        private void ActionsListChanged(object sender, EventArgs e)
        {
            DirtyService.MarkDirty();
        }

        public bool CanClose() => true;

        public void Closed()
        {
        }

        private void Ok()
        {
            if (_dirtyService.IsDirty)
            {
                Close?.Invoke(this, new CloseEventArgs(true));
            }
            else
            {
                Close?.Invoke(this, new CloseEventArgs(false));
            }
        }

        public State GetModel()
        {
            _model.EntryActions = _entryActions.Items.Select(a => a.Id).ToArray();
            _model.ExitActions = _exitActions.Items.Select(a => a.Id).ToArray();
            return _model.Clone();
        }
    }
}
