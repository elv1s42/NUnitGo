using System.Xml.Serialization;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Reason
    {
        public Reason()
        {
        }
        
        public Reason(TestResultXml result)
        {
            if(!result.IsFailure) Message = new Message {Value = result.Message};
        }

        [XmlElement("message")]
        public Message Message { get; set; }
    }
}
