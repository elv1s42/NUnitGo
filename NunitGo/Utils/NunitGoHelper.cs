using System;
using System.IO;
using System.Reflection;

namespace NunitGo.Utils
{
    public static class NunitGoHelper
    {
        public static string Output;
        public static string Screenshots;
        public static string Attachments;
        public static bool GenerateReport;
        public static bool TakeScreenshotAfterTestFailed;

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
