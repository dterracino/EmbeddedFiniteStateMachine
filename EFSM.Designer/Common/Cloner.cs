using Newtonsoft.Json;

namespace EFSM.Designer.Common
{
    public static class Cloner
    {
        public static T Clone<T>(this T item)
           where T : class
        {
            if (item == null)
                return null;

            var serializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string serialized = JsonConvert.SerializeObject(item, serializerSettings);

            return (T)JsonConvert.DeserializeObject(serialized, serializerSettings);
        }
    }
}
