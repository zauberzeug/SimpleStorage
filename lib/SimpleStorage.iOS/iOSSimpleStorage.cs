using System;
using MonoTouch.Foundation;

namespace PerpetualEngine.Storage
{
    /// <summary>
    /// iOS specific implementation to let EditGroup(string) return a n iOSSimpleStorage object
    /// </summary>
    public partial class SimpleStorage
    {
        static SimpleStorage()
        {
            SimpleStorage.EditGroup = (name) => {
                return new iOSSimpleStorage(name);
            };
        }
    }

    public class iOSSimpleStorage : SimpleStorage
    {
        public iOSSimpleStorage(string groupName) : base(groupName)
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
                NSUserDefaults.StandardUserDefaults.SetValueForKey(new NSString(value), new NSString(Group + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        override public string Get(string key)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(Group + "_" + key);
        }

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        override public void Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(new NSString(Group + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}