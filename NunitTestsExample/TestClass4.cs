using System;
using System.Threading;
using NUnit.Framework;
using NunitGo;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass4
    {
        [TestCase("0", 1, "11111111-1111-1111-1111-111111111141")]
        [TestCase("1", 1, "11111111-1111-1111-1111-111111111142")]
        [TestCase("2", 1, "11111111-1111-1111-1111-111111111143")]
        [TestCase("3", 4, "11111111-1111-1111-1111-111111111144")]
        [TestCase("4", 4, "11111111-1111-1111-1111-111111111145")]
        [TestCase("5", 4, "11111111-1111-1111-1111-111111111146")]
        [TestCase("6", 3, "11111111-1111-1111-1111-111111111147")]
        [TestCase("7", 7, "11111111-1111-1111-1111-111111111148")]
        [NunitGoAction]
        public void ParamTestName(string input, int expected, string guid)
        {
            NunitGoActionAttribute.TestGuid = new Guid(guid);
            Thread.Sleep(300);
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
