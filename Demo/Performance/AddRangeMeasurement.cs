using System;
using PerpetualEngine.Storage;
using Xamarin.Forms;

namespace Demo.Performance
{
    public class AddRangeMeasurement : AddMeasurement
    {
        public AddRangeMeasurement(int count, int contentSize) : base(count, contentSize)
        {
            ButtonText = "AddRange (count=" + count + "; " + "size/object=" + contentSize + ")";
        }

        protected override void Execute()
        {
            JsonPersistingList.Add(LocalMemoryItems);
        }
    }
}
