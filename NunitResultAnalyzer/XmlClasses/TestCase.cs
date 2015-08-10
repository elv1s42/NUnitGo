using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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
            Executed = "";
            Result = "";
            Success = "";
            Time = "";
            Asserts = "";
        }

        public TestCase(TestResultXml result)
        {
            Name = result.FullName;
            Executed = result.Executed.ToString();
            Result = result.ResultState;
            Success = result.IsSuccess.ToString();
            Time = result.Time.ToString("##.#####");
            Asserts = result.AssertCount.ToString("D");
            if (result.IsSuccess) return;
            Failure = new Failure(result);
            Reason = new Reason(result);
        }

        [XmlElement("failure")]
        public Failure Failure { get; set; }

        [XmlElement("reason")]
        public Reason Reason { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

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

        [XmlIgnore]
        public DateTime StartDateTime;

        [XmlIgnore]
        public DateTime EndDateTime;

        [XmlIgnore]
        public Dictionary<string, DateTime> Screenshots;

        [XmlIgnore]
        public string Log;

        [XmlIgnore]
        public string Trace;

        [XmlIgnore]
        public string Out;

        [XmlIgnore]
        public string Error;

        [XmlIgnore]
        public string Guid;

    }
}
