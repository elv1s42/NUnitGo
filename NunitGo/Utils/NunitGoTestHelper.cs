using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NunitGo.NunitGoItems;

namespace NunitGo.Utils
{
    public static class NunitGoTestHelper
    {
        public static void SaveAsXml(this NunitGoTest test, string fullPath)
        {
            test.Save(fullPath);
        }

        private static NunitGoTest Load(string fullPath)
        {
            return XmlHelper.Load<NunitGoTest>(fullPath, "Excteption while loading NunitGoTest, Path = " + fullPath);
        }

        public static List<NunitGoTest> GetTestsFromFolder(string folder)
        {
            var tests = new List<NunitGoTest>();
            try
            {
                var dirInfo = new DirectoryInfo(folder);
                var files = dirInfo.GetFiles("*.xml");
                tests.AddRange(files.Select(fileInfo => Load(fileInfo.FullName)));
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception while loading test xml file");
            }
            return tests;
        }

        public static List<NunitGoTest> GetNewestTests(string attachmentsPath)
        {
            var tests = new List<NunitGoTest>();
            var folders = Directory.GetDirectories(attachmentsPath);

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
