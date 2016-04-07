using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnitGoCore.NunitGoItems;

namespace NUnitGoCore.Utils
{
    public static class NunitGoTestHelper
    {
        public static void SaveAsXml(this NunitGoTest test, string fullPath)
        {
            test.Save(fullPath);
        }
        
        public static void DeleteTestFiles(this NunitGoTest test, string screenshotsPath)
        {
            try
            {
                var finishDate = test.DateTimeFinish;
                var scriptPath = Path.Combine(test.AttachmentsPath, Output.Files.GetTestHistoryScriptName(finishDate));
                var htmlPath = Path.Combine(test.AttachmentsPath, Output.Files.GetTestHtmlName(finishDate));
                var xmlPath = Path.Combine(test.AttachmentsPath, Output.Files.GetTestXmlName(finishDate));
                File.Delete(scriptPath);
                File.Delete(htmlPath);
                File.Delete(xmlPath);
                foreach (var screenshot in test.Screenshots)
                {
                    File.Delete(Path.Combine(screenshotsPath, screenshot.Name));
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception in CleanUpTestFiles");
            }
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
