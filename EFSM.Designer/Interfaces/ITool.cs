namespace EFSM.Designer.Interfaces
{
    public interface ITool : IHasCategory, INamedItem, IHasDescription
    {
        bool ShowInToolbox { get; }
    }
}
