using System.Collections.Generic;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Results
    {
        [XmlElement("test-case")]
        public List<TestCase> TestCases { get; set; }

        [XmlElement("test-suite")]
        public List<TestSuite> TestSuites { get; set; }
    }
}
