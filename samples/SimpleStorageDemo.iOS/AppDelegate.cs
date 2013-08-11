using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using PerpetualEngine.Storage;

namespace SimpleStorageDemo.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            // set the singleton creation to plattform specific instance; should alway be done in the AppDelegate
            SimpleStorage.EditGroup = (string groupName) => {
                return new iOSSimpleStorage(groupName);
            };

            // open a new storage group with name "Demo" -- thanks to the delegate above, this is even possible
            // in code which is shared between Android and iOS
            var storage = SimpleStorage.EditGroup("Demo");

            // loading key "app_launches" with an empty string as default value
            var appLaunches = storage.Load("app_launches", "").Split(',').ToList();

            // adding a new timestamp to list to show that SimpleStorage is working
            appLaunches.Add(DateTime.Now.ToString());

            // save the value with key "app_launches" for next application start
            storage.Save("app_launches", String.Join(",", appLaunches));

            // simple presentation of the timestamp list with MonoTouch.Dialog
            var section = new Section();
            section.AddAll(from l in appLaunches where !String.IsNullOrEmpty(l) select new StringElement(l));
            window.RootViewController = new DialogViewController(new RootElement("SimpleStorage Demo") {
                section
            });
            
            window.MakeKeyAndVisible();
            
            return true;
        }
    }
}

