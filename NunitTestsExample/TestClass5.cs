using System.Threading;
using NUnit.Framework;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass5
    {
        [Test]
        public void TestMethod1()
        {
            Thread.Sleep(600);
            Assert.AreEqual(1, 2);
        }

        [Test, Category("SuccessCategory")]
        public void TestMethod2()
        {
            Thread.Sleep(100);
            Assert.AreEqual(1, 1);
        }
        
        [TestCase("0", 1)]
        [TestCase("1", 1), Category("SuccessCategory")]
        [TestCase("2", 1, TestName = "Testing name attribute")]
        public void ParamTestName(string input, int expected)
        {
            Thread.Sleep(500);
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
