using System;
using Android.Content;

namespace PerpetualEngine.Storage
{
    /// <summary>
    /// Android specific implementation to let EditGroup(string) return a n iOSSimpleStorage object
    /// </summary>
    public partial class SimpleStorage
    {
        /// <summary>
        /// Before using SimpleStorage somwhere in your Android App, make sure you call this method to set the context
        /// </summary>
        /// <param name="context">App Context.</param>
        public static void SetContext(Context context)
        {
            SimpleStorage.EditGroup = (name) => {
                return new DroidSimpleStorage(name, context);
            };
        }
    }

    public class DroidSimpleStorage : SimpleStorage
    {
        ISharedPreferences Prefs{ get; set; }

        public DroidSimpleStorage(string groupName, Context c) :base (groupName)
        {
            Prefs = c.GetSharedPreferences(groupName, FileCreationMode.Private);
        }

        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        public override string Get(string key)
        {
            return Prefs.GetString(key, null);
        }

        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name="value">if value is null, the key will be deleted</param>
        public override void Put(string key, string value)
        {
            Prefs.Edit().PutString(key, value).Commit();
        }

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        public override void Delete(string key)
        {
            Prefs.Edit().PutString(key, null).Commit();
        }
    }
}

