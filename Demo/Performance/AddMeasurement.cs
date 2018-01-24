using System;
using System.Collections.Generic;
using PerpetualEngine.Storage;
using Xamarin.Forms;

namespace Demo.Performance
{
    public class AddMeasurement : Measurement
    {
        Button button;
        Label label;
        View view;
        int count;
        int contentSize;

        public AddMeasurement(int count, int contentSize)
        {
            this.count = count;
            this.contentSize = contentSize;
        }

        public override View View {
            get {
                if (view == null)
                    view = CreateUI();
                return view;
            }
        }

        View CreateUI()
        {
            var layout = new StackLayout();
            button = new Button {
                Text = @"Add (count=" + count + "; " + "size/object=" + contentSize + ")",
                Command = new Command(Run),
            };
            label = new Label();
            layout.Children.Add(button);
            layout.Children.Add(label);
            return layout;
        }

        List<TestItem> CreateTestItems()
        {
            var list = new List<TestItem>();
            for (int i = 0; i < count; i++)
                list.Add(new TestItem(Guid.NewGuid().ToString(), contentSize));
            return list;
        }

        void Run()
        {
            var persistentList = new JsonPersistingList<TestItem>("asdf");
            var list = CreateTestItems();
            var startTime = DateTime.Now;
            foreach (var item in list)
                persistentList.Add(item);

            var msNeeded = (DateTime.Now - startTime).TotalMilliseconds;
            label.Text = "needed time (ms) : " + msNeeded;
            persistentList.Clear();
        }
    }
}
