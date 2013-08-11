using System;
using MonoTouch.Foundation;

namespace PerpetualEngine.Storage
{
    public class iOSSimpleStorage : SimpleStorage
    {
        public iOSSimpleStorage(string groupName) : base(groupName)
        {
        }

        override public void Put(string key, string value)
        {
            if (value == null) 
                Delete(key);
            else 
                NSUserDefaults.StandardUserDefaults.SetValueForKey(new NSString(value), new NSString(Group + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        override public string Get(string key)
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey(Group + "_" + key);
        }

        override public void Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(new NSString(Group + "_" + key));
        }
    }
}