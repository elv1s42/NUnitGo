using System;

namespace NunitGo.Utils
{
    public class NunitGoTestScreenshot
    {
        public NunitGoTestScreenshot()
        {
            var now = DateTime.Now;
            Name = Helper.GetScreenName(now);
            Date = now;
        }

        public NunitGoTestScreenshot(DateTime date)
        {
            Name = Helper.GetScreenName(date);
            Date = date;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
