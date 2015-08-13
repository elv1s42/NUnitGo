using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot()
        {
            var format = ImageFormat.Png;
            var now = DateTime.Now;
            var screenPath = Helper.Screenshots;
            
            Directory.CreateDirectory(screenPath);
            
            var screenName = String.Format("screenshot_{0}.{1}",
                now.ToString("yyyyMMddHHmmssfff"), format.ToString().ToLower());

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
                    bmpScreenCapture.Save(screenPath + @"\" + screenName, format);
                }
            }
        }
    }
}
