using System.IO;
using System.Xml.Serialization;
using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    internal static class NunitGoConfigurationHelper
    {
        public static void Save(this NunitGoConfiguration configuration, string fullPath)
        {
            var ser = new XmlSerializer(typeof(NunitGoConfiguration));
            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, configuration);
            }
        }

        public static NunitGoConfiguration Load(string fullPath)
        {
            NunitGoConfiguration configuration;
            var ser = new XmlSerializer(typeof(NunitGoConfiguration));
            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                configuration = (NunitGoConfiguration)ser.Deserialize(fs);
            }
            return configuration;
        }
    }
}
