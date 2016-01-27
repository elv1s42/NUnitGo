using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    internal static class NunitGoConfigurationHelper
    {
        public static void Save(this NunitGoConfiguration configuration, string fullPath)
        {
            XmlHelper.Save(configuration, fullPath);
        }

        public static NunitGoConfiguration Load(string fullPath)
        {           
            return XmlHelper.Load<NunitGoConfiguration>(fullPath);
        }
    }
}
