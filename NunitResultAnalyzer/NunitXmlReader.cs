using System.IO;
using System.Xml;
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

        public string Serialize(TestResults testResults)
        {
            var s = new XmlSerializer(typeof(TestResults), new XmlRootAttribute("test-results"));
            var sww = new StringWriter();
            var writer = XmlWriter.Create(sww);
            s.Serialize(writer, testResults);
            var xml = sww.ToString();
            return xml;
        }

        public void Save(TestResults testResults, string path)
        {
            var s = Serialize(testResults);
            var xdoc = new XmlDocument();
            xdoc.LoadXml(s);
            xdoc.Save(path);
        }
    }
}
