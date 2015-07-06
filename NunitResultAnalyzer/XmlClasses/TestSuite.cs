using System;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class TestSuite
    {
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

        [XmlIgnore]
        public DateTime StartDateTime;

        [XmlIgnore]
        public DateTime EndDateTime;
    }
}
