using EFSM.Designer.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace EFSM.Designer.View
{
    /// <summary>
    /// Interaction logic for ActionsView.xaml
    /// </summary>
    public partial class ActionsView : UserControl
    {
        public static readonly DependencyProperty OutputsProperty
           = DependencyProperty.Register("Outputs", typeof(IEnumerable<StateMachineOutputActionViewModel>),
               typeof(ActionsView), new FrameworkPropertyMetadata(TextProperty_PropertyChanged));

        public ActionsView()
        {
            InitializeComponent();
        }

        private static void TextProperty_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// This is necessary to provide the outputs dropdown.
        /// </summary>
        public IEnumerable<StateMachineOutputActionViewModel> Outputs
        {
            get { return (IEnumerable<StateMachineOutputActionViewModel>)GetValue(OutputsProperty); }
            set { SetValue(OutputsProperty, value); }
        }
    }
}
