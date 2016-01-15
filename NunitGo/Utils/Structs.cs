using System;

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
            public static string Test = "test" + Type.Default;
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

    public static class Ids
    {
        public static string GetProjectId()
        {
            return "project-" + Guid.NewGuid();
        }

        public static string GetClassId()
        {
            return "class-" + Guid.NewGuid();
        }

        public static string GetTestId(string guid)
        {
            return "test-" + guid;
        }

        public static string GetTestModalId(string guid)
        {
            return "modal-test-" + guid;
        }

        public static string GetBackgroundId(string modalId)
        {
            return "background-" + modalId;
        }
    }
}
