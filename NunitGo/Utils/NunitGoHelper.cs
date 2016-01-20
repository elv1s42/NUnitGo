using System;
using System.IO;
using System.Reflection;
using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    public static class NunitGoHelper
    {
        public static string Output;
        public static string Screenshots;
        public static string Attachments;
        public static bool GenerateReport;
        public static bool TakeScreenshotAfterTestFailed;
        public static NunitGoConfiguration Configuration;

        private static string GetPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            return path;
        }

        static NunitGoHelper()
        {
            var configuration = NunitGoConfigurationHelper.Load(Path.Combine(GetPath(), "NUnitGoConfig.xml"));
            Output = configuration.LocalOutputPath;
            Screenshots = Output + @"\Screenshots";
            Attachments = Output + @"\Attachments";
            GenerateReport = configuration.GenerateReport;
            TakeScreenshotAfterTestFailed = configuration.TakeScreenshotAfterTestFailed;
            Configuration = configuration;
        }

        public static void CreateDirectories()
        {
            Directory.CreateDirectory(Output);
            Directory.CreateDirectory(Screenshots);
            Directory.CreateDirectory(Attachments);
        }
    }
}
