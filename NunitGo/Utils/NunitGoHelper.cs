﻿using System;
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
        public static bool GenerateReport;
        public static bool TakeScreenshotAfterTestFailed;

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
            return XDocument.Load(path + "/NUnitGoConfig.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals(name))
                .Value;
        }

        static NunitGoHelper()
        {
            Output = GetValue("LocalOutputPath");
            Screenshots = Output + @"\Screenshots";
            GenerateReport = bool.Parse(GetValue("GenerateReport"));
            TakeScreenshotAfterTestFailed = bool.Parse(GetValue("TakeScreenshotAfterTestFailed"));
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
