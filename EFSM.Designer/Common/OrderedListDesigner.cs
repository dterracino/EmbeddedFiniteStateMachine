using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace EFSM.Designer.Common
{
    public class OrderedListDesigner<TItemType> : ViewModelBase
        where TItemType : class
    {
        private readonly Func<TItemType> _newItemFactory;
        private readonly Action<TItemType> _addedAction;
        private readonly Action<TItemType> _deletedAction;
        private readonly ObservableCollection<TItemType> _items;
        private IList _selectedItems;

        /// <summary>
        /// Raised when the list is changed.
        /// </summary>
        public event EventHandler ListChanged;

        public OrderedListDesigner(Func<TItemType> newItemFactory, IEnumerable<TItemType> items = null, Action<TItemType> addedAction = null, Action<TItemType> deletedAction = null)
        {
            if (newItemFactory == null) throw new ArgumentNullException(nameof(newItemFactory));

            _newItemFactory = newItemFactory;
            _addedAction = addedAction;
            _deletedAction = deletedAction;
            MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            MoveDownCommand = new RelayCommand(MoveDown, CanMoveDown);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            MoveToTopCommand = new RelayCommand(MoveToTop, CanMoveToTop);
            MoveToBottomCommand = new RelayCommand(MoveToBottom, CanMoveToBottom);
            InsertAboveCommand = new RelayCommand(InsertAbove, CanInsertAbove);
            InsertBelowCommand = new RelayCommand(InsertBelow, CanInsertBelow);

            if (items == null)
            {
                _items = new ObservableCollection<TItemType>();
            }
            else
            {
                _items = new ObservableCollection<TItemType>(items);
            }

            _items.CollectionChanged += ItemsCollectionChanged;
        }

        protected void RaiseListChanged()
        {
            ListChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseListChanged();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    if (_addedAction != null)
                    {
                        foreach (var item in e.NewItems.Cast<TItemType>())
                        {
                            _addedAction(item);
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:

                    if (_deletedAction != null)
                    {
                        foreach (var item in e.OldItems.Cast<TItemType>())
                        {
                            _deletedAction(item);
                        }
                    }

                    break;
            }
        }

        public ICommand MoveUpCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand MoveToTopCommand { get; private set; }
        public ICommand MoveToBottomCommand { get; private set; }
        public ICommand InsertAboveCommand { get; private set; }
        public ICommand InsertBelowCommand { get; private set; }

        private TItemType CreateNewEntry() => _newItemFactory();

        private bool CanMoveToTop() => CanMoveUp();

        private void MoveToTop()
        {
            var newIndex = 0;

            foreach (var entry in SelectedEntries)
            {
                //Get where the item is right now
                var oldIndex = _items.IndexOf(entry);

                //Move the entry
                _items.Move(oldIndex, newIndex);

                newIndex++;
            }
        }

        private bool CanMoveToBottom() => CanMoveDown();

        private void MoveToBottom()
        {
            foreach (var entry in SelectedEntries)
            {
                var oldIndex = _items.IndexOf(entry);

                _items.Move(oldIndex, _items.Count - 1);
            }
        }

        private bool CanInsertAbove() => SelectedEntries.Length == 1;

        private void InsertAbove()
        {
            var firstSelected = SelectedEntries.FirstOrDefault();

            if (firstSelected == null)
                return;

            var index = _items.IndexOf(firstSelected);

            _items.Insert(index, CreateNewEntry());
        }

        private bool CanInsertBelow() => (SelectedEntries.Length == 1 || Items.Count == 0);

        private void InsertBelow()
        {
            if (Items.Count == 0)
            {
                _items.Add(CreateNewEntry());
                return;
            }

            var firstSelected = SelectedEntries.FirstOrDefault();

            if (firstSelected == null)
                return;

            var index = _items.IndexOf(firstSelected);

            _items.Insert(index + 1, CreateNewEntry());
        }

        private bool CanDelete() => SelectedEntries.Length > 0;

        private void Delete()
        {
            var selectedEntries = SelectedEntries;

            foreach (var entry in selectedEntries)
            {
                _items.Remove(entry);
            }
        }

        private void MoveUp()
        {
            foreach (var row in SelectedEntries)
            {
                var index = _items.IndexOf(row);

                _items.Move(index, index - 1);
            }
        }

        private bool CanMoveUp()
        {
            var selectedEntries = SelectedEntries;

            if (selectedEntries.Length == 0)
                return false;

            if (selectedEntries.Any(entry => _items.IndexOf(entry) == 0))
                return false;

            return true;
        }

        private void MoveDown()
        {
            foreach (var row in SelectedEntries.Reverse())
            {
                var index = _items.IndexOf(row);

                _items.Move(index, index + 1);
            }
        }

        private bool CanMoveDown()
        {
            var selectedEntries = SelectedEntries.ToArray();

            if (selectedEntries.Length == 0)
                return false;

            if (selectedEntries.Any(entry => _items.IndexOf(entry) >= _items.Count - 1))
                return false;

            return true;
        }

        /// <summary>
        /// Use this to bind the selected items in the data grid.
        /// </summary>
        public IList SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => SelectedEntries);
            }
        }

        /// <summary>
        /// Gets a typed collection of the selected entries.
        /// </summary>
        public TItemType[] SelectedEntries
        {
            get
            {
                //Make sure that we have something to work with here.
                if (SelectedItems == null)
                    return new TItemType[] { };

                //We use .OfType so that we avoid grabbing the "new line" row.
                return SelectedItems.OfType<TItemType>().OrderBy(entry => _items.IndexOf(entry)).ToArray();
            }
        }

        public ObservableCollection<TItemType> Items => _items;
    }
}
