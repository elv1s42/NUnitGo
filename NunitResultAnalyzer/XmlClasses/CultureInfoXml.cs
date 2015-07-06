using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class CultureInfoXml
    {
        [XmlAttribute("current-culture")]
        public string CurrentCulture { get; set; }

        [XmlAttribute("current-uiculture")]
        public string CurrentUiculture { get; set; }
    }
}
