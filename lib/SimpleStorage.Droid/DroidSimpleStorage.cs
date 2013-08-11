using System;
using Android.Content;

namespace PerpetualEngine.Storage
{
    public class DroidSimpleStorage : SimpleStorage
    {
        ISharedPreferences Prefs{ get; set; }

        public DroidSimpleStorage(string groupName, Context c) :base (groupName)
        {
            Prefs = c.GetSharedPreferences(groupName, FileCreationMode.Private);
        }

        public override string Load(string key)
        {
            return Prefs.GetString(key, null);
        }

        public override void Save(string key, string value)
        {
            Prefs.Edit().PutString(key, value).Commit();
        }

        public override void Delete(string key)
        {
            Prefs.Edit().PutString(key, null).Commit();
        }
    }
}

