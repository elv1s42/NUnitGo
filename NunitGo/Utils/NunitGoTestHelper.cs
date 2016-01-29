using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NunitGo.NunitGoItems;
using ScreenshotTaker;

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
            return XmlHelper.Load<NunitGoTest>(fullPath);
        }
        
        public static List<NunitGoTest> GetTests(string localOutputPath)
        {
            var tests = new List<NunitGoTest>();
            var filesFound = new List<String>();
            filesFound.AddRange(Directory.GetFiles(localOutputPath, Output.Outputs.TestXml, SearchOption.AllDirectories));
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

        public static void TakeScreenshot(this NunitGoTest test, string screenshotsPath, DateTime creationTime = default(DateTime))
        {
            creationTime = creationTime.Equals(default(DateTime)) ? DateTime.Now : creationTime;
            test.Screenshots.Add(new Screenshot(creationTime));
            Taker.TakeScreenshot(creationTime, screenshotsPath);
        }

        public static void AddScreenshots(this NunitGoTest test,
            List<Screenshot> screens)
        {
            if(!test.Screenshots.Any()) test.Screenshots = new List<Screenshot>();
            var start = test.DateTimeStart;
            var end = test.DateTimeFinish;

            foreach (var screen in screens.Where(screen => screen.Date >= start && screen.Date <= end))
            {
                if (!test.Screenshots.Any(x => x.Name.Equals(screen.Name)))
                {
                    test.Screenshots.Add(screen);
                }
            }
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
