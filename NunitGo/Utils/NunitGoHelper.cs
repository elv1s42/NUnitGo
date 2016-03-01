using System;
using System.IO;
using System.Reflection;
using NunitGoCore.NunitGoItems;

namespace NunitGoCore.Utils
{
    public static class NunitGoHelper
    {
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
            try
            {
                var path = GetPath();
                Directory.SetCurrentDirectory(path);
                var configuration = NunitGoConfigurationHelper.Load(@"NUnitGoConfig.xml");
                //var configuration = NunitGoConfigurationHelper.Load(Path.Combine(path, "NUnitGoConfig.xml"));
                Configuration = configuration;
            }
            catch (Exception ex)
            {
                Log.Exception(ex, GetPath(), "Exception in NunitGoHelper constructor");
            }
        }
    }
}
