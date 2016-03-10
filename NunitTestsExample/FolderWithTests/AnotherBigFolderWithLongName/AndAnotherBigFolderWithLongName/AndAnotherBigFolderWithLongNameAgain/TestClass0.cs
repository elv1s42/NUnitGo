using System.Threading;
using NunitGoCore;
using NunitGoCore.Attributes;
using NUnit.Framework;

namespace NunitTestsExample.FolderWithTests.AnotherBigFolderWithLongName.AndAnotherBigFolderWithLongName.AndAnotherBigFolderWithLongNameAgain
{
    [TestFixture]
    public class TestClass0
    {
        [Test, NunitGoAction("11111111-1111-1111-1111-111111111000", "Project1", "Subsystem1", "Very long namespace test")]
        public void LongNamespaceTest()
        {
            NunitGo.EventStarted("Checking something");
            Thread.Sleep(500);
            NunitGo.EventFinished("Checking something");
            Assert.AreEqual(1, 1);
        }
    }
}
