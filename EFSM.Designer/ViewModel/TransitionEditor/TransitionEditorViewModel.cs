using Cas.Common.WPF.Behaviors;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Designer.ViewModel.TransitionEditor;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;

namespace EFSM.Designer.ViewModel
{
    public class TransitionEditorViewModel : ViewModelBase, ICloseableViewModel
    {
        private CriteriaTransitionViewModel _criteriaViewModel = null;
        public CriteriaTransitionViewModel CriteriaViewModel
        {
            get { return _criteriaViewModel; }
            private set { _criteriaViewModel = value; RaisePropertyChanged(); }
        }

        public event EventHandler<CloseEventArgs> Close;

        public ObservableCollection<StateMachineInputViewModel> Inputs { get; private set; }
        public ObservableCollection<StateMachineOutputActionViewModel> Outputs { get; private set; }

        private TransitionViewModel _transition = null;
        public TransitionViewModel Transition
        {
            get { return _transition; }
            private set { _transition = value; RaisePropertyChanged(); }
        }

        public ICommand OkCommand { get; private set; }

        private readonly IDirtyService _dirtyService = new DirtyService();
        public IDirtyService DirtyService => _dirtyService;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
                _dirtyService.MarkDirty();
            }
        }

        private OrderedListDesigner<StateMachineOutputActionViewModel> _actions;
        public OrderedListDesigner<StateMachineOutputActionViewModel> Actions
        {
            get { return _actions; }
            private set { _actions = value; RaisePropertyChanged(); }
        }

        public TransitionEditorViewModel(TransitionViewModel transition, IEnumerable<StateMachineInputViewModel> inputs, IEnumerable<StateMachineOutputActionViewModel> outputs)
        {
            Inputs = new ObservableCollection<StateMachineInputViewModel>(inputs);
            Outputs = new ObservableCollection<StateMachineOutputActionViewModel>(outputs);
            Transition = transition;


            Actions = new OrderedListDesigner<StateMachineOutputActionViewModel>(NewFactory, outputs.Where(o => transition.Actions.Contains(o.Id)));
            _actions.ListChanged += ActionsOnListChanged;

            Name = Transition.Name;


            CriteriaViewModel = new CriteriaTransitionViewModel(this);

            OkCommand = new RelayCommand(Ok);
            _dirtyService.MarkClean();
        }

        private void ActionsOnListChanged(object sender, EventArgs e)
        {
            _dirtyService.MarkDirty();
        }

        private StateMachineOutputActionViewModel NewFactory()
        {
            return new StateMachineOutputActionViewModel();
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

        public bool CanClose() => true;

        public void Closed()
        {
        }

        private void MarkDirty()
        {
            _dirtyService.MarkDirty();
        }
    }
}
