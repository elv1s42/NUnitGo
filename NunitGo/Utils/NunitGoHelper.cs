using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace NunitGo.Utils
{
    public static class NunitGoHelper
    {
        public static string Output;
        public static string Screenshots;
        public static bool AfterTestGeneration;
        public static bool AfterSuiteGeneration;
        public static bool SaveOutput;

        private static string GetPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            return path;
        }

        private static string GetValue(string name)
        {
            var path = GetPath();
            return XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals(name))
                .Value;
        }

        static NunitGoHelper()
        {
            Output = GetValue("output-path");
            Screenshots = Output + @"\Screenshots";
            AfterTestGeneration = bool.Parse(GetValue("after-test-generation"));
            AfterSuiteGeneration = bool.Parse(GetValue("after-suite-generation"));
            SaveOutput = bool.Parse(GetValue("save-output"));
        }

        public static void CreateDirectories()
        {
            //if (Directory.Exists(Output)) return;
            Directory.CreateDirectory(Output);
            Directory.CreateDirectory(Output + @"\Attachments");
            Directory.CreateDirectory(Output + @"\Screenshots");
        }
    }
}
