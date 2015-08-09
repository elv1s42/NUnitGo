using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Utils
{
    public static class HelperSerializable
    {
        public static void SerializeObject<T>(this T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in SerializeObject<T> {0}, {1}", e.Message, e.StackTrace));
            }
        }

        public static T DeserializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return default(T); }

            var objectOut = default(T);

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                var xmlString = xmlDocument.OuterXml;

                using (var read = new StringReader(xmlString))
                {
                    var outType = typeof(T);

                    var serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception e)
            {
                Log.Write(String.Format("Exception in DeserializeObject<T> {0}, {1}", e.Message, e.StackTrace));
            }

            return objectOut;
        }
    }
}
