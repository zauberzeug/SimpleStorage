using System;
using System.Collections.Generic;

namespace PerpetualEngine.Storage
{
    /// <summary>
    /// Deskotp specific implementation to let EditGroup(string) return an DesktopSimpleStorage object
    /// </summary>
    public partial class SimpleStorage
    {
        static SimpleStorage()
        {
            SimpleStorage.EditGroup = (name) => {
                return new DesktopSimpleStorage(name);
            };
        }
    }

    /// <summary>
    /// Does not persistent right now... only used for unit tests
    /// </summary>
    public class DesktopSimpleStorage : SimpleStorage
    {
        Dictionary<string,string> database = new Dictionary<string,string>();

        public DesktopSimpleStorage(string groupName) : base(groupName)
        {
        }

        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name="value">if value is null, the key will be deleted</param>
        override public void Put(string key, string value)
        {
            if (value == null) 
                Delete(key);
            else 
                database.Add(Group + "_" + key, value);
        }

        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        override public string Get(string key)
        {
            return database[Group + "_" + key];
        }

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        override public void Delete(string key)
        {
            database.Remove(Group + "_" + key);
        }
    }
}