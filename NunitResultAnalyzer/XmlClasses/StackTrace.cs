using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class StackTrace
    {
        [XmlText]
        public string Value { get; set; }
    }
}
