using Autofac;
using Cas.Common.WPF;
using Cas.Common.WPF.Interfaces;
using EFSM.Designer.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public abstract class DesignerViewModelBase : ViewModelBase, ISelectable, IAreaSelector
    {
        public ICommand OnPreviewMouseLeftButtonUpCommand { get; private set; }
        public ICommand OnPreviewMouseRightButtonUpCommand { get; private set; }
        public ICommand OnDropCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private bool _isSelected;
        private readonly ISelectionService _selectionService;
        private bool _isSelectionBoxVisible;
        private Rect _selectionBox;
        private readonly IDirtyService _dirtyService;

        protected DesignerViewModelBase(IDirtyService dirtyService = null, ISelectionService selectionService = null)
        {
            _selectionService = selectionService ?? ApplicationContainer.Container.Resolve<ISelectionService>();
            _dirtyService = dirtyService ?? new DirtyService();

            InitiateCommands();
        }

        private void InitiateCommands()
        {
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            OnPreviewMouseLeftButtonUpCommand = new RelayCommand<MouseEventArgs>(OnPreviewMouseLeftButtonUp);
            OnPreviewMouseRightButtonUpCommand = new RelayCommand<MouseEventArgs>(OnPreviewMouseRightButtonUp);
            OnDropCommand = new RelayCommand<DragEventArgs>(OnDrop);
        }

        protected IEnumerable<IDeleteable> GetSelectedDeleteables() => _selectionService.GetSelected().OfType<IDeleteable>();

        public virtual bool IsReadOnly => false;

        protected virtual void Delete()
        {
            var deletables = GetSelectedDeleteables().ToArray();

            if (deletables.Any())
            {
                foreach (var deletable in GetSelectedDeleteables())
                {
                    deletable.Delete();
                }

                SelectionService.SelectNone();

                UndoProvider?.SaveUndoState();
            }
        }

        protected virtual bool CanDelete()
        {
            if (IsReadOnly)
                return false;

            return GetSelectedDeleteables().Any();
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public IDirtyService DirtyService => _dirtyService;

        public ISelectionService SelectionService => _selectionService;

        /// <summary>
        /// Override this to provide undo / redo capabilities.
        /// </summary>
        public virtual IUndoProvider UndoProvider => null;

        public bool IsSelectionBoxVisible
        {
            get { return _isSelectionBoxVisible; }
            set
            {
                _isSelectionBoxVisible = value;
                RaisePropertyChanged();
            }
        }

        public Rect SelectionBox
        {
            get { return _selectionBox; }
            set
            {
                _selectionBox = value;
                RaisePropertyChanged();
            }
        }

        public void BeginBoxSelection(Rect rect)
        {
            IsSelectionBoxVisible = true;
            SelectionBox = rect;
        }

        public void ContinueBoxSelection(Rect rect)
        {
            IsSelectionBoxVisible = true;
            SelectionBox = rect;
        }

        public void CancelBoxSelection()
        {
            IsSelectionBoxVisible = false;
        }

        public void CompleteBoxSelection(Rect rect)
        {
            IsSelectionBoxVisible = false;
            SelectionBox = rect;

            SelectBox(rect);
        }

        public virtual void SelectBox(Rect rec)
        {
        }

        public virtual void OnDrop(DragEventArgs e)
        {
        }

        public virtual void OnPreviewMouseLeftButtonUp(MouseEventArgs e)
        {
        }

        public virtual void OnPreviewMouseRightButtonUp(MouseEventArgs e)
        {
        }
    }
}
