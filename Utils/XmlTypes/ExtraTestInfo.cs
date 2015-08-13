using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Utils.XmlTypes
{
    public class ExtraTestInfo
    {
        public ExtraTestInfo()
        {
            Guid = new Guid();
            TestId = "";
            RunnerId = "";
            TestName = "";
            FullTestName = "";
            UniqueTestName = "";
            StartDate = new DateTime();
            FinishDate = new DateTime();
            AssertCount = 0;
            Log = "";
            Out = "";
            Trace = "";
            Error = "";
        }

        public static List<ExtraTestInfo> Load(string path)
        {
            List<ExtraTestInfo> extraInfo;
            var xs = new XmlSerializer(typeof(List<ExtraTestInfo>));
            using (var sr = new StreamReader(path))
            {
                extraInfo = (List<ExtraTestInfo>)xs.Deserialize(sr);
            }
            return extraInfo;
        }
        
        [XmlElement("guid")]
        public Guid Guid { get; set; }

        [XmlElement("test-id")]
        public string TestId { get; set; }

        [XmlElement("runner-id")]
        public string RunnerId { get; set; }

        [XmlElement("test-name")]
        public string TestName { get; set; }

        [XmlElement("full-test-name")]
        public string FullTestName { get; set; }

        [XmlElement("unique-test-name")]
        public string UniqueTestName { get; set; }

        [XmlElement("start")]
        public DateTime StartDate { get; set; }

        [XmlElement("finish")]
        public DateTime FinishDate { get; set; }

        [XmlElement("assert-count")]
        public int AssertCount { get; set; }
        
        [XmlElement("log")]
        public string Log { get; set; }

        [XmlElement("out")]
        public string Out { get; set; }

        [XmlElement("trace")]
        public string Trace { get; set; }

        [XmlElement("error")]
        public string Error { get; set; }
    }
}