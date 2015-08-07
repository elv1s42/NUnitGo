using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class TestResults
    {
        public TestResults()
        {
            Name = "";
            Total = "";
            Errors = "";
            Failures = "";
            NotRun = "";
            Inconclusive = "";
            Ignored = "";
            Skipped = "";
            Invalid = "";
            Date = "";
            Time = "";
            Environment = new Environment();
            CultureInfoXml = new CultureInfoXml();
            TestSuite = new TestSuite();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("total")]
        public string Total { get; set; }

        [XmlAttribute("errors")]
        public string Errors { get; set; }

        [XmlAttribute("failures")]
        public string Failures { get; set; }

        [XmlAttribute("not-run")]
        public string NotRun { get; set; }

        [XmlAttribute("inconclusive")]
        public string Inconclusive { get; set; }

        [XmlAttribute("ignored")]
        public string Ignored { get; set; }

        [XmlAttribute("skipped")]
        public string Skipped { get; set; }

        [XmlAttribute("invalid")]
        public string Invalid { get; set; }

        [XmlAttribute("date")]
        public string Date { get; set; }

        [XmlAttribute("time")]
        public string Time { get; set; }

        [XmlElement("environment")]
        public Environment Environment { get; set; }

        [XmlElement("culture-info")]
        public CultureInfoXml CultureInfoXml { get; set; }

        [XmlElement("test-suite")]
        public TestSuite TestSuite { get; set; }

    }
}
