

//using Cas.Common.WPF;
//using EFSM.Domain;
//using System.Collections.ObjectModel;
//using System.Linq;

//namespace EFSM.Designer.ViewModel
//{
//    public class StateMachineViewModelBase : DesignerViewModelBase
//    {
//        protected StateMachine _model;

//        public StateMachineViewModelBase(StateMachine model)
//        {
//            _model = model;
//            InitiateModel();
//        }

//        private void InitiateModel()
//        {
//            AddStateViewModels();
//            AddTransitionViewModel();
//            AddInputViewModel();
//        }

//        private ObservableCollection<StateMachineInputViewModel> _inputs = new ObservableCollection<StateMachineInputViewModel>();
//        public ObservableCollection<StateMachineInputViewModel> Inputs
//        {
//            get { return _inputs; }
//            set { _inputs = value; RaisePropertyChanged(); }
//        }

//        private ObservableCollection<StateViewModel> _states = new ObservableCollection<StateViewModel>();
//        public ObservableCollection<StateViewModel> States
//        {
//            get { return _states; }
//            set { _states = value; RaisePropertyChanged(); }
//        }

//        private readonly ObservableCollection<TransitionViewModel> _transitions = new ObservableCollection<TransitionViewModel>();
//        public ObservableCollection<TransitionViewModel> Transitions => _transitions;

//        private void AddTransitionViewModel()
//        {
//            Transitions.Clear();

//            if (_model.Transitions != null)
//            {
//                foreach (var item in _model.Transitions)
//                {
//                    var source = States.First(s => s.Id == item.SourceStateId);
//                    var target = States.First(s => s.Id == item.TargetStateId);
//                    var transition = AddTransition(source, target, item.Name);
//                }
//            }
//        }

//        private void AddInputViewModel()
//        {
//            Inputs.Clear();

//            if (_model.Inputs != null)
//            {
//                Inputs.AddRange(_model.Inputs.Select(i => new StateMachineInputViewModel(i)));
//            }
//        }

//        private void AddStateViewModels()
//        {
//            States.Clear();

//            if (_model.States != null)
//            {
//                States.AddRange(_model.States.Select(st => new StateViewModel(st, this, _undoProvider)));
//            }
//        }
//    }
//}
