using System.Threading;
using NUnit.Framework;

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

        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(500);
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestMethod3()
        {
            Thread.Sleep(500);
            Assert.AreEqual(1, 1);
        }
    }
}
