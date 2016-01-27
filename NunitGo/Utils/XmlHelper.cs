using System;
using System.IO;
using System.Xml.Serialization;

namespace NunitGo.Utils
{
    public static class XmlHelper
    {
        public static void Save<T>(this T t, string fullPath) where T : class 
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    ser.Serialize(fs, t);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Save exception");
            }
        }

        public static T Load<T>(string fullPath) where T : class
        {
            try
            {
                T t;
                var ser = new XmlSerializer(typeof(T));
                using (var fs = new FileStream(fullPath, FileMode.Open))
                {
                    t = (T)ser.Deserialize(fs);
                }
                return t;
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Load exception");
                return null;
            }
        }
    }
}
