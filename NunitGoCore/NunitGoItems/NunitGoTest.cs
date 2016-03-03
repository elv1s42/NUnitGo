using System;
using System.Collections.Generic;
using NunitGoCore.NunitGoItems.Events;
using NunitGoCore.NunitGoItems.Screenshots;
using NunitGoCore.Utils;

namespace NunitGoCore.NunitGoItems
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
        public string TestStackTrace;
        public string TestMessage;
        public string Result;
        public Guid Guid;
        public string AttachmentsPath;
        public string TestHrefRelative;
        public string TestHrefAbsolute;
        public string LogHref;
        public bool HasOutput;
        public List<Screenshot> Screenshots;
        public List<TestEvent> Events;

        public NunitGoTest()
        {
            Name = string.Empty;
            FullName = string.Empty;
            ClassName = string.Empty;
            ProjectName = string.Empty;
            TestDuration = 0.0;
            DateTimeStart = new DateTime();
            DateTimeFinish = new DateTime();
            TestStackTrace = string.Empty;
            TestMessage = string.Empty;
            Result = "Unknown";
            Guid = Guid.NewGuid();
            TestHrefRelative = string.Empty;
            TestHrefAbsolute = string.Empty;
            LogHref = string.Empty;
            AttachmentsPath = string.Empty;
            HasOutput = false;
            Screenshots = new List<Screenshot>();
            Events = new List<TestEvent>();
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
