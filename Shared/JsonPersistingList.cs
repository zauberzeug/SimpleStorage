using System;
using Newtonsoft.Json;

namespace PerpetualEngine.Storage
{
    public class JsonPersistingList<T> : PersistentList<T> where T : IIdentifiable
    {
        /// <param name="autoType">Type for Newtonsoft.Json TypeNameHandling.Auto.</param>
        public JsonPersistingList(string editGroup, JsonSerializerSettings settings = null, Type autoType = null) : base(
                editGroup,
                obj => {
                    var str = JsonConvert.SerializeObject(obj, autoType, settings);
                    return str;
                },
                str => {
                    try {
                        return JsonConvert.DeserializeObject<T>(str, settings);
                    } catch (Exception e) {
                        Console.WriteLine("could not deserialize '" + str + "'");
                        Console.WriteLine(e.GetType() + ": " + e.Message);
                        return default(T);
                    }
                })
        {
        }
    }
}