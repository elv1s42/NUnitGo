using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace Utils.Extensions
{
    public static class TestResultXmlExtension
    {
        public static string ToString(this TestResultXml result)
        {
            var s = new XmlSerializer(typeof(TestResultXml), new XmlRootAttribute("test-results"));
            string xml;
            using (var sw = new StringWriter())
            {
                var writer = XmlWriter.Create(sw);
                s.Serialize(writer, result);
                xml = sw.ToString();
            }
            return xml;
        }

        public static XmlDocument ToXmlDoc(this TestResultXml result)
        {
            var xml = ToString(result);
            var xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            return xdoc;
        }

        public static void Save(this TestResultXml result, string path)
        {
            var xdoc = ToXmlDoc(result);
            xdoc.Save(path);
        }

        public static int CountTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountTests(current));
        }

        public static int CountErrorTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite && result.IsError) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountErrorTests(current));
        }

        public static int CountFailureTests(this TestResultXml result, int currentCount = 0)
        {
            var count = currentCount;
            if (result.Test != null && !result.IsSuite && result.IsFailure) count++;
            return result.Results.Aggregate(count, (current, testResult) => testResult.CountFailureTests(current));
        }
    }
}
