using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PerpetualEngine.Storage;
using Xamarin.Forms;

namespace Demo.Performance
{
    public abstract class AddMeasurement : Measurement
    {
        Button button;
        protected Label label;
        View view;
        int count;
        int contentSize;
        protected string ButtonText;

        protected JsonPersistingList<TestItem> JsonPersistingList { get; set; }
        protected List<TestItem> LocalMemoryItems { get; set; }

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
                Text = ButtonText,
                Command = new Command(Run),
            };
            label = new Label();
            layout.Children.Add(button);
            layout.Children.Add(label);
            return layout;
        }

        protected List<TestItem> CreateTestItems()
        {
            var list = new List<TestItem>();
            for (int i = 0; i < count; i++)
                list.Add(new TestItem(Guid.NewGuid().ToString(), contentSize));
            return list;
        }

        void Run()
        {
            JsonPersistingList = new JsonPersistingList<TestItem>("asdf");
            if (!JsonPersistingList.IsEmpty)
                throw new ArgumentException("List should be empty");
            LocalMemoryItems = CreateTestItems();
            var startTime = DateTime.Now;

            Execute();

            var msNeeded = (DateTime.Now - startTime).TotalMilliseconds;
            label.Text = "needed time (ms) : " + msNeeded;
            JsonPersistingList.Clear();
        }

        protected abstract void Execute();
    }
}
