using EFSM.Designer.Interfaces;
using EFSM.Domain;
using Newtonsoft.Json;
using System.IO;

namespace EFSM.Designer.Engine
{
    public class FilePersistor : IPersistor
    {
        public StateMachineProject Create()
        {
            var project = new StateMachineProject();

            project.Massage();

            return project;
        }

        public StateMachineProject LoadProject(string path)
        {
            var project = JsonConvert.DeserializeObject<StateMachineProject>(File.ReadAllText(path));

            project.Massage();

            return project;
        }

        public void SaveProject(StateMachineProject project, string path)
        {
            string json = JsonConvert.SerializeObject(project, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}
