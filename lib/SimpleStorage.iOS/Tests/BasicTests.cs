using System;
using NUnit.Framework;
using PerpetualEngine.Storage;
using System.Collections.Generic;

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

		[Test]
		public void TestGettingDefaultList()
		{
			var storage = SimpleStorage.EditGroup(this.GetType().ToString());
			var result = storage.Get<List<string>>("test", new List<string>());
			foreach (var i in result)
				Assert.Fail ();
			Assert.AreEqual(0, result.Count);
		}
    }
}