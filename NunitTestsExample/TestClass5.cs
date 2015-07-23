using NUnit.Framework;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass5
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


        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        public void ParamTestName(string input, int expected)
        {
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
