using System;
using NUnit.Framework;
using PerpetualEngine.Storage;
using System.Runtime.Serialization;

namespace Tests
{
    [TestFixture()]
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
            storage.Clear();
        }

        [Serializable]
        class ClassWithNonSerializableMembers :IDeserializationCallback
        {
            [field:NonSerialized]
            public event Action TestAction = delegate {};

            [NonSerialized]
            public int TestInteger = 1;

            public void ExecuteTestAction()
            {
                TestAction();
            }

            #region IDeserializationCallback implementation

            public void OnDeserialization(object sender)
            {
                TestAction = delegate {
                };
                TestInteger = 1;
            }

            #endregion

        }

        [Test]
        public void TestNonSerializableMemberInitialization()
        {
            storage.Put("test", new ClassWithNonSerializableMembers() { TestInteger = 2 });

            var restored = storage.Get<ClassWithNonSerializableMembers>("test");
            Assert.AreEqual(1, restored.TestInteger);

            Assert.DoesNotThrow(() => { 
                restored.ExecuteTestAction();
            });
        }
    }
}

