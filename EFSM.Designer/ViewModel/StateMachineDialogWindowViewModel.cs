using Autofac;
using EFSM.Designer.Common;
using EFSM.Designer.Interfaces;
using EFSM.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class StateMachineDialogWindowViewModel : ViewModelBase, IUndoProvider
    {
        public string Title => "State Machine Editor";

        private readonly IUndoService<StateMachine> _undoService;
        public IIsDirtyService DirtyService { get; private set; } = new IsDirtyService();

        public ICommand DeleteCommand { get; private set; }
        public ICommand OkCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }

        private StateMachineViewModel _stateMachineViewModel = null;
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

        private StateMachine _stateMachine;


        public StateMachineDialogWindowViewModel(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            InitiateStateMachineViewModel();


            _undoService = new UndoService<StateMachine>();
            _undoService.Clear(SaveMomento());

            DirtyService.MarkClean();

            DeleteCommand = new RelayCommand(Delete);
            OkCommand = new RelayCommand(OkButtonClick);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            RedoCommand = new RelayCommand(Redo, CanRedo);
        }

        private void InitiateStateMachineViewModel()
        {
            StateMachineViewModel = ApplicationContainer.Container.Resolve<StateMachineViewModel>
               (new TypedParameter(typeof(StateMachine), _stateMachine),
               new TypedParameter(typeof(StateMachineDialogWindowViewModel), this));
        }

        public StateMachine GetModel()
        {
            return StateMachineViewModel.GetModel();
        }

        private void OkButtonClick()
        {
            //throw new NotImplementedException();
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
    }
}