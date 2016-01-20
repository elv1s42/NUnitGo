using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NunitGo.NunitGoItems;
using ScreenshotTaker;

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

        private static NunitGoTest Load(string fullPath)
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
            filesFound.AddRange(Directory.GetFiles(NunitGoHelper.Output, Output.Outputs.TestXml/*"*.xml"*/, SearchOption.AllDirectories));
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

        public static string TakeScreenshot(this NunitGoTest test, DateTime creationTime = default(DateTime))
        {
            creationTime = creationTime.Equals(default(DateTime)) ? DateTime.Now : creationTime;
            test.Screenshots.Add(new Screenshot(creationTime));
            return Taker.TakeScreenshot(creationTime, NunitGoHelper.Screenshots + @"\");
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
