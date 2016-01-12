using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace NunitGo.Utils
{
    public static class NunitGoTestHelper
    {
        public static void Save(this NunitGoTest test, string fullPath)
        {
            var ser = new XmlSerializer(typeof(NunitGoTest));
            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, test);
            }
        }

        public static NunitGoTest Load(string fullPath)
        {
            NunitGoTest test;
            var ser = new XmlSerializer(typeof(NunitGoTest));
            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                test = (NunitGoTest) ser.Deserialize(fs);
            }
            return test;
        }
        
        public static List<NunitGoTest> GetTests()
        {
            var tests = new List<NunitGoTest>();
            var filesFound = new List<String>();
            filesFound.AddRange(Directory.GetFiles(Helper.Output, "*.xml", SearchOption.AllDirectories));
            foreach (var file in filesFound)
            {
                try
                {
                    tests.Add(Load(file));
                }
                catch (Exception)
                {
                }
            }
            return tests;
        }

        public static void TakeScreenshot(this NunitGoTest test)
        {
            var now = DateTime.Now;
            Helper.TakeScreenshot(now);
            test.Screenshots.Add(new NunitGoTestScreenshot(now));
        }

        public static void TakeScreenshot(this NunitGoTest test, DateTime date)
        {
            Helper.TakeScreenshot(date);
            test.Screenshots.Add(new NunitGoTestScreenshot(date));
        }

        public static void AddScreenshots(this NunitGoTest test,
            List<NunitGoTestScreenshot> screens)
        {
            if(!test.Screenshots.Any()) test.Screenshots = new List<NunitGoTestScreenshot>();
            var start = test.DateTimeStart;
            var end = test.DateTimeFinish;

            foreach (var screen in screens.Where(screen => screen.Date >= start && screen.Date <= end))
            {
                test.Screenshots.Add(screen);
            }
        }

        public static void AddScreenshots(this List<NunitGoTest> tests,
            List<NunitGoTestScreenshot> screens)
        {
            foreach (var test in tests)
            {
                test.AddScreenshots(screens);
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
