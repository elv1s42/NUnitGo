using System;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class TestSuite
    {
        public TestSuite()
        {
            Results = new Results();
            Type = "";
            Name = "";
            Executed = "";
            Result = "";
            Success = "";
            Time = "";
            Asserts = "";
            StartDateTime = new DateTime();
            EndDateTime = new DateTime();
        }

        public TestSuite(TestResultXml result)
        {
            if (result.Results != null) Results = new Results(result.Results);
            if (result.Test != null) Type = result.Test.TestType;
            Name = result.Name;
            Executed = result.Executed.ToString();
            Result = result.ResultState;
            Success = result.IsSuccess.ToString();
            Time = result.Time.ToString("##.#####");
            Asserts = result.AssertCount.ToString("D");
            StartDateTime = new DateTime();
            EndDateTime = new DateTime();
        }

        [XmlElement("results")]
        public Results Results { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

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

        [XmlAttribute("start-date")]
        public DateTime StartDateTime;

        [XmlAttribute("finish-date")]
        public DateTime EndDateTime;
    }
}
