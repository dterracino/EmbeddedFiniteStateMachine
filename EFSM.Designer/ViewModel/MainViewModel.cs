using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EFSM.Designer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<StateMachineViewModel> _stateMachines = new ObservableCollection<StateMachineViewModel>();

        public MainViewModel()
        {
            
        }

        public string Title => "State Machine Designer";

        public ObservableCollection<StateMachineViewModel> StateMachines => _stateMachines;
    }
}