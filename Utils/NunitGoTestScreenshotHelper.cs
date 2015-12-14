using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Utils
{
    public class NunitGoTestScreenshotHelper
    {
        public static String[] GetFilesWithFilters(String searchFolder, String[] filters, bool isRecursive)
        {
            var filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        public static List<NunitGoTestScreenshot> GetScreenshots(string path)
        {
            var result = new List<NunitGoTestScreenshot>();
            var filters = new[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = GetFilesWithFilters(path, filters, false);

            foreach (var fileInfo in files.Select(file => new FileInfo(file)))
            {
                fileInfo.Refresh();

                result.Add(new NunitGoTestScreenshot { Name = fileInfo.Name, Date = fileInfo.CreationTime });
            }

            return result;
        }
    }
}
