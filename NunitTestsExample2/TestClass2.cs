using NUnit.Framework;

namespace NunitTestsExample2
{
    [TestFixture, Ignore("Ignored test fixture")]
    public class TestClass2
    {
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 2);
        }

        [Test, Category("SuccessCategory")]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
