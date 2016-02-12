using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    public static class NunitGoTestHelper
    {
        public static void Save(this NunitGoTest test, string fullPath)
        {
            XmlHelper.Save(test, fullPath);
        }

        private static NunitGoTest Load(string fullPath)
        {
            return XmlHelper.Load<NunitGoTest>(fullPath, "Excteption while loading NunitGoTest, Path = " + fullPath);
        }
        
        public static List<NunitGoTest> GetTests(string attachmentsPath)
        {
            var tests = new List<NunitGoTest>();
            var filesFound = new List<string>();
            filesFound.AddRange(Directory.GetFiles(attachmentsPath, Output.Files.TestXmlFile, SearchOption.AllDirectories));

            var folders = Directory.GetDirectories(attachmentsPath);
                //.Where(x => Regex
                //    .Match(Path.GetFileName(x), @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}")
                //    .Success);
            
            foreach (var folder in folders)
            {
                try
                {
                    var dirInfo = new DirectoryInfo(folder);
                    var newestFile = dirInfo.GetFiles("*.xml").OrderByDescending(x => x.CreationTime).First().FullName;
                    tests.Add(Load(newestFile));
                }
                catch (Exception ex)
                {
                    Log.Exception(ex, "Exception while loading test xml file");
                }
            }

            return tests;
        }
        
        public static DateTime GetStartDate(this List<NunitGoTest> tests)
        {
            return tests.OrderBy(x => x.DateTimeStart).First().DateTimeStart;
        }

        public static DateTime GetFinishDate(this List<NunitGoTest> tests)
        {
            return tests.OrderBy(x => x.DateTimeFinish).Last().DateTimeFinish;
        }

        public static TimeSpan Duration(this List<NunitGoTest> tests)
        {
            return (GetFinishDate(tests) - GetStartDate(tests));
        }
    }
}
