using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace Utils.Extensions
{
    public static class ExtraTestInfoExtension
    {
        public static void Save(this List<ExtraTestInfo> list, string path)
        {
            var xs = new XmlSerializer(typeof(List<ExtraTestInfo>));
            var sw = new StreamWriter(path);
            xs.Serialize(sw, list);
        }
    }
}
