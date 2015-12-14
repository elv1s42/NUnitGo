using System;

namespace NunitGo
{
    public class NunitGoTest
    {
        public string FullName;
        public string Id;
        public double TestDuration;
        public DateTime DateTimeStart;
        public DateTime DateTimeFinish;
        public int ScreenshotsCount;
        public string FailureStackTrace;
        public string FailureMessage;
        public string Result;
        public Guid Guid;

        public NunitGoTest()
        {
            FullName = "";
            Id = "";
            TestDuration = 0.0;
            DateTimeStart = new DateTime();
            DateTimeFinish = new DateTime();
            ScreenshotsCount = 0;
            FailureStackTrace = "";
            FailureMessage = "";
            Result = "Unknown";
            Guid = Guid.NewGuid();
        }

        public void SaveTestToHtml(NunitGoTest test)
        {
            

        }
    }
}
