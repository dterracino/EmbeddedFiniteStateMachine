using System;
using System.Windows;
using System.Windows.Controls;

namespace EFSM.Designer.Controls
{
    /// <summary>
    /// Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty TextProperty;

        static InputUserControl()
        {
            ValueProperty = DependencyProperty.Register(nameof(Value), typeof(bool), typeof(InputUserControl));
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

        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public Guid GroupNameGuid { get; } = Guid.NewGuid();
    }
}
