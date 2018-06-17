using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using PerpetualEngine.Storage;

namespace PerpetualEngine.Storage
{
    public class JsonObservableCollection<T> : ObservableCollection<T> where T : IIdentifiable
    {
        readonly JsonPersistingList<T> persistence;

        public JsonObservableCollection(string key)
        {
            persistence = new JsonPersistingList<T>(key);
            foreach (var item in persistence)
                Items.Add(item);
        }

        public void AddAll(IEnumerable<T> items)
        {
            foreach (var item in items) {
                Items.Add(item);
                persistence.Add(item);
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void InsertItem(int index, T item)
        {
            persistence.Insert(index, item);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            persistence.Remove(this[index].Id);
            persistence.Insert(index, item);
            base.SetItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            persistence.Remove(this[index].Id);
            base.RemoveItem(index);
        }

        public void RemoveById(string id)
        {
            Remove(this.First(item => item.Id == id));
        }

        public void Update(T item)
        {
            persistence.Update(item.Id);
        }

        protected override void ClearItems()
        {
            persistence.Clear();
            base.ClearItems();
        }
    }
}

