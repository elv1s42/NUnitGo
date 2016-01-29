namespace NunitGo.Utils
{
    public class Output
    {
        public struct Files
        {
            public static string TestOutputFile = "out" + FileType.Default;
            public static string TestHtmlFile = "Test" + FileType.Html;
            public static string TestXmlFile = "Test" + FileType.Xml;
            public static string TestListFile = "TestList" + FileType.Html;
            public static string TestStatisticsFile = "TestStatistics" + FileType.Html;
            public static string TimelineFile = "Timeline" + FileType.Html;
            public static string FullReportFile = "index" + FileType.Html;
            public static string ReportStyleFile = "reportStyle" + FileType.Css;
        }

        public struct FileType
        {
            public static string Default = ".html";
            public static string Txt = ".txt";
            public static string Xml = ".xml";
            public static string Html = ".html";
            public static string Css = ".css";
        }
    }
}
