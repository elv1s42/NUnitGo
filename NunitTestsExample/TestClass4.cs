using System;
using System.Threading;
using NUnit.Framework;
using NunitGoCore.Attributes;

namespace NunitTestsExample
{
    [TestFixture]
    public class ParamTestClass
    {
        [TestCase("0", 1, "11111111-1111-1111-1111-111111111141", TestName = "Test 1")]
        [TestCase("1", 1, "11111111-1111-1111-1111-111111111142", TestName = "Test 2")]
        [TestCase("2", 1, "11111111-1111-1111-1111-111111111143", TestName = "Test 3")]
        [TestCase("3", 4, "11111111-1111-1111-1111-111111111144", TestName = "Test 4")]
        [TestCase("4", 4, "11111111-1111-1111-1111-111111111145", TestName = "Test 5")]
        [TestCase("5", 4, "11111111-1111-1111-1111-111111111146", TestName = "Test 6")]
        [TestCase("6", 3, "11111111-1111-1111-1111-111111111147", TestName = "Test 7")]
        [TestCase("7", 7, "11111111-1111-1111-1111-111111111148", TestName = "Test 8")]
        [NunitGoAction]
        [SingleTestSubscription]
        public void ParamTestName(string input, int expected, string guid)
        {
            NunitGoActionAttribute.TestGuid = new Guid(guid);
            Thread.Sleep(100);
            Assert.AreEqual(input, expected.ToString("D"));
        }
    }
}
