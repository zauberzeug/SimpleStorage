using System;
using Newtonsoft.Json;

namespace PerpetualEngine.Storage
{
    public class JsonPersistingList<T> : PersistentList<T> where T : IIdentifiable
    {
        public JsonPersistingList(string editGroup) : base(
                editGroup,
                obj => {
                    var str = JsonConvert.SerializeObject(obj);
                    return str;
                },
                str => {
                    try {
                        return JsonConvert.DeserializeObject<T>(str);
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