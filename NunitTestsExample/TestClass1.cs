using System;
using System.Threading;
using NUnit.Framework;
using Utils;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestMethod1()
        {
            Console.WriteLine("Testing log writing 1");
            Thread.Sleep(1000);
            Console.WriteLine("Testing log writing 2");
            Thread.Sleep(2000);
            Console.WriteLine("Testing log writing 3");
            Thread.Sleep(1000);
            Console.WriteLine("Testing log writing 4");
            Thread.Sleep(2000);
            Console.WriteLine("Testing log writing 5");
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void TestMethod2()
        {
            Thread.Sleep(2000);
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestMethod3()
        {
            Console.WriteLine("Testing log writing 1");
            Console.WriteLine("Testing log writing 2");
            ScreenshotHelper.TakeScreenshot();
            Thread.Sleep(1000);
            ScreenshotHelper.TakeScreenshot();
            Thread.Sleep(1000);
            throw new Exception("Some error occured!");
        }

        [Test]
        public void TestMethod4()
        {
            Thread.Sleep(2000);
            Assert.Inconclusive("Inconc. test :)");
        }

        [Test]
        public void TestMethod5()
        {
            Thread.Sleep(2000);
            Assert.Ignore("Test was ignored!");
        }
    }
}
