using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Reason
    {
        [XmlElement("message")]
        public Message Message { get; set; }
    }
}
