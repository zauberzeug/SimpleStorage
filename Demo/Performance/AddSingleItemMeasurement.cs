using System;
using System.Threading.Tasks;
using PerpetualEngine.Storage;

namespace Demo.Performance
{
    public class AddSingleItemMeasurement : AddMeasurement
    {
        public AddSingleItemMeasurement(int count, int contentSize) : base(count, contentSize)
        {
            ButtonText = "SingleAdd (count=" + count + "; " + "size/object=" + contentSize + ")";
        }

        protected override void Execute()
        {
            foreach (var item in LocalMemoryItems)
                JsonPersistingList.Add(item);
        }
    }
}
