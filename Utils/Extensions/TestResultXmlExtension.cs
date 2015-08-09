using System.IO;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace Utils.Extensions
{
    public static class TestResultXmlExtension
    {
        public static void Save(this TestResultXml result, string path)
        {
            var xs = new XmlSerializer(typeof(TestResultXml));
            var sw = new StreamWriter(path);
            xs.Serialize(sw, result);
        }
    }
}
