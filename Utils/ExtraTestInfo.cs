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

    }
}