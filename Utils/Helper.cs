using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Utils
{
    public static class Helper
    {
        public static string Output;
        public static string Screenshots;
        public static string Results;
        public static bool AfterTestGeneration;
        public static bool AfterSuiteGeneration;

        static Helper()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            Output = XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals("output-path"))
                .Value;

            Screenshots = XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals("screenshots-path"))
                .Value;

            Results = XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals("results-path"))
                .Value;

            AfterTestGeneration = bool.Parse(XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals("after-test-generation"))
                .Value);

            AfterSuiteGeneration = bool.Parse(XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals("after-suite-generation"))
                .Value);
        }
    }
}
