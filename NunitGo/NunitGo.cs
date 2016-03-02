using System;
using System.Collections.Generic;
using System.Linq;
using NunitGoCore.NunitGoItems.Events;
using NunitGoCore.NunitGoItems.Screenshots;
using NunitGoCore.Utils;

namespace NunitGoCore
{
    public static class NunitGo
    {
        private static List<TestEvent> _events;
        private static List<Screenshot> _screenshots;
        internal static Guid TestGuid = Guid.Empty;

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

        public static void SetTestGuid(string guid)
        {
            TestGuid = new Guid(guid);
        }

        internal static Guid GetTestGuid()
        {
            return TestGuid;
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
            TestGuid = Guid.Empty;
            _events = new List<TestEvent>();
            _screenshots = new List<Screenshot>();
        }
    }
}
