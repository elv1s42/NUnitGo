using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NunitGo
{
    public class NunitGoTest
    {
        public string FullName;
        public string Id;
        public string TestDuration;
        public DateTime DateTimeStart;
        public DateTime DateTimeFinish;
        public int ScreenshotsCount;
        public string FailureStackTrace;
        public string FailureMessage;
        public string Result;

        public NunitGoTest()
        {
            FullName = "";
            Id = "";
            TestDuration = "0.0";
            DateTimeStart = new DateTime();
            DateTimeFinish = new DateTime();
            ScreenshotsCount = 0;
            FailureStackTrace = "";
            FailureMessage = "";
            Result = "Unknown";
        }

        public NunitGoTest(ITest test, TestContext context)
        {
            FullName = test.FullName;
            Id = test.Id;
            FailureStackTrace = context.Result.StackTrace ?? "";
            FailureMessage = context.Result.Message ?? "";
            Result = context.Result.Outcome != null ? context.Result.Outcome.Status.ToString() : "Unknown";
        }
    }
}
