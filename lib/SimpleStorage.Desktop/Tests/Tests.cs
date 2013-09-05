using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace PerpetualEngine.Storage
{
    [TestFixture()]
    public class Tests
    {
        SimpleStorage storage;

        [SetUp]
        public void SetUp()
        {
            storage = SimpleStorage.EditGroup(Guid.NewGuid().ToString());
        }

        [TearDown]
        public void TearDown()
        {
            storage.Clear();
        }

        [Test]
        public void TestSavingSimpleArray()
        {
            string[] test = {"a", "b"};
            storage.Put("test", test);
            var result = storage.Get<string[]>("test");
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
        }

        [Test]
        public void TestSavingDateTime()
        {
            var time = DateTime.Now;
            storage.Put("test", time);
            var result = storage.Get<DateTime>("test");
            Assert.AreEqual(time, result);
        }

        [Test]
        public void TestSavingTimeSpan()
        {
            var time = TimeSpan.FromMinutes(122);
            storage.Put("test", time);
            var result = storage.Get<TimeSpan>("test");
            Assert.AreEqual(122.0, result.TotalMinutes);
        }

        [Test]
        public void TestSavingList()
        {
            var test = new List<string>() {"a", "b"};
            storage.Put("test", test);
            var result = storage.Get<List<string>>("test");
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
        }

        [Test]
        public void TestLoadingDefaultString()
        {
            var result = storage.Get<string>("test", "default string");
            Assert.AreEqual("default string", result);
        }

        [Test]
        public void TestLoadingDefaultTimeSpan()
        {
            var result = storage.Get<TimeSpan>("test", TimeSpan.FromMinutes(1));
            Assert.AreEqual(TimeSpan.FromSeconds(60), result);
        }

    }
}

