using System.Xml.Serialization;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Failure
    {
        public Failure()
        {
        }

        public Failure(TestResultXml result)
        {
            if (!result.IsFailure) return;
            Message = new Message { Value = result.Message };
            StackTrace = new StackTrace { Value = result.StackTrace };
        }

        [XmlElement("message")]
        public Message Message { get; set; }

        [XmlElement("stack-trace")]
        public StackTrace StackTrace { get; set; }
    }
}
