using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace PerpetualEngine.Storage
{
    [TestFixture]
    public class JsonPersistingListTests
    {
        const string key = "strings";

        string CollectionString {
            get { return string.Join(" ", new JsonPersistingList<TestItem>(key)); }
        }

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
            new JsonPersistingList<TestItem>(key).Clear();
        }

        [Test]
        public void TestStoringAndLoading()
        {
            var list = new JsonPersistingList<TestItem>(key);
            list.Add(new TestItem("A"));
            Assert.True(SimpleStorage.EditGroup(key).HasKey("A"));
            Assert.AreEqual("{\"Id\":\"A\",\"Value\":\"a\"}", SimpleStorage.EditGroup(key).Get("A"));

            list = new JsonPersistingList<TestItem>(key);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("A", list.First().Id);
        }

        [Test]
        public void TestSkippingNonDeserializableEntries()
        {
            var list = new JsonPersistingList<TestItem>(key);
            list.Add(new TestItem("1"));
            list.Add(new TestItem("2"));
            list.Add(new TestItem("3"));

            var storage = SimpleStorage.EditGroup(key);
            storage.Put("2", "some bad data");

            list = new JsonPersistingList<TestItem>(key);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("1", list.First().Id);
            Assert.AreEqual("3", list.Skip(1).First().Id);
        }

        [Test]
        public void AddAllTest()
        {
            var list = new JsonPersistingList<TestItem>(key);
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

        [Test]
        public void CRUDTests()
        {
            var collection = new JsonPersistingList<TestItem>(key) {
                new TestItem("A"),
                new TestItem("B"),
                new TestItem("C"),
            };
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Cc"));

            collection.Add(new TestItem("D"));
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Cc Dd"));

            collection.Insert(2, new TestItem("E"));
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Ee Cc Dd"));

            collection.Remove("C");
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Ee Dd"));

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => collection.Remove("C"));

            collection.Update(new TestItem("E", "q"));
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Eq Dd"));

            collection.Add(new List<TestItem> { new TestItem("X"), new TestItem("Y"), new TestItem("Z") });
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Eq Dd Xx Yy Zz"));

            collection.Add(new TestItem("x"), new TestItem("y"), new TestItem("z"));
            Assert.That(CollectionString, Is.EqualTo("Aa Bb Eq Dd Xx Yy Zz xx yy zz"));

            Assert.Throws(typeof(ApplicationException), () => collection.Add(new TestItem("X", "1"), new TestItem("W", "2")));

            collection.Clear();
            Assert.That(CollectionString, Is.EqualTo(""));
        }

        /// <summary>
        /// Tests passing JSON settings with the example of auto type name handling.
        /// </summary>
        [Test]
        public void JsonSettingsTests()
        {
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto };
            var list = new JsonPersistingList<JsonTestItem>("types", settings);
            list.Add(new JsonTestItem("A", "Value", new JsonTestContent("Content")));

            list = new JsonPersistingList<JsonTestItem>("types", settings);
            Assert.That(list[0].Id, Is.EqualTo("A"));
            Assert.That(list[0].Value, Is.EqualTo("Value"));
            Assert.That((list[0].Content as JsonTestContent).Value, Is.EqualTo("Content"));
            list.Clear();

            var list2 = new JsonPersistingList<TestItem>("types2", settings, typeof(TestItem));
            list2.Add(new JsonTestItem("B", "Value2", new JsonTestContent("Content2")));

            list2 = new JsonPersistingList<TestItem>("types2", settings, typeof(TestItem));
            Assert.That(list2[0].Id, Is.EqualTo("B"));
            Assert.That(list2[0].Value, Is.EqualTo("Value2"));
            Assert.That(((list2[0] as JsonTestItem).Content as JsonTestContent).Value, Is.EqualTo("Content2"));
            list2.Clear();
        }

        class JsonTestItem : TestItem
        {
            public JsonTestItem(string id, string value, object content) : base(id, value)
            {
                Content = content;
            }

            public object Content { get; set; }
        }

        class JsonTestContent
        {
            public JsonTestContent(string value)
            {
                Value = value;
            }

            public string Value { get; set; }
        }
    }
}
