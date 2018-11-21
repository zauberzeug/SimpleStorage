using System;
using System.Collections.Generic;
using System.Linq;

namespace PerpetualEngine.Storage
{
    /// <summary>
    /// Deskotp specific implementation to let EditGroup(string) return an DesktopSimpleStorage object
    /// </summary>
    public partial class SimpleStorage
    {
        static SimpleStorage()
        {
            SimpleStorage.EditGroup = name => new DesktopSimpleStorage(name);
        }
    }

    /// <summary>
    /// Not persistent right now... only used for unit tests
    /// </summary>
    public class DesktopSimpleStorage : SimpleStorage
    {
        static Dictionary<string, string> Database = new Dictionary<string, string>();

        static string lockObject = "lock";

        public DesktopSimpleStorage(string groupName) : base(groupName)
        {
        }

        public static void Clear()
        {
            lock (lockObject) {
                Database.Clear();
            }
        }

        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name = "key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        override public void Put(string key, string value)
        {
            if (value == null) {
                Delete(key);
                return;
            }

            lock (lockObject) {
                var id = Group + "_" + key;
                if (Database.ContainsKey(id))
                    Database.Remove(id);

                Database.Add(id, value);
            }
        }

        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        override public string Get(string key)
        {
            lock (lockObject) {
                var id = Group + "_" + key;
                return Database.ContainsKey(id) ? Database[id] : null;
            }
        }

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        override public void Delete(string key)
        {
            lock (lockObject) {
                Database.Remove(Group + "_" + key);
            }
        }

        public static List<string> StoredKeys {
            get {
                List<string> storedKeys;
                lock (lockObject) {
                    storedKeys = Database.Keys.ToList();
                }
                return storedKeys;
            }
        }
    }
}