using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NunitGo.NunitGoItems.Screenshots
{
    public static class Taker
    {
        private static void SaveScreen(this Image img, string path, string name, ImageFormat format)
        {
            var fullPath = path + @"\" + name + "." + format;
            if (!Directory.Exists(path.TrimEnd('\\')))
            {
                Directory.CreateDirectory(path.TrimEnd('\\'));
            }
            Console.WriteLine("Saving screenshot: " + fullPath);
            img.Save(fullPath, format);
            Console.WriteLine("Saved.");
        }

        public static string GetScreenName(DateTime now, ImageFormat format = null)
        {
            format = format ?? ImageFormat.Png;
            return string.Format("screenshot_{0}.{1}", now.ToString("yyyyMMddHHmmssfff"), format.ToString().ToLower());
        }

        public static string GetPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            return path + @"\_Screenshots";
        }

        public static void DeleteScreenshots(string path = "")
        {
            var folderPath = path.Equals("") ? GetPath() : path;
            var folder = new DirectoryInfo(folderPath);

            foreach (var file in folder.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in folder.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static string TakeScreenshot(DateTime creationTime = default(DateTime), string screenPath = "")
        {
            var format = ImageFormat.Png;
            var now = DateTime.Now;
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

                    var file = (screenPath.Equals("") ? GetPath() : screenPath) + screenName;
                    bmpScreenCapture.Save(file, format);
                    var fileInfo = new FileInfo(file);
                    fileInfo.Refresh();
                    fileInfo.CreationTime = creationTime;

                }
            }
            return screenName;
        }

        public static void TakeScreenshot(string path = "", string name = "")
        {
            var now = DateTime.Now;
            var screenPath = path.Equals("") ? GetPath() : path;
            var screenName = name.Equals("") ? string.Format("screenshot_{0}", now.ToString("yyyyMMddHHmmssfff")) : name;

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
                    bmpScreenCapture.SaveScreen(screenPath, screenName, ImageFormat.Png);
                }
            }
        }
    }
}
