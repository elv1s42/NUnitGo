using System;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Screenshot
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("date")]
        public DateTime Date { get; set; }
    }
}
