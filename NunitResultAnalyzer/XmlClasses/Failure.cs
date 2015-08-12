using System.Xml.Serialization;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Failure
    {
        public Failure()
        {
            Message = new Message();
            StackTrace = new StackTrace();
        }

        public Failure(TestResultXml result)
        {
            if (!result.IsFailure)
            {
                Message = new Message();
                StackTrace = new StackTrace();
            }
            var message = result.Message;
            var stack = result.StackTrace;
            Message = new Message { Value = message };
            StackTrace = new StackTrace { Value = stack };
        }

        [XmlElement("message")]
        public Message Message { get; set; }

        [XmlElement("stack-trace")]
        public StackTrace StackTrace { get; set; }
    }
}
