using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class CultureInfoXml
    {
        public CultureInfoXml()
        {
            CurrentCulture = "";
            CurrentUiculture = "";
        }

        [XmlAttribute("current-culture")]
        public string CurrentCulture { get; set; }

        [XmlAttribute("current-uiculture")]
        public string CurrentUiculture { get; set; }
    }
}
