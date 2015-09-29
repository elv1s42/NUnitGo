using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NUnit.Core;

namespace Utils.XmlTypes
{
    [XmlRoot("test-result")]
    public class TestResultXml
    {
        public static TestResultXml Load(string path)
        {
            var testResults = new TestResultXml();
            try
            {
                var s = new XmlSerializer(typeof(TestResultXml), new XmlRootAttribute("test-results"));
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    testResults = (TestResultXml)s.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in TestResult Deserialize: '{0}', '{1}'", e.Message, e.StackTrace));
            }
            return testResults;
        }

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

        [XmlElement("assert-count")]
        public int AssertCount { get; set; }

        [XmlElement("failure-site")]
        public string FailureSite { get; set; }

        [XmlElement("executed")]
        public bool Executed { get; set; }

        [XmlElement("has-results")]
        public bool HasResults { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("full-name")]
        public string FullName { get; set; }
        
        [XmlElement("is-error")]
        public bool IsError { get; set; }

        [XmlElement("is-failure")]
        public bool IsFailure { get; set; }

        [XmlElement("is-success")]
        public bool IsSuccess { get; set; }

        [XmlElement("is-suite")]
        public bool IsSuite { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("unique-name")]
        public string UniqueTestName { get; set; }

        [XmlElement("result-state")]
        public string ResultState { get; set; }

        [XmlElement("results-list")]
        public List<TestResultXml> Results { get; set; }

        [XmlElement("stack-trace")]
        public string StackTrace { get; set; }

        [XmlElement("time")]
        public double Time { get; set; }

        [XmlElement("test")]
        public TestXml Test { get; set; }
    }
}
