using System.Collections.ObjectModel;
using PerpetualEngine.Storage;

namespace PerpetualEngine.Storage
{
    public class PersistentObservableStrings : ObservableCollection<string>
    {
        readonly PersistentStrings persistence;

        public PersistentObservableStrings(string key)
        {
            persistence = new PersistentStrings(key);
            foreach (var item in persistence)
                Add(item);
        }

        protected override void InsertItem(int index, string item)
        {
            if (!persistence.Contains(item))
                persistence.Insert(index, item);

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (persistence.Contains(this[index]))
                persistence.Remove(this[index]);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            persistence.Clear();

            base.ClearItems();
        }

        public void ReplaceWith(string item)
        {
            Clear();
            Add(item);
        }

        public void ReplaceWith(Collection<string> items)
        {
            Clear();
            foreach (var item in items)
                Add(item);
        }
    }
}

