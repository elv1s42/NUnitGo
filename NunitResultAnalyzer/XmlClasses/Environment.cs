using System.Xml.Serialization;

namespace NunitResultAnalyzer.XmlClasses
{
    public class Environment
    {
        public Environment(string nunitVersion)
        {
            NunitVersion = nunitVersion;
            ClrVersion = System.Environment.Version.ToString();
            OsVersion = System.Environment.OSVersion.VersionString;
            Platform = System.Environment.OSVersion.Platform.ToString();
            Cwd = "";
            MachineName = System.Environment.MachineName;
            User = System.Environment.UserName;
            UserDomain = System.Environment.UserDomainName;
        }

        public Environment()
        {
            NunitVersion = "";
            ClrVersion = "";
            OsVersion = "";
            Platform = "";
            Cwd = "";
            MachineName = "";
            User = "";
            UserDomain = "";
        }

        [XmlAttribute("nunit-version")]
        public string NunitVersion { get; set; }

        [XmlAttribute("clr-version")]
        public string ClrVersion { get; set; }

        [XmlAttribute("os-version")]
        public string OsVersion { get; set; }

        [XmlAttribute("platform")]
        public string Platform { get; set; }

        [XmlAttribute("cwd")]
        public string Cwd { get; set; }

        [XmlAttribute("machine-name")]
        public string MachineName { get; set; }

        [XmlAttribute("user")]
        public string User { get; set; }

        [XmlAttribute("user-domain")]
        public string UserDomain { get; set; }
    }
}
