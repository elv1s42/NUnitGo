using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class StackTrace
    {
        public StackTrace()
        {
            Value = "";
        }

        [XmlText]
        public string Value { get; set; }
    }
}
