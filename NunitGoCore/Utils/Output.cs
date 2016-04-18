using System;
using System.Collections.Generic;

namespace NUnitGoCore.Utils
{
    public class Output
    {
        public struct Files
        {
            public const string TestHtmlFile = "Test" + FileType.Html;
            public const string TestXmlFile = "Test" + FileType.Xml;
            
            public const string TestListFile = "TestList" + FileType.Html;
            public const string TestStatisticsFile = "TestStatistics" + FileType.Html;
            public const string TimelineFile = "Timeline" + FileType.Html;
            public const string FullReportFile = "index" + FileType.Html;
            public const string ReportStyleFile = "reportStyle" + FileType.Css;
            public const string PrimerStyleFile = "primer" + FileType.Css;
            public const string OcticonStyleFile = "octicons" + FileType.Css;
            public const string StatsScript = "stats" + FileType.Js;
            
            public static List<string> OcticonsStyleFiles = new List<string>
            {
                "octicons" + FileType.Css,
                "octicons" + FileType.Eot,
                "octicons" + FileType.Svg,
                "octicons" + FileType.Ttf,
                "octicons" + FileType.Woff
            };

            public static string GetTestXmlName(DateTime finishTime)
            {
                return "Test-" + finishTime.ToString("yy-MM-dd-HH-mm-ss-fff") + FileType.Xml;
            }

            public static string GetTestHtmlName(DateTime finishTime)
            {
                return "Test-" + finishTime.ToString("yy-MM-dd-HH-mm-ss-fff") + FileType.Html;
            }

            public static string GetTestHistoryScriptName(DateTime testFinishDateTime)
            {
                return "test-script-" + testFinishDateTime.ToString("yy-MM-dd-HH-mm-ss-fff") + ".js";
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

        public static string GetScreenshotsPath(string localOutputPath)
        {
            return localOutputPath + @"\Screenshots\";
        }

        public static string GetAttachmentsPath(string localOutputPath)
        {
            return localOutputPath + @"\Attachments\";
        }
        
        public struct FileType
        {
            public const string Default = ".html";
            public const string Txt = ".txt";
            public const string Xml = ".xml";
            public const string Html = ".html";
            public const string Css = ".css";
            public const string Js = ".js";
            public const string Ttf = ".ttf";
            public const string Eot = ".eot";
            public const string Woff = ".woff";
            public const string Svg = ".svg";
        }
    }
}
