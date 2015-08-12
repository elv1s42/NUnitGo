using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Message
    {
        public Message()
        {
            Value = "";
        }

        [XmlText]
        public string Value { get; set; }
    }
}
