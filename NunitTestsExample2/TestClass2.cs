using NUnit.Framework;
using NunitGo;

namespace NunitTestsExample2
{
    [TestFixture, Ignore("Ignored test fixture")]
    [NunitGoAction]
    public class TestClass2
    {
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
