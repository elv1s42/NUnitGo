using System;

namespace NunitGo.Utils
{
    public class OutputType
    {
        public struct Type
        {
            public static string Default = ".html";
            public static string Txt = ".txt";
            public static string Html = ".html";
        }
    }

    public class Structs
    {
        public struct Outputs
        {
            public static string Out = "out" + OutputType.Type.Default;
            public static string Log = "log" + OutputType.Type.Default;
            public static string Error = "error" + OutputType.Type.Default;
            public static string Trace = "trace" + OutputType.Type.Default;
            public static string Test = "test" + OutputType.Type.Default;
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
