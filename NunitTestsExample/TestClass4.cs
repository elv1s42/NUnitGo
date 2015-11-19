using System.Threading;
using NUnit.Framework;
using NunitGo;

namespace NunitTestsExample
{
    [TestFixture]
    [NunitGoAction]
    public class TestClass4
    {
        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 4)]
        [TestCase("4", 4)]
        [TestCase("5", 4)]
        [TestCase("6", 3)]
        [TestCase("7", 7)]
        public void ParamTestName(string input, int expected)
        {
            Thread.Sleep(300);
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
