using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using NunitResultAnalyzer.XmlClasses;
using Utils;

namespace NunitResultAnalyzer
{
    public static class NunitXmlReader
    {
        public static TestResults Deserialize(string xmlPath)
        {
            var testResults = new TestResults();
            try
            {
                var s = new XmlSerializer(typeof (TestResults), new XmlRootAttribute("test-results"));
                using (var fs = new FileStream(xmlPath, FileMode.Open))
                {
                    testResults = (TestResults) s.Deserialize(fs);
                }
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in TestResult Deserialize: '{0}', '{1}'", e.Message, e.StackTrace));
            }
            return testResults;
        }

        public static string Serialize(TestResults testResults)
        {
            var s = new XmlSerializer(typeof(TestResults), new XmlRootAttribute("test-results"));
            var sw = new StringWriter();
            var writer = XmlWriter.Create(sw);
            s.Serialize(writer, testResults);
            var xml = sw.ToString();
            return xml;
        }

        public static void Save(TestResults testResults, string path)
        {
            var s = Serialize(testResults);
            var xdoc = new XmlDocument();
            xdoc.LoadXml(s);
            xdoc.Save(path);
        }
    }
}
