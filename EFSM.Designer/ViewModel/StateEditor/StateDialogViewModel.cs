using Cas.Common.WPF;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Extensions;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel.StateEditor
{
    public class StateDialogViewModel : ViewModelBase, ICloseableViewModel
    {
        public event EventHandler<CloseEventArgs> Close;
        public ObservableCollection<StateMachineOutputActionViewModel> Outputs { get; }
        public ICommand OkCommand { get; }
        public IDirtyService DirtyService => _dirtyService;

        private readonly IDirtyService _dirtyService = new DirtyService();
        private readonly State _model;
        private readonly StateMachineViewModel _parent;
        private OrderedListDesigner<StateMachineOutputActionViewModel> _entryActions;
        private OrderedListDesigner<StateMachineOutputActionViewModel> _exitActions;
        private Action<State> _updateParent;
        private IMessageBoxService _messageBoxService;

        public StateDialogViewModel(StateMachineViewModel parent, State model, Action<State> updateParent, IMessageBoxService messageBoxService)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            Outputs = _parent.Outputs ?? throw new ArgumentNullException(nameof(_parent.Outputs));
            _messageBoxService = messageBoxService ?? throw new ArgumentNullException(nameof(messageBoxService));
            _updateParent = updateParent;

            InitializeActions();
            OkCommand = new RelayCommand(Ok);

            DirtyService.MarkClean();
        }

        public OrderedListDesigner<StateMachineOutputActionViewModel> EntryActions
        {
            get { return _entryActions; }
            set
            {
                _entryActions = value;
                RaisePropertyChanged();
            }
        }
        public OrderedListDesigner<StateMachineOutputActionViewModel> ExitActions
        {
            get { return _exitActions; }
            set
            {
                _exitActions = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                DirtyService.MarkDirty();
                RaisePropertyChanged(() => Title);
            }
        }

        public string Title => Name;

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

        public bool CanClose()
        {
            if (DirtyService.IsDirty)
            {
                var result = MessageBox.Show("Save changes?", "State Machine has changed", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Save();
                        break;
                    case MessageBoxResult.No:
                        return true;
                    case MessageBoxResult.Cancel:
                        return false;
                }
            }

            return true;
        }


        public void Closed()
        {
        }

        private void Save()
        {
            _parent.DirtyService.MarkDirty();
            _dirtyService.MarkClean();
            _updateParent(GetModel());
        }

        private void Ok()
        {
            try
            {
                if (_dirtyService.IsDirty)
                {
                    Save();
                    Close?.Invoke(this, new CloseEventArgs(true));
                }
                else
                {
                    Close?.Invoke(this, new CloseEventArgs(false));
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.Show(ex);
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
