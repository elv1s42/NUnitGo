using System;

namespace NunitGo.Utils
{
    public class NunitGoTestScreenshot
    {
        public NunitGoTestScreenshot()
        {
            var now = DateTime.Now;
            Name = NunitGoHelper.GetScreenName(now);
            Date = now;
        }

        public NunitGoTestScreenshot(DateTime date)
        {
            Name = NunitGoHelper.GetScreenName(date);
            Date = date;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
