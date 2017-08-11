using System.IO;
using EFSM.Domain;
using Newtonsoft.Json;

namespace EFSM.Generator
{
    public static class ProjectFactory
    {
        public static StateMachineProject LoadFromFile(string path)
        {
            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<StateMachineProject>(json);
        }

        public static void SaveToFile(this StateMachineProject project, string path)
        {
            string json = JsonConvert.SerializeObject(project, Formatting.Indented);

            File.WriteAllText(path, json);
        }
    }
}