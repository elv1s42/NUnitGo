using System.IO;
using System.Xml.Serialization;
using NunitResultAnalyzer.XmlClasses;

namespace NunitResultAnalyzer
{
    public class NunitXmlReader
    {
        public string XmlPath;

        public NunitXmlReader(string xmlPath)
        {
            XmlPath = xmlPath;
        }

        public TestResults Deserialize()
        {
            TestResults testResults;

            var s = new XmlSerializer(typeof (TestResults), new XmlRootAttribute("test-results"));
            using (var fs = new FileStream(XmlPath, FileMode.Open))
            {
                testResults = (TestResults) s.Deserialize(fs);
            }

            return testResults;
        }
    }
}
