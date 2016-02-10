using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        public static List<NunitGoTest> GetTests(string localOutputPath)
        {
            var tests = new List<NunitGoTest>();
            var filesFound = new List<string>();
            filesFound.AddRange(Directory.GetFiles(localOutputPath, Output.Files.TestXmlFile, SearchOption.AllDirectories));
            foreach (var file in filesFound)
            {
                try
                {
                    tests.Add(Load(file));
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
