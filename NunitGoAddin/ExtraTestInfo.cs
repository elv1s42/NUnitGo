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

        [XmlElement("start")]
        public DateTime StartDate { get; set; }

        [XmlElement("finish")]
        public DateTime FinishDate { get; set; }

        [XmlElement("out")]
        public string OutPath { get; set; }

        [XmlElement("error")]
        public string ErrorPath { get; set; }

        [XmlElement("trace")]
        public string TracePath { get; set; }

        [XmlElement("log")]
        public string LogPath { get; set; }
    }
}