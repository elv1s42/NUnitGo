namespace NunitGoAddin
{
    public static class Log
    {
        private static string GetFilePath()
        {
            return "";
        }
        
        public static void Write(string msg)
        {
            var sw = System.IO.File.AppendText(GetFilePath() + "NunitGoAddinLog.txt");
            try
            {
                var logLine = System.String.Format("{0:G}: {1}", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
