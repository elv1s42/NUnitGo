﻿using System;
using System.IO;

namespace Utils
{
    public static class Log
    {
        private static string GetFilePath()
        {
            return Helper.Output;
        }

        public static void Clean()
        {
            try
            {
                File.WriteAllText(GetFilePath() + @"\NunitGoAddinLog.txt", String.Empty);
            }
            catch (Exception e)
            {
                Write("Exception in Clean() method: " + e.Message + " " + e.StackTrace);
            }
        }

        public static void Write(string msg)
        {
            var sw = File.AppendText(GetFilePath() + @"\NunitGoAddinLog.txt");
            try
            {
                var logLine = String.Format("{0:G}: {1}", DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}