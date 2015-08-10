using System.Globalization;
using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class CultureInfoXml
    {
        public CultureInfoXml()
        {
            CurrentCulture = CultureInfo.CurrentCulture.Name;
            CurrentUiculture = CultureInfo.CurrentUICulture.Name;
        }

        [XmlAttribute("current-culture")]
        public string CurrentCulture { get; set; }

        [XmlAttribute("current-uiculture")]
        public string CurrentUiculture { get; set; }
    }
}
