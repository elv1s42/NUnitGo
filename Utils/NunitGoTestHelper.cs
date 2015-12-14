using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Utils.XmlTypes;

namespace Utils
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

        public static void AddScreenshots(this NunitGoTest test,
            List<NunitGoTestScreenshot> screens)
        {
            test.Screenshots = new List<NunitGoTestScreenshot>();
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
    }
}
