using System;

namespace NunitGoCore.NunitGoItems.Events
{
    public class NunitGoTestEvent
    {
        public string Name;
        public DateTime Started;
        public DateTime Finished;

        public double Duration
        {
            get { return (Finished - Started).TotalSeconds; }
        }

        public NunitGoTestEvent(string eventName = "", DateTime started = default(DateTime), DateTime finished = default(DateTime))
        {
            Name = eventName;
            Started = started;
            Finished = finished;
        }
    }
}
