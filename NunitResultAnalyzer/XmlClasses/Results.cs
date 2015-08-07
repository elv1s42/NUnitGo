using System.Collections.Generic;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Results
    {
        public Results()
        {
            TestCases = new List<TestCase>();
            TestSuites = new List<TestSuite>();
        }

        [XmlElement("test-case")]
        public List<TestCase> TestCases { get; set; }

        [XmlElement("test-suite")]
        public List<TestSuite> TestSuites { get; set; }
    }
}
