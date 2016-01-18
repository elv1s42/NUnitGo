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
            Console.WriteLine("Testing log writing 6");
            Console.WriteLine("Testing log writing 7");
            Console.WriteLine("Testing log writing 8");
            Console.WriteLine("Testing log writing 9");
            Console.WriteLine("Testing log writing 10");
            Console.WriteLine("Testing log writing 11");
            Console.WriteLine("Testing log writing 12");
            Console.WriteLine("Testing log writing 13");
            Console.WriteLine("Testing log writing 14");
            Console.WriteLine("Testing log writing 15");
            Console.WriteLine("Testing log writing 16");
            Console.WriteLine("Testing log writing 17");
            Console.WriteLine("Testing log writing 18");
            Console.WriteLine("Testing log writing 19");
            Console.WriteLine("Testing log writing 20");
            Console.WriteLine("Testing log writing 21");
            Console.WriteLine("Testing log writing 22");
            Console.WriteLine("Testing log writing 23");
            Console.WriteLine("Testing log writing 24");
            Console.WriteLine("Testing log writing 25");
            Console.WriteLine("Testing log writing 26");
            Console.WriteLine("Testing log writing 27");
            Console.WriteLine("Testing log writing 28");
            Console.WriteLine("Testing log writing 29");
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
            NunitGoHelper.TakeScreenshot();
            Thread.Sleep(100);
            NunitGoHelper.TakeScreenshot();
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
