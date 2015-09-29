using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Results
    {
        public List<TestCase> GetTestCases(List<TestResultXml> results)
        {
            return (from result in results where !result.Test.IsSuite select new TestCase(result)).ToList();
        }

        public List<TestSuite> GetTestSuites(List<TestResultXml> results)
        {
            return (from result in results where result.Test.IsSuite select new TestSuite(result)).ToList();
        }

        public Results(List<TestResultXml> results)
        {
            TestCases = GetTestCases(results);
            TestSuites = GetTestSuites(results);
        }

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
