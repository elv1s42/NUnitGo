using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Utils;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class TestCase
    {
        public TestCase()
        {
            Failure = new Failure();
            Reason = new Reason();
            Name = "";
            FullName = "";
            UniqueName = "";
            Executed = "";
            Result = "";
            Success = "";
            Time = "";
            Asserts = "";
        }

        public TestCase(TestResultXml result)
        {
            Name = result.Name;
            FullName = result.FullName;
            UniqueName = result.UniqueTestName;
            Executed = result.Executed.ToString();
            Result = result.ResultState;
            Success = result.IsSuccess.ToString();
            Time = result.Time.ToString("##.#####");
            Asserts = result.AssertCount.ToString("D");
            if (result.IsSuccess) return;
            Failure = new Failure(result);
            Reason = new Reason(result);
        }

        public string GetBackgroundColor()
        {
            switch (Result)
            {
                case "Ignored":
                    return Colors.TestIgnored;
                case "Success":
                    return Colors.TestPassed;
                case "Error":
                    return Colors.TestBroken;
                case "Inconclusive":
                    return Colors.TestInconclusive;
                case "Failure":
                    return Colors.TestFailed;
                default:
                    return Colors.TestUnknown;
            }
        }

        [XmlElement("failure")]
        public Failure Failure { get; set; }

        [XmlElement("reason")]
        public Reason Reason { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("unique-name")]
        public string UniqueName { get; set; }

        [XmlAttribute("executed")]
        public string Executed { get; set; }

        [XmlAttribute("result")]
        public string Result { get; set; }

        [XmlAttribute("success")]
        public string Success { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlAttribute("asserts")]
        public string Asserts { get; set; }

        [XmlElement("start-date")]
        public DateTime StartDateTime;

        [XmlElement("finish-date")]
        public DateTime EndDateTime;

        [XmlArray("screenshots")]
        public List<Screenshot> Screenshots;

        [XmlElement("test-log")]
        public string Log;

        [XmlElement("test-trace")]
        public string Trace;

        [XmlElement("test-output")]
        public string Out;

        [XmlElement("test-error")]
        public string Error;

        [XmlElement("test-guid")]
        public string Guid;

    }
}
