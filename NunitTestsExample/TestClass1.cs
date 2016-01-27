using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NunitGo;
using NunitGo.Attributes;
using NunitGo.Utils;
using ScreenshotTaker;

namespace NunitTestsExample
{
    [TestFixture]
    public class TestClass1
    {
        [Test, NunitGoAction(
            "11111111-1111-1111-1111-111111111111", 
            "Project1", 
            "Subsystem1", 
            "Long log test")]
        public void LongLogTest()
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
            for (var i = 6; i < 55; i++)
            {
                Console.WriteLine("Testing log writing " + i);
            }
            Assert.AreEqual(1, 2);
        }

        [Test, NunitGoAction(
            "11111111-1111-1111-1111-111111111113", 
            "Project1", 
            "Subsystem2",
            "Three screenshots expected test")]
        [NunitGoSingleSubscription]
        public void ThreeScreenshotsExpected()
        {
            Console.WriteLine("Testing log writing 1");
            Console.WriteLine("Testing log writing 2");
            Taker.TakeScreenshot(NunitGoHelper.Screenshots);
            Thread.Sleep(100);
            Taker.TakeScreenshot(NunitGoHelper.Screenshots);
            Thread.Sleep(100);
            throw new Exception("Some error occured!");
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111112", "Project1", "Subsystem1"), Category("SuccessCategory")]
        [NunitGoSubscription(Name = "TestSubscription1", UnsuccessfulOnly = false)]
        public void SuccessTest()
        {
            Thread.Sleep(200);
            Assert.AreEqual(1, 1);
        }
        
        [Test, NunitGoAction("11111111-1111-1111-1111-111111111114", "Project1", "Subsystem2")]
        public void TestMethodInconclusive()
        {
            Thread.Sleep(300);
            Assert.Inconclusive("Inconc. test :)");
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111115", "Project1")]
        public void TestMethodIgnored()
        {
            Thread.Sleep(300);
            Assert.Ignore("Test was ignored!");
        }

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111115", "Project1", "Subsystem2")]
        [NunitGoSubscription(Name = "TestSubscription1")]
        [NunitGoSubscription(Name = "TestSubscription2")]
        public void TestMethodTwoSubs()
        {
            Thread.Sleep(300);
            Assert.Ignore("Test was ignored!");
        }
    }
}
