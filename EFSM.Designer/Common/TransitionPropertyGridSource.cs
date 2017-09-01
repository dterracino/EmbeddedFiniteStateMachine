using EFSM.Designer.ViewModel;
using System;
using System.ComponentModel;

namespace EFSM.Designer.Common
{
    [DisplayName("Transition")]
    public class TransitionPropertyGridSource
    {
        private readonly TransitionViewModel _transitionViewModel;

        public TransitionPropertyGridSource(TransitionViewModel transitionViewModel)
        {
            _transitionViewModel = transitionViewModel ?? throw new ArgumentNullException(nameof(transitionViewModel));
        }

        [Description("The name of the transition")]
        public string Name
        {
            get { return _transitionViewModel.Name; }
            set { _transitionViewModel.Name = value; }
        }


    }
}
