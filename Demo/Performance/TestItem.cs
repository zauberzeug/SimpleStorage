using System;
using PerpetualEngine.Storage;

namespace Demo.Performance
{
    public class TestItem : IIdentifiable
    {
        public string Id { get; private set; }
        public string[] Content;

        public TestItem(string id, int contentSize)
        {
            Id = id;
            Content = new string[contentSize];
        }
    }
}
