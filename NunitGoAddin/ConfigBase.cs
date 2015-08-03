using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace NunitGoAddin
{
    public static class ConfigBase
    {
        public static string Location;

        static ConfigBase()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            var outputPath =
                XDocument.Load(path + "/config.xml")
                    .Descendants()
                    .First(x => x.Name.LocalName.Equals("output-path"))
                    .Value + @"\";

            Location = outputPath;
        }
    }
}
