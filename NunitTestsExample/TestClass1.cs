using System;
using NUnit.Framework;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestMethod1()
        {
            Console.WriteLine("Testing log writing");
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestMethod3()
        {
            throw new Exception("Some error occured!");
        }

        [Test]
        public void TestMethod4()
        {
            Assert.Inconclusive("Inconc. test :)");
        }

        [Test]
        public void TestMethod5()
        {
            Assert.Ignore("Test was ignored!");
        }
    }
}
