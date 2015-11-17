using System.Collections.Generic;
using System.Linq;
using NUnit.Core;

namespace Utils.XmlTypes
{
    public class TestResultXml
    {
        public TestResultXml()
        {
            AssertCount = 0;
            Description = "";
            Executed = false;
            FailureSite = FailureSite;
            FullName = "";
            HasResults = false;
            IsError = false;
            IsFailure = false;
            IsSuccess = false;
            Message = "";
            Name = "";
            ResultState = "";
            StackTrace = "";
            IsSuite = false;
            Results = new List<TestResultXml>();
            Time = 0.0;
            Test = new TestXml();
        }

        public TestResultXml(TestResult result)
        {
            AssertCount = result.AssertCount;
            Description = result.Description;
            Executed = result.Executed;
            FailureSite = result.FailureSite.ToString();
            FullName = result.FullName;
            HasResults = result.HasResults;
            IsError = result.IsError;
            IsFailure = result.IsFailure;
            IsSuccess = result.IsSuccess;
            Message = result.Message;
            Name = result.Name;
            ResultState = result.ResultState.ToString();
            StackTrace = result.StackTrace;
            Test = new TestXml(result.Test);
            UniqueTestName = Test.UniqueName;
            IsSuite = Test.IsSuite;
            Time = result.Time;
            var res = new List<TestResultXml>();
            if (result.Results != null)
                res.AddRange(from TestResult nunitResult in result.Results
                             select new TestResultXml(nunitResult));
            Results = res;
        }

        public int AssertCount { get; set; }
        public string FailureSite { get; set; }
        public bool Executed { get; set; }
        public bool HasResults { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public bool IsError { get; set; }
        public bool IsFailure { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsSuite { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string UniqueTestName { get; set; }
        public string ResultState { get; set; }
        public List<TestResultXml> Results { get; set; }
        public string StackTrace { get; set; }
        public double Time { get; set; }
        public TestXml Test { get; set; }
    }
}
