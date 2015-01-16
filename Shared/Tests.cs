using NUnit.Framework;
using System;

namespace PerpetualEngine.Storage
{
    [TestFixture()]
    public partial class SimpleStorageTests
    {

        [Test()]
        public void testSavingString()
        {
            var storage = SimpleStorage.EditGroup(Guid.NewGuid().ToString());
            storage.Put("test-key", "22");
            Assert.That("22", Is.EqualTo(storage.Get("test-key")));
        }
    }
}