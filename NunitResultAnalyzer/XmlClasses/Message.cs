using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Message
    {
        [XmlText]
        public string Value { get; set; }
    }
}
