using System;
using System.Collections.Generic;
using System.Linq;
using NUnitGoCore.NunitGoItems.Events;
using NUnitGoCore.NunitGoItems.Screenshots;
using NUnitGoCore.Utils;

namespace NUnitGoCore
{
    public static class NunitGo
    {
        private static List<TestEvent> _events;
        private static List<Screenshot> _screenshots;
        internal static Guid TestGuid = Guid.Empty;
        internal static string TestName = "";

        public static void Event(string name, Action testEventAction)
        {
            _events.Add(new TestEvent(name, DateTime.Now));
            testEventAction.Invoke();
            _events.First(x => x.Name.Equals(name)).Finished = DateTime.Now;
        }

        public static void TakeScreenshot()
        {
            var now = DateTime.Now;
            _screenshots.Add(new Screenshot(now));
            Taker.TakeScreenshot(Output.GetScreenshotsPath(NunitGoHelper.Configuration.LocalOutputPath), now);
        }

        public static void EventStarted(string name)
        {
            _events.Add(new TestEvent(name, DateTime.Now));
        }

        public static void EventFinished(string name)
        {
            _events.First(x => x.Name.Equals(name)).Finished = DateTime.Now;
        }

        public static void EventStarted(string name, DateTime date)
        {
            _events.Add(new TestEvent(name, date));
        }

        public static void EventFinished(string name, DateTime date)
        {
            _events.First(x => x.Name.Equals(name)).Finished = date;
        }

        public static void SetTestGuid(string guid)
        {
            TestGuid = new Guid(guid);
        }

        public static void SetTestName(string name)
        {
            TestName = name;
        }

        internal static List<Screenshot> GetScreenshots()
        {
            return _screenshots;
        }

        internal static List<TestEvent> GetEvents()
        {
            foreach (var testEvent in _events.Where(testEvent => testEvent.Finished.Equals(default(DateTime))))
            {
                testEvent.Finished = DateTime.Now;
            }
            return _events;
        }

        internal static void SetUp()
        {
            CleanUp();
        }

        internal static void TearDown()
        {
            CleanUp();
        }

        private static void CleanUp()
        {
            TestName = "";
            TestGuid = Guid.Empty;
            _events = new List<TestEvent>();
            _screenshots = new List<Screenshot>();
        }
    }
}
