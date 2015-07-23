using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class TestCase
    {
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

    }
}
