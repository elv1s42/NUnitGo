namespace NunitGo.Utils
{
    public class Output
    {
        public struct Outputs
        {
            public static string Out = "out" + Type.Default;
            public static string Log = "log" + Type.Default;
            public static string Error = "error" + Type.Default;
            public static string Trace = "trace" + Type.Default;
            public static string Test = "Test" + Type.Html;
            public static string TestList = "TestList" + Type.Html;
            public static string TestStatistics = "TestStatistics" + Type.Html;
            public static string Timeline = "Timeline" + Type.Html;
            public static string FullReport = "index" + Type.Html;
            public static string ReportStyle = "reportStyle" + Type.Css;
        }

        public struct Type
        {
            public static string Default = ".html";
            public static string Txt = ".txt";
            public static string Html = ".html";
            public static string Css = ".css";
        }
    }
}
