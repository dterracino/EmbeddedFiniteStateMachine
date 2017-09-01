using EFSM.Designer.Interfaces;
using EFSM.Domain;
using Newtonsoft.Json;
using System.IO;

namespace EFSM.Designer.Engine
{
    public class FilePersistor : IPersistor
    {
        public StateMachineProject LoadProject(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new StateMachineProject();
            }

            return JsonConvert.DeserializeObject<StateMachineProject>(File.ReadAllText(path));
        }

        public void SaveProject(StateMachineProject project, string path)
        {
            string json = JsonConvert.SerializeObject(project, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}
