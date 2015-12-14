using System;
using System.Collections.Generic;

namespace Utils
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
        public string OutputPath;
        public List<NunitGoTestScreenshot> Screenshots;

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
            OutputPath = "";
        }

        public bool IsSuccess()
        {
            return Result.Equals("Success") || Result.Equals("Passed");
        }

        public string GetBackgroundColor()
        {
            switch (Result)
            {
                case "Ignored":
                    return Colors.TestIgnored;
                case "Skipped:Ignored":
                    return Colors.TestIgnored;

                case "Passed":
                    return Colors.TestPassed;
                case "Success":
                    return Colors.TestPassed;

                case "Failed:Error":
                    return Colors.TestBroken;
                case "Error":
                    return Colors.TestBroken;

                case "Inconclusive":
                    return Colors.TestInconclusive;
                
                case "Failure":
                    return Colors.TestFailed;
                case "Failed":
                    return Colors.TestFailed;
                
                default:
                    return Colors.TestUnknown;
            }
        }
    }
}
