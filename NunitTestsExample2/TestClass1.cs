using System;
using System.Threading;
using NUnit.Framework;
using NunitGo;

namespace NunitTestsExample2
{
    [TestFixture]
    public class TestClass1
    {
        [TestCase("0", 1, "11111111-1111-1111-1111-111111111211")]
        [TestCase("1", 1, "11111111-1111-1111-1111-111111111212")]
        [TestCase("2", 1, "11111111-1111-1111-1111-111111111213", TestName = "Testing name attribute")]
        [NunitGoAction]
        public void ParamTestName1(string input, int expected, string guid)
        {
            NunitGoActionAttribute.TestGuid = new Guid(guid);
            Thread.Sleep(500);
            Assert.AreEqual(input, expected.ToString("D"));
        }

        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1, TestName = "Some test name")]
        public void ParamTestName2(string input, int expected)
        {
            Thread.Sleep(500);
            Assert.AreEqual(input, expected.ToString("D"));
        }

        [Test]
        public void TestMethod3()
        {
            Console.WriteLine("Testing log writing 1");
            Console.WriteLine("Testing log writing 2");
            throw new Exception("Some error occured!");
        }
    }
}
