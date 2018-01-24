using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PerpetualEngine.Storage
{
    [TestFixture]
    public class JsonPersistingListTests
    {
        string editGroup;

        [SetUp]
        public void Setup()
        {
            editGroup = Guid.NewGuid().ToString();
        }

        [Test]
        public void TestStoringAndLoading()
        {
            var list = new JsonPersistingList<TestItem>(editGroup);
            list.Add(new TestItem("A"));
            Assert.True(SimpleStorage.EditGroup(editGroup).HasKey("A"));
            Assert.AreEqual("{\"Id\":\"A\",\"Value\":\"a\"}", SimpleStorage.EditGroup(editGroup).Get("A"));

            list = new JsonPersistingList<TestItem>(editGroup);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("A", list.First().Id);
        }

        [Test]
        public void TestSkippingNonDeserializableEntries()
        {
            var list = new JsonPersistingList<TestItem>(editGroup);
            list.Add(new TestItem("1"));
            list.Add(new TestItem("2"));
            list.Add(new TestItem("3"));

            var storage = SimpleStorage.EditGroup(editGroup);
            storage.Put("2", "some bad data");

            list = new JsonPersistingList<TestItem>(editGroup);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("1", list.First().Id);
            Assert.AreEqual("3", list.Skip(1).First().Id);
        }

        [Test]
        public void AddAllTest()
        {
            var list = new JsonPersistingList<TestItem>(editGroup);
            int addedCalledCount = 0;
            list.Added += delegate {
                addedCalledCount++;
            };

            var items = new List<TestItem>{
                new TestItem("0"),
                new TestItem("1"),
            };
            list.Add(items);

            Assert.That(list.Count(), Is.EqualTo(2));
            Assert.That(addedCalledCount, Is.EqualTo(2));
            Assert.That(list[0].Id, Is.EqualTo("0"));
            Assert.That(list[1].Id, Is.EqualTo("1"));

            list.Add(new TestItem("2"), new TestItem("3"));
            Assert.That(list.Count(), Is.EqualTo(4));
            Assert.That(addedCalledCount, Is.EqualTo(4));
            Assert.That(list[2].Id, Is.EqualTo("2"));
            Assert.That(list[3].Id, Is.EqualTo("3"));
        }
    }
}
