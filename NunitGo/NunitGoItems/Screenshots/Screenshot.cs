using System;

namespace NunitGoCore.NunitGoItems.Screenshots
{
    public class Screenshot
    {
        public Screenshot()
        {
            var now = DateTime.Now;
            Name = Taker.GetScreenName(now);
            Date = now;
        }

        public Screenshot(DateTime date)
        {
            Name = Taker.GetScreenName(date);
            Date = date;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
