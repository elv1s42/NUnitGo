using System;

namespace NunitGo.NunitGoItems.Events
{
    public class NunitGoTestEvent
    {
        public string Name;
        private DateTime _started;
        private DateTime _finished;

        public double Duration {
            get { return (_finished - _started).TotalSeconds; }
        }

        public NunitGoTestEvent(string eventName)
        {
            Name = eventName;
            _started = DateTime.Now;
            _finished = DateTime.Now;
        }

        public NunitGoTestEvent(string eventName, DateTime started, DateTime finished)
        {
            Name = eventName;
            _started = started;
            _finished = finished;
        }

        public void Started(DateTime date = default(DateTime))
        {
            _started = date.Equals(default(DateTime)) ? DateTime.Now : date;
        }

        public void Finished(DateTime date = default(DateTime))
        {
            _finished = date.Equals(default(DateTime)) ? DateTime.Now : date;
        }
    }
}
