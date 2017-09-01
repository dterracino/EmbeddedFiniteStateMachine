using EFSM.Designer.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace EFSM.Designer.View
{
    /// <summary>
    /// Interaction logic for PropertyGridView.xaml
    /// </summary>
    public partial class PropertyGridView : UserControl
    {
        private bool _hasPropertyChanged;

        public static readonly DependencyProperty SelectionServiceProperty = DependencyProperty.Register(
            "SelectionService",
            typeof(ISelectionService),
            typeof(PropertyGridView),
            new PropertyMetadata(null, SelectionServiceChanged));

        public static readonly DependencyProperty UndoProviderProperty = DependencyProperty.Register(
            "UndoProvider",
            typeof(IUndoProvider),
            typeof(PropertyGridView));


        public PropertyGridView()
        {
            InitializeComponent();
        }

        public ISelectionService SelectionService
        {
            get { return (ISelectionService)GetValue(SelectionServiceProperty); }
            set { SetValue(SelectionServiceProperty, value); }
        }

        public IUndoProvider UndoProvider
        {
            get { return (IUndoProvider)GetValue(UndoProviderProperty); }
            set { SetValue(UndoProviderProperty, value); }
        }

        private void OnSelectionChanged(object sender, EventArgs args)
        {
            var selectionService = SelectionService;

            IPropertyGridSource source = null;

            if (selectionService == null)
                return;

            var selectedItems = selectionService.GetSelected();

            if (selectedItems.Length == 1)
            {
                source = selectedItems[0] as IPropertyGridSource;
            }

            MainPropertyGrid.SelectedObject = source?.PropertyGridData;
        }

        private static void SelectionServiceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var propertyGridView = d as PropertyGridView;

            if (propertyGridView == null)
            {
                throw new InvalidOperationException("Invalid dependency object.");
            }

            var newValue = e.NewValue as ISelectionService;
            var oldValue = e.OldValue as ISelectionService;

            if (oldValue != null)
            {
                oldValue.SelectionChanged -= propertyGridView.OnSelectionChanged;
            }

            if (newValue != null)
            {
                newValue.SelectionChanged += propertyGridView.OnSelectionChanged;
            }

            //Update the selection
            propertyGridView.OnSelectionChanged(null, EventArgs.Empty);
        }
        private void SaveUndoStateIfChanged()
        {
            if (_hasPropertyChanged)
            {
                _hasPropertyChanged = false;

                UndoProvider?.SaveUndoState();
            }
        }

        private void PropertyGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            _hasPropertyChanged = false;
        }

        private void PropertyGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveUndoStateIfChanged();
        }

        private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            _hasPropertyChanged = true;
        }

        private void PropertyGrid_SelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SaveUndoStateIfChanged();
        }

    }
}
