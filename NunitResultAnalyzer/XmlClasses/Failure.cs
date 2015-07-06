using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Failure
    {
        [XmlElement("message")]
        public Message Message { get; set; }

        [XmlElement("stack-trace")]
        public StackTrace StackTrace { get; set; }
    }
}
