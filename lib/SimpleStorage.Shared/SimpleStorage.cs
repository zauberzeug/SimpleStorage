using System;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace PerpetualEngine.Storage
{
    public abstract class SimpleStorage
    {

        protected string Group { get; set; }

        public SimpleStorage(string groupName)
        {
            Group = groupName;
        }

        /// <summary>
        /// plattform specific instance to save/load values within a given group
        /// </summary>
        /// <value>A delegate expecting a group name and returning a plattform specific variant of SimpleStorage</value>
        public static Func<string, SimpleStorage> EditGroup { get; set; }

        /// <summary>
        /// Speichert einen Wert.
        /// </summary>
        /// <param name="key">Idenifizierung</param>
        /// <param name="value">Der Wert</param>
        public abstract void Save(string key, string value);

        /// <summary>
        /// Läd einen abgespeicherten Wert.
        /// </summary>
        /// <param name="key">null, wenn kein Wert gespeichert ist, sonst der Wert</param>
        public abstract string Load(string key);

        public string Load(string key, string defaultValue)
        {
            var value = Load(key);
            if (value == null)
                return defaultValue;
            else
                return value;
        }

        public abstract void Delete(string key);

        public void Save(string key, DateTime? value)
        {
            if (value == null)
                Delete(key);
            var data = value.ToString();
            Save(key, data);
        }

        public void Save(string key, TimeSpan value)
        {
            var data = value.ToString();
            Save(key, data);
        }

        void Save(string key, object value)
        {

            var data = SerializeObject(value);
            Save(key, data);
        }

        Nullable<T> Load<T>(string key) where T : struct
        {
            throw new  NotImplementedException("has problem when reading string (http://stackoverflow.com/questions/8625705/xmlexception-text-node-cannot-appear-in-this-state)");
            var data = Load(key);
            Console.WriteLine("loading: " + data);
            if (data == null)
                return null;
            try {
                return DeserializeObject<T>(key);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public DateTime? LoadDateTime(string key)
        {
            var data = Load(key);
            if (data == null)
                return null;
            try {
                return DateTime.Parse(data);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public TimeSpan LoadTimeSpan(string key, TimeSpan defaultValue)
        {
            var data = Load(key);
            if (data == null)
                return defaultValue;
            try {
                return TimeSpan.Parse(data);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return defaultValue;
            }
        }

        static string SerializeObject<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            StringWriter textWriter = new StringWriter();

            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }

        T DeserializeObject<T>(string toDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(toDeserialize);

            return (T)xmlSerializer.Deserialize(stringReader);
        }
    }
}