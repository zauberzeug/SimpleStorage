# Getting Started

The basic usage is very simple

    var storage = SimpleStorage.EditGroup("group name of key/value store");
    storage.Put("myKey", "some value");
    var value = storage.Get("myKey");

This component also provides async/await implementations (PutAsync, GetAsync, HasKeyAsync and DeleteAsync).
There are specialized implementations for Android and iOS which make use of the native Prefreneces APIs to store the values. Thanks to a static initialization and the impleStorage.EditGroup creator delegate you can use the above code also in files shared on both platforms. 

## Android Setup

On Android, we need the App context to use the shared preferences. Before you use SimpleStorage anywhere in your App, make sure you set the context with SimpleStorage.SetContext(). For example:

    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);
        
        SimpleStorage.SetContext(ApplicationContext);
        
        SetContentView(Resource.Layout.Main);
         
        // other code
    }