using System.Threading;
using NUnit.Framework;
using NunitGoCore.Attributes;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass2
    {
        [Test]
        public void TestMethod1()
        {
            Thread.Sleep(500);
            Assert.AreEqual(1, 2);
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111122", "Project1", "Subsystem1"), Category("SuccessCategory")]
        [SingleTestSubscription(FullPath = "SubscriptionSingle.xml", UnsuccessfulOnly = false)]
        public void TestSingleSubscriptionFromXml()
        {
            Thread.Sleep(200);
            Assert.AreEqual(1, 1);
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111123", "Project1", "Subsystem2"), Category("SuccessCategory")]
        [Subscription(FullPath = "SubscriptionMulti.xml", UnsuccessfulOnly = false)]
        public void TestSubscriptionFromXml()
        {
            Thread.Sleep(100);
            Assert.AreEqual(1, 1);
        }
    }
}
