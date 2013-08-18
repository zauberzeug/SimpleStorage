using System;
using NUnit.Framework;
using PerpetualEngine.Storage;

namespace Tests
{
    [TestFixture]
    public class AsyncTests
    {
       
        [SetUp]
        public void SetUp()
        {
            SimpleStorage.EditGroup = (name) => {
                return new iOSSimpleStorage(name);
            };
        }

        [Test]
        public void TestLifeCycleOfStringValue()
        {
            var storage = SimpleStorage.EditGroup(this.GetType().ToString());
            Assert.IsFalse(storage.HasKeyAsync("test").Result);

            storage.PutAsync("test", "42").Wait();
            Assert.IsTrue(storage.HasKeyAsync("test").Result);
            Assert.That(storage.GetAsync("test").Result, Is.EqualTo("42"));

            storage.DeleteAsync("test").Wait();
            Assert.IsFalse(storage.HasKeyAsync("test").Result);
        }
    }
}