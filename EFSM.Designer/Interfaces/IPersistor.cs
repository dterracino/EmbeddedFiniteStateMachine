using EFSM.Domain;

namespace EFSM.Designer.Interfaces
{
    public interface IPersistor
    {
        StateMachineProject Create();

        StateMachineProject LoadProject(string path);

        void SaveProject(StateMachineProject metadata, string path);
    }
}
