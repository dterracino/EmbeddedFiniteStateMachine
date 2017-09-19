using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cas.Common.WPF;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace EFSM.Designer.ViewModel.TransitionEditor
{
    public class TransitionEditorViewModel : ViewModelBase, ICloseableViewModel
    {
        private CriteriaTransitionViewModel _criteriaViewModel;
        private OrderedListDesigner<StateMachineOutputActionViewModel> _actions;
        private readonly IDirtyService _dirtyService = new DirtyService();
        private string _name;
        private readonly TransitionViewModel _transition;

        public TransitionEditorViewModel(TransitionViewModel transition, IEnumerable<StateMachineInputViewModel> inputs, IEnumerable<StateMachineOutputActionViewModel> outputs)
        {
            Inputs = new ObservableCollection<StateMachineInputViewModel>(inputs);
            Outputs = new ObservableCollection<StateMachineOutputActionViewModel>(outputs);
            _transition = transition;

            Actions = new OrderedListDesigner<StateMachineOutputActionViewModel>(NewFactory, outputs.Where(o => transition.Actions.Contains(o.Id)));
            _actions.ListChanged += ActionsOnListChanged;

            Name = Transition.Name;

            CriteriaViewModel = new CriteriaTransitionViewModel(this);

            OkCommand = new RelayCommand(Ok);
            _dirtyService.MarkClean();
        }

        public CriteriaTransitionViewModel CriteriaViewModel
        {
            get { return _criteriaViewModel; }
            private set
            {
                _criteriaViewModel = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler<CloseEventArgs> Close;

        public ObservableCollection<StateMachineInputViewModel> Inputs { get; }
        public ObservableCollection<StateMachineOutputActionViewModel> Outputs { get; }

        public TransitionViewModel Transition
        {
            get { return _transition; }
        }

        public ICommand OkCommand { get; }

        public IDirtyService DirtyService => _dirtyService;
        
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
    }
}
