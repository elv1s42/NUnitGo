namespace Utils
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
        }
    }
}
