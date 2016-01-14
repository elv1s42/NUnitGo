using System;
using System.Threading;
using NUnit.Framework;
using NunitGo;
using NunitGo.Utils;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass1
    {
        [Test, NunitGoAction("11111111-1111-1111-1111-111111111111", "Project1", "Subsystem1")]
        public void TestMethod1()
        {
            Console.WriteLine("Testing log writing 1");
            Thread.Sleep(100);
            Console.WriteLine("Testing log writing 2");
            Thread.Sleep(200);
            Console.WriteLine("Testing log writing 3");
            Thread.Sleep(100);
            Console.WriteLine("Testing log writing 4");
            Thread.Sleep(200);
            Console.WriteLine("Testing log writing 5");
            Assert.AreEqual(1, 2);
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111112", "Project1", "Subsystem1"), Category("SuccessCategory")]
        public void TestMethod2()
        {
            Thread.Sleep(200);
            Assert.AreEqual(1, 1);
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111113", "Project1", "Subsystem2")]
        public void TestMethod3()
        {
            Console.WriteLine("Testing log writing 1");
            Console.WriteLine("Testing log writing 2");
            Helper.TakeScreenshot();
            Thread.Sleep(100);
            Helper.TakeScreenshot();
            Thread.Sleep(100);
            throw new Exception("Some error occured!");
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111114", "Project1", "Subsystem2")]
        public void TestMethod4()
        {
            Thread.Sleep(300);
            Assert.Inconclusive("Inconc. test :)");
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111115")]
        public void TestMethod5()
        {
            Thread.Sleep(300);
            Assert.Ignore("Test was ignored!");
        }
    }
}
