using System;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace PerpetualEngine.Storage
{
    [TestFixture]
    public class SerializationTests
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
            (storage as DesktopSimpleStorage).Clear();
        }

        [Serializable]
        class ClassWithNonSerializableMembers: IDeserializationCallback
        {
            [field:NonSerialized]
            public event Action TestAction = delegate {};

            [NonSerialized]
            public int TestInteger = 1;

            public void ExecuteTestAction()
            {
                TestAction();
            }

            public void OnDeserialization(object sender)
            {
                TestAction = delegate {
                };
                TestInteger = 1;
            }
        }

        [Test]
        public void TestNonSerializableMemberInitialization()
        {
            storage.Put("test", new ClassWithNonSerializableMembers { TestInteger = 2 });

            var restored = storage.Get<ClassWithNonSerializableMembers>("test");
            Assert.AreEqual(1, restored.TestInteger);

            Assert.DoesNotThrow(restored.ExecuteTestAction);
        }
    }
}

