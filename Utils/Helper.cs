using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Utils
{
    public static class Helper
    {
        public static string Output;
        public static string Screenshots;
        public static bool AfterTestGeneration;
        public static bool AfterSuiteGeneration;
        public static bool SaveOutput;

        private static string GetPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            return path;
        }

        private static string GetValue(string name)
        {
            var path = GetPath();
            return XDocument.Load(path + "/config.xml")
                .Descendants()
                .First(x => x.Name.LocalName.Equals(name))
                .Value;
        }

        static Helper()
        {
            Output = GetValue("output-path");
            Screenshots = Output + @"\Screenshots";
            AfterTestGeneration = bool.Parse(GetValue("after-test-generation"));
            AfterSuiteGeneration = bool.Parse(GetValue("after-suite-generation"));
            SaveOutput = bool.Parse(GetValue("save-output"));
        }

        public static void CreateDirectories()
        {
            if (Directory.Exists(Output)) return;
            Directory.CreateDirectory(Output);
            Directory.CreateDirectory(Output + @"\Attachments");
            Directory.CreateDirectory(Output + @"\Screenshots");
        }

        private static string GetScreenName(DateTime now, ImageFormat format = null)
        {
            format = format ?? ImageFormat.Png;
            return String.Format("screenshot_{0}.{1}", now.ToString("yyyyMMddHHmmssfff"), format.ToString().ToLower());
        }

        public static void TakeScreenshot(DateTime creationTime = default(DateTime))
        {
            var format = ImageFormat.Png;
            var now = DateTime.Now;
            var screenPath = Screenshots + @"\";
            creationTime = creationTime.Equals(default(DateTime)) ? now : creationTime;
            var screenName = GetScreenName(creationTime, format);

            using (var bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);

                    var file = screenPath + screenName;
                    bmpScreenCapture.Save(file, format);
                    var fileInfo = new FileInfo(file);
                    fileInfo.Refresh();
                    fileInfo.CreationTime = creationTime;

                }
            }
        }
    }
}
