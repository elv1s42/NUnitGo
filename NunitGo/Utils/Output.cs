using System;

namespace NunitGo.Utils
{
    public class Output
    {
        public struct Files
        {
            public static string TestHtmlFile = "Test" + FileType.Html;
            public static string TestXmlFile = "Test" + FileType.Xml;
            
            public static string TestListFile = "TestList" + FileType.Html;
            public static string TestStatisticsFile = "TestStatistics" + FileType.Html;
            public static string TimelineFile = "Timeline" + FileType.Html;
            public static string FullReportFile = "index" + FileType.Html;
            public static string ReportStyleFile = "reportStyle" + FileType.Css;

            public static string GetTestXmlName(DateTime finishTime)
            {
                return "Test-" + finishTime.ToString("yy-MM-dd-HH-mm-ss-fff") + FileType.Xml;
            }

            public static string GetTestHtmlName(DateTime finishTime)
            {
                return "Test-" + finishTime.ToString("yy-MM-dd-HH-mm-ss-fff") + FileType.Html;
            }
        }

        public static string GetHistoryChartId(Guid testGuid, DateTime testFinishDateTime)
        {
            return "history-" + testGuid.ToString().ToLower() + testFinishDateTime.ToString("-yy-MM-dd-HH-mm-ss-fff");
        }

        public static string GetStatsPieId()
        {
            return "statistics-pie";
        }

        public static string GetTestHistoryScriptName(DateTime testFinishDateTime)
        {
            return "test-script-" + testFinishDateTime.ToString("yy-MM-dd-HH-mm-ss-fff") + ".js";
        }

        public static string GetMainStatsScriptName()
        {
            return "stats.js";
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
