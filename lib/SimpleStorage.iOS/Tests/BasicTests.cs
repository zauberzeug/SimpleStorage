using System;
using NUnit.Framework;
using PerpetualEngine.Storage;

namespace Tests
{
    [TestFixture]
    public class BasicTests
    {
       

        [Test]
        public void TestLifeCycleOfStringValue()
        {
            var storage = SimpleStorage.EditGroup(this.GetType().ToString());
            Assert.IsFalse(storage.HasKey("test"));

            storage.Put("test", "42");
            Assert.IsTrue(storage.HasKey("test"));
            Assert.That(storage.Get("test"), Is.EqualTo("42"));

            storage.Delete("test");
            Assert.IsFalse(storage.HasKey("test"));
        }

        [Test]
        public void TestDeletingStringBySettingNull()
        {
            var storage = SimpleStorage.EditGroup(this.GetType().ToString());
            Assert.IsFalse(storage.HasKey("test"));

            storage.Put("test", "42");
            Assert.IsTrue(storage.HasKey("test"));
            Assert.That(storage.Get("test"), Is.EqualTo("42"));

            storage.Put("test", null);
            Assert.IsFalse(storage.HasKey("test"));
        }
    }
}