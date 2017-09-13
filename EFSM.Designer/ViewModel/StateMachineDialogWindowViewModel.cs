using Autofac;
using Cas.Common.WPF.Behaviors;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
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

        public ICommand DeleteCommand { get; private set; }
        public ICommand OkCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand SimulationCommand { get; private set; }

        private StateMachineViewModel _stateMachineViewModel = null;

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

        public StateMachineDialogWindowViewModel(StateMachine stateMachine, IViewService viewService)
        {
            _viewService = viewService;
            _stateMachine = stateMachine;
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
        }

        public StateMachine GetModel()
        {
            return StateMachineViewModel.GetModel();
        }

        private void OkButtonClick()
        {
            Close?.Invoke(this, new CloseEventArgs(true));
        }

        private void Delete()
        {
        }

        private StateMachine SaveMomento()
        {
            return GetModel();
        }

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

        public bool CanUndo()
        {
            return _undoService.CanUndo();
        }

        public bool CanRedo()
        {
            return _undoService.CanRedo();
        }

        public bool CanClose()
        {
            return true;
        }

        public void Closed()
        {
        }
    }
}