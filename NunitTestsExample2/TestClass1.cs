using System;
using System.Threading;
using NunitGoCore;
using NunitGoCore.Attributes;
using NUnit.Framework;

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
            Thread.Sleep(200);
            Assert.AreEqual(input, expected.ToString("D"));
        }

        [TestCase("0", 1, "11111111-1111-1111-1111-111111111214", TestName = "param test 1")]
        [TestCase("1", 1, "11111111-1111-1111-1111-111111111215", TestName = "param test 2")]
        [TestCase("2", 1, "11111111-1111-1111-1111-111111111216", TestName = "param test 3")]
        [NunitGoAction]
        public void ParamTestName2(string input, int expected, string guid)
        {
            //NunitGo.SetTestGuid(guid);
            NunitGoActionAttribute.TestGuid = new Guid(guid);
            Thread.Sleep(200);
            NunitGo.EventStarted("Test event 1");
            Thread.Sleep(200);
            NunitGo.EventFinished("Test event 1");
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
