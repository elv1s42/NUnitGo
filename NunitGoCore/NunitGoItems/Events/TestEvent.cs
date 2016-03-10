using System;

namespace NUnitGoCore.NunitGoItems.Events
{
    public class TestEvent
    {
        public string Name;
        public DateTime Started;
        public DateTime Finished;

        public double Duration
        {
            get { return (Finished - Started).TotalSeconds; }
        }

        public string DurationString
        {
            get { return (Finished - Started).ToString(@"hh\:mm\:ss\:fff"); }
        }

        public TestEvent()
        {
            Name = "";
            Started = default(DateTime);
            Finished = default(DateTime);
        }

        public TestEvent(string eventName = "", DateTime started = default(DateTime), DateTime finished = default(DateTime))
        {
            Name = eventName;
            Started = started;
            Finished = finished;
        }
    }
}
