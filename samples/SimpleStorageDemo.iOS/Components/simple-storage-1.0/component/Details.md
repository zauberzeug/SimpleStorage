# Getting Started with .

The basic usage is very simple

    var storage = SimpleStorage.EditGroup("group name of key/value store");
    var storage.Put("myKey", "some value");
    var value = storage.Get("myKey");

There are implementations for Android and iOS, but you can use the above code also in files shared on both platforms. To make this possible, SimpleStorage.EditGroup is a delegate which you need to set when the App launches:

## iOS
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            SimpleStorage.EditGroup = (string groupName) => {
                return new iOSSimpleStorage(groupName);
            };
	    
            // other code
       }

### Android
    protected override void OnCreate(Bundle bundle)
    {
       base.OnCreate(bundle);

       SimpleStorage.EditGroup = (string groupName) => {
           return new DroidSimpleStorage(groupName, this);
       };

       SetContentView(Resource.Layout.Main);

       // other code

   }