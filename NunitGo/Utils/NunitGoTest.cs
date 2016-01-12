using System;
using System.Collections.Generic;

namespace NunitGo.Utils
{
    public class NunitGoTest
    {
        public string Name;
        public string FullName;
        public string ClassName;
        public string ProjectName;
        public double TestDuration;
        public DateTime DateTimeStart;
        public DateTime DateTimeFinish;
        public int ScreenshotsCount;
        public string FailureStackTrace;
        public string FailureMessage;
        public string Result;
        public Guid Guid;
        public string OutputPath;
        public bool HasOutput;
        public List<NunitGoTestScreenshot> Screenshots;

        public NunitGoTest()
        {
            Name = String.Empty;
            FullName = String.Empty;
            ClassName = String.Empty;
            ProjectName = String.Empty;
            TestDuration = 0.0;
            DateTimeStart = new DateTime();
            DateTimeFinish = new DateTime();
            ScreenshotsCount = 0;
            FailureStackTrace = String.Empty;
            FailureMessage = String.Empty;
            Result = "Unknown";
            Guid = Guid.NewGuid();
            OutputPath = String.Empty;
            HasOutput = false;
        }

        public bool IsSuccess()
        {
            return Result.Equals("Success") || Result.Equals("Passed");
        }

        public bool IsFailed()
        {
            return Result.Equals("Failure") || Result.Equals("Failed");
        }

        public bool IsBroken()
        {
            return Result.Equals("Failed:Error") || Result.Equals("Error");
        }

        public bool IsIgnored()
        {
            return Result.Equals("Ignored") || Result.Equals("Skipped:Ignored");
        }

        public bool IsInconclusive()
        {
            return Result.Equals("Inconclusive");
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
