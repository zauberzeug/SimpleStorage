using System;
using NUnit.Framework;

namespace PerpetualEngine.Storage
{
    [TestFixture()]
    public partial class SimpleStorageTests
    {
        [SetUp]
        public void Init()
        {
            SimpleStorage.EditGroup = (string groupName) => {
                return new iOSSimpleStorage(groupName);
            };
        }
    }
}

