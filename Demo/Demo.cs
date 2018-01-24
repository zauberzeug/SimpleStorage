using System;
using System.Collections.Generic;
using Demo.Performance;
using Xamarin.Forms;

namespace Demo
{
    public class App : Application
    {
        List<Measurement> measurements = new List<Measurement>{
            new AddMeasurement(100,10),
            new AddMeasurement(1000,10),
            new AddMeasurement(2000,10),
            new AddMeasurement(4000,10),
        };
        public App()
        {
            // The root page of your application
            var contentPage = new ContentPage {
                Title = "Demo",
            };
            var content = new StackLayout {
                VerticalOptions = LayoutOptions.Center,
            };

            foreach (var m in measurements)
                content.Children.Add(m.View);

            contentPage.Content = content;

            MainPage = new NavigationPage(contentPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
