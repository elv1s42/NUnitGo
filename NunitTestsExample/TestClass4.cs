using NUnit.Framework;

namespace NunitTestsExample
{
    [TestFixture]
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
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
