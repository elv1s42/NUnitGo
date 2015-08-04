using System;
using System.Xml.Serialization;

namespace NunitGoAddin
{
    public class ExtraTestInfo
    {
        [XmlElement("guid")]
        public Guid Guid { get; set; }

        [XmlElement("test-name")]
        public string TestName { get; set; }

        [XmlElement("full-test-name")]
        public string FullTestName { get; set; }

        [XmlElement("unique-test-name")]
        public string UniqueTestName { get; set; }

        [XmlElement("start")]
        public DateTime StartDate { get; set; }

        [XmlElement("finish")]
        public DateTime FinishDate { get; set; }

        [XmlElement("log")]
        public string Log { get; set; }

        [XmlElement("out")]
        public string Out { get; set; }

        [XmlElement("trace")]
        public string Trace { get; set; }

        [XmlElement("error")]
        public string Error { get; set; }
    }
}