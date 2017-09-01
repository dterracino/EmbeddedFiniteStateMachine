using System;

namespace EFSM.Designer.Interfaces
{
    public interface ISelectionService
    {
        event EventHandler SelectionChanged;

        void SelectNone();

        ISelectable[] GetSelected();

        void Select(ISelectable selectable);

        void Unselect(ISelectable selectable);
    }
}
