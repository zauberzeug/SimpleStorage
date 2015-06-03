using System;
using System.Linq;
using NUnit.Framework;

namespace PerpetualEngine.Storage
{
    [TestFixture]
    public class PersistentStringsTest
    {
        string editGroup;

        [SetUp]
        public void Setup()
        {
            editGroup = Guid.NewGuid().ToString();
        }

        PersistentStrings BuildTestList()
        {
            var list = new PersistentStrings(editGroup);
            list.Add("0");
            list.Add("1");
            list.Add("2");
            return list;
        }

        [Test]
        public void TestLoadingStringsFromPersistence()
        {
            BuildTestList();
            var list = new PersistentStrings(editGroup);
            for (var i = 0; i < list.Count; i++)
                Assert.That(list[i], Is.EqualTo(i.ToString()));
            Assert.That(list.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestInsertingItems()
        {
            var list = BuildTestList();
            list.Insert(1, "x");
            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(list[1], Is.EqualTo("x"));

            list.Insert(0, "y");
            Assert.That(list.Count, Is.EqualTo(5));
            Assert.That(list.First(), Is.EqualTo("y"));
        }

    }
}

