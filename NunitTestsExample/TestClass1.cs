using System;
using System.Threading;
using NUnit.Framework;
using NUnitGoCore;
using NUnitGoCore.Attributes;

namespace NUnitGoTestsExample
{
    [TestFixture]
    public class TestClass1
    {
        [Test, NunitGoAction("11111111-1111-1111-1111-111111111100", "Project1", "Subsystem1", "Unsuccessful test with 3 events")]
        public void UnsuccessTestThreeEvents()
        {
            //first test event
            NunitGo.EventStarted("Checking something");
            Thread.Sleep(500);
            NunitGo.EventFinished("Checking something");
            //second test event
            NunitGo.EventStarted("Some operation time");
            Thread.Sleep(200);
            //third test event - test fails
            NunitGo.EventStarted("Suboperation time");
            Assert.AreEqual(1, 2);
            NunitGo.EventFinished("Suboperation time");
            NunitGo.EventFinished("Some operation time");
        }

        private const string EventName1 = "Checking some stuff 1";

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111110", "Project1", "Subsystem1", "Successful test with 3 events"), Category("SuccessCategory")]
        [Subscription(Name = "TestSubscription1", UnsuccessfulOnly = false)]
        [EventDurationSubscription(EventName1, 0.001, "Evgeniy.Kosyakov@katharsis.ru")]
        public void SuccessTestThreeEvents()
        {
            var r = new Random();
            //first test event
            NunitGo.EventStarted(EventName1);
            Thread.Sleep(r.Next(500));
            NunitGo.EventFinished(EventName1);
            //second test event
            NunitGo.EventStarted("Checking some stuff 2");
            Thread.Sleep(200);
            //third test event
            NunitGo.EventStarted("Checking some stuff 3");
            Thread.Sleep(r.Next(1000));
            NunitGo.EventFinished("Checking some stuff 3");
            NunitGo.EventFinished("Checking some stuff 2");
            Thread.Sleep(r.Next(700));
            Assert.AreEqual(1, 1);
        }

        [Test, NunitGoAction(
            "11111111-1111-1111-1111-111111111111", 
            "Project1", 
            "Subsystem1", 
            "Long log test name")]
        [Subscription("tester1@test.test", "tester2@test.test")]
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

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111112", "Project1", "Subsystem1"), Category("SuccessCategory")]
        [Subscription(Name = "TestSubscription1", UnsuccessfulOnly = false)]
        public void SuccessTest()
        {
            Thread.Sleep(200);
            Assert.AreEqual(1, 1);
        }
        
        [Test, NunitGoAction(
            "11111111-1111-1111-1111-111111111113", 
            "Project1", 
            "Subsystem2",
            "Three screenshots expected test")]
        [SingleTestSubscription]
        public void ThreeScreenshotsExpected()
        {
            Console.WriteLine("Testing log writing 1");
            Console.WriteLine("Testing log writing 2");
            NunitGo.TakeScreenshot();
            Thread.Sleep(400);
            NunitGo.TakeScreenshot();
            Thread.Sleep(300);
            throw new Exception("Some error occured!");
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

        [Test, NunitGoAction("11111111-1111-1111-1111-111111111116", "Project1", "Subsystem2")]
        [Subscription(Name = "TestSubscription1")]
        [Subscription(Name = "TestSubscription2")]
        public void TestMethodTwoSubs()
        {
            Thread.Sleep(300);
            Assert.Ignore("Test was ignored!");
        }
    }
}
