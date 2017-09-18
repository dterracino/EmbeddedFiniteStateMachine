using Autofac;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineDialogWindowViewModel : ViewModelBase, IUndoProvider, ICloseableViewModel
    {
        public string Title => "State Machine Editor";

        private readonly IUndoService<StateMachine> _undoService;
        private IViewService _viewService;
        private StateMachine _stateMachine;
        public IIsDirtyService DirtyService { get; private set; } = new IsDirtyService();
        private IIsDirtyService _parentDirtyService;

        public ICommand DeleteCommand { get; private set; }
        public ICommand OkCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand SimulationCommand { get; private set; }

        private StateMachineViewModel _stateMachineViewModel = null;

        private OrderedListDesigner<StateMachineInputViewModel> _inputs;
        public OrderedListDesigner<StateMachineInputViewModel> Inputs => _inputs;

        private OrderedListDesigner<StateMachineOutputActionViewModel> _outputs;
        public OrderedListDesigner<StateMachineOutputActionViewModel> Outputs => _outputs;

        public event EventHandler<CloseEventArgs> Close;

        public StateMachineViewModel StateMachineViewModel
        {
            get { return _stateMachineViewModel; }
            set
            {
                if (_stateMachineViewModel != value)
                {
                    _stateMachineViewModel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public StateMachineDialogWindowViewModel(StateMachine stateMachine, IViewService viewService, IIsDirtyService parentDirtyService)
        {
            _viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            _parentDirtyService = parentDirtyService ?? throw new ArgumentNullException(nameof(parentDirtyService));
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));

            InitiateStateMachineViewModel();


            _undoService = new UndoService<StateMachine>();
            _undoService.Clear(SaveMomento());

            DirtyService.MarkClean();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            DeleteCommand = new RelayCommand(Delete);
            OkCommand = new RelayCommand(OkButtonClick);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            RedoCommand = new RelayCommand(Redo, CanRedo);
            SimulationCommand = new RelayCommand(Simulate);
        }

        private void Simulate()
        {
            var viewModel = ApplicationContainer.Container.Resolve<SimulationViewModel>(
                new TypedParameter(typeof(StateMachine), GetModel())
                );

            _viewService.ShowDialog(viewModel);
        }

        private void InitiateStateMachineViewModel()
        {
            StateMachineViewModel = ApplicationContainer.Container.Resolve<StateMachineViewModel>
               (
                new TypedParameter(typeof(StateMachine), _stateMachine),
                new TypedParameter(typeof(IUndoProvider), this),
                new TypedParameter(typeof(IIsDirtyService), DirtyService)
               );
            _inputs = new OrderedListDesigner<StateMachineInputViewModel>(CreateInput, StateMachineViewModel.Inputs, addedAction: ConnectorAdded, deletedAction: InputDeleted);
            _inputs.ListChanged += ChildListChanged;

            _outputs = new OrderedListDesigner<StateMachineOutputActionViewModel>(CreateOutput, StateMachineViewModel.Outputs, addedAction: OutputAdded, deletedAction: OutputDeleted);
            _outputs.ListChanged += ChildListChanged;
        }

        public StateMachine GetModel() => StateMachineViewModel.GetModel();

        private StateMachineInputViewModel CreateInput()
        {
            DirtyService.MarkDirty();
            return new StateMachineInputViewModel(new StateMachineInput { Id = Guid.NewGuid(), Name = "Input" }, StateMachineViewModel);
        }

        private StateMachineOutputActionViewModel CreateOutput()
        {
            DirtyService.MarkDirty();
            return new StateMachineOutputActionViewModel(new StateMachineOutputAction { Id = Guid.NewGuid(), Name = "Output" }, StateMachineViewModel);
        }

        private void ChildListChanged(object sender, EventArgs e)
        {
            DirtyService.MarkDirty();
        }

        private void InputDeleted(StateMachineInputViewModel input)
        {
            foreach (var item in StateMachineViewModel.Transitions)
            {
                item.DeleteInput(input);
            }
        }

        private void OutputDeleted(StateMachineOutputActionViewModel output)
        {
            var actionForDelete = StateMachineViewModel.Outputs.FirstOrDefault(o => o.Id == output.Id);

            if (actionForDelete != null)
            {
                StateMachineViewModel.Outputs.Remove(actionForDelete);
            }

            foreach (var item in StateMachineViewModel.Transitions)
            {
                item.DeleteOutputId(output.Id);
            }

            foreach (var item in StateMachineViewModel.States)
            {
                item.DeleteAction(output.Id);
            }
        }

        private void ConnectorAdded(StateMachineInputViewModel connector)
        {
            StateMachineViewModel.Inputs.Add(connector);
            DirtyService.MarkDirty();
        }

        private void OutputAdded(StateMachineOutputActionViewModel output)
        {
            StateMachineViewModel.Outputs.Add(output);
            DirtyService.MarkDirty();
        }


        private void OkButtonClick()
        {
            DirtyService.MarkClean();
            _parentDirtyService.MarkDirty();
            Close?.Invoke(this, new CloseEventArgs(true));
        }

        private void Delete()
        {
        }

        private StateMachine SaveMomento() => GetModel();

        private void Redo()
        {
            _stateMachine = _undoService.Redo();
            InitiateStateMachineViewModel();
        }

        private void Undo()
        {
            _stateMachine = _undoService.Undo();
            InitiateStateMachineViewModel();
        }

        public void SaveUndoState()
        {
            _undoService.Do(SaveMomento());
        }

        public bool CanUndo() => _undoService.CanUndo();

        public bool CanRedo() => _undoService.CanRedo();

        public bool CanClose()
        {
            if (DirtyService.IsDirty)
            {
                if (MessageBox.Show("Really close?", "State Machine has changed", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        public void Closed()
        {
        }
    }
}