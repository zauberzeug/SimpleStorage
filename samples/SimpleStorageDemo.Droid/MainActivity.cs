using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PerpetualEngine.Storage;

namespace SimpleStorageDemo.Droid
{
    [Activity (Label = "SimpleStorageDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // set the singleton creation to plattform specific instance; should alway be done in the AppDelegate
            SimpleStorage.EditGroup = (string groupName) => {
                return new DroidSimpleStorage(groupName, this);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            // open a new storage group with name "Demo" -- thanks to the delegate defined in OnCreate,
            // this is even possible in code which is shared between Android and iOS
            var storage = SimpleStorage.EditGroup("Demo");

            // loading key "app_launches" with an empty string as default value
            var appLaunches = storage.Load("app_launches", "").Split(',').ToList();

            // adding a new timestamp to list to show that SimpleStorage is working
            appLaunches.Add(DateTime.Now.ToString());

            // save the value with key "app_launches" for next application start
            storage.Save("app_launches", String.Join(",", appLaunches));

            // simple presentation of the timestamp list
            var list = FindViewById<LinearLayout>(Resource.Id.list);
            foreach (var appLaunch in appLaunches) {
                if (String.IsNullOrEmpty(appLaunch))
                    continue;
                var textView = new TextView(this);
                textView.Text = appLaunch;
                textView.TextSize = 35;
                list.AddView(textView);
            }
        }
    }
}


