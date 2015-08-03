using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Utils
{
    public static class Locator
    {
        public static string Output;
        public static string Screenshots;
        public static string Results;

        static Locator()
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
        }
    }
}
