namespace EFSM.Designer.Interfaces
{
    public interface IUndoService<TState> where TState : class
    {
        TState Undo();

        bool CanUndo();

        TState Redo();

        bool CanRedo();

        void Do(TState state);

        void Clear(TState initialState);
    }
}
