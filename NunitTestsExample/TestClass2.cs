using System.Threading;
using NUnit.Framework;
using NunitGo;

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
        public void TestMethod2()
        {
            Thread.Sleep(500);
            Assert.AreEqual(1, 1);
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111123", "Project1", "Subsystem2"), Category("SuccessCategory")]
        public void TestMethod3()
        {
            Thread.Sleep(500);
            Assert.AreEqual(1, 1);
        }
    }
}
