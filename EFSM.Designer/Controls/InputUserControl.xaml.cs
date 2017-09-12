using System.Windows;
using System.Windows.Controls;

namespace EFSM.Designer.Controls
{
    /// <summary>
    /// Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        public static readonly DependencyProperty IsOnProperty;
        public static readonly DependencyProperty TextProperty;

        static InputUserControl()
        {
            IsOnProperty = DependencyProperty.Register(nameof(IsOn), typeof(bool), typeof(InputUserControl));
            TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(InputUserControl));
        }

        public InputUserControl()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }
    }
}
