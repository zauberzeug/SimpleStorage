using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PerpetualEngine.Storage
{
    public class PersistentStrings: IEnumerable, IEnumerable<string>
    {
        SimpleStorage storage;
        readonly List<string> list;
        const string key = "persistentStrings";

        public PersistentStrings(string groupName)
        {
            storage = SimpleStorage.EditGroup(groupName);
            list = storage.Get<List<string>>(key) ?? new List<string>();
        }

        public int Count { get { return list.Count; } }

        public bool IsEmpty { get { return list.Count == 0; } }

        public virtual void Insert(int index, string value)
        {
            list.Insert(index, value);
            storage.Put(key, list);
        }

        public virtual void Add(string value)
        {
            Insert(list.Count, value);
        }

        public virtual void Remove(string item)
        {
            list.Remove(item);
            storage.Put(key, list);
        }

        public virtual void Clear()
        {
            storage.Delete(key);
            list.Clear();
        }

        public string ElementAt(int index)
        {
            return list.ElementAt(index);
        }

        public bool Contains(string id)
        {
            return list.Contains(id);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public IEnumerable<string> Reverse()
        {
            for (int i = list.Count - 1; i >= 0; i--)
                yield return list[i];
        }

        public string this[int index] { get { return list[index]; } }
    }
}

