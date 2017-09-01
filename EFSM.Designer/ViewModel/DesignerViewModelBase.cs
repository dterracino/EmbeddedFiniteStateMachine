using Autofac;
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
        private bool _isSelected;
        private readonly ISelectionService _selectionService;
        private bool _isSelectionBoxVisible;
        private Rect _selectionBox;
        //private readonly IIsDirtyService _dirtyService;

        protected DesignerViewModelBase(/*IIsDirtyService dirtyService = null,*/ ISelectionService selectionService = null)
        {
            _selectionService = selectionService ?? ApplicationContainer.Container.Resolve<ISelectionService>();
            //_dirtyService = dirtyService ?? new IsDirtyService();

            DeleteCommand = new RelayCommand(Delete, CanDelete);
        }

        public ICommand DeleteCommand { get; private set; }

        protected IEnumerable<IDeleteable> GetSelectedDeleteables()
        {
            return _selectionService.GetSelected().OfType<IDeleteable>();
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

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

        //public IIsDirtyService DirtyService
        //{
        //    get { return _dirtyService; }
        //}

        public ISelectionService SelectionService
        {
            get { return _selectionService; }
        }

        /// <summary>
        /// Override this to provide undo / redo capabilities.
        /// </summary>
        public virtual IUndoProvider UndoProvider
        {
            get { return null; }
        }

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
    }
}
