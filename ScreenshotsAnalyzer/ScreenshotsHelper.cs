using System;
using System.Collections.Generic;
using System.IO;

namespace ScreenshotsAnalyzer
{
    public static class ScreenshotsHelper
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

        public static Dictionary<string, DateTime> GetScreenshots(string path)
        {
            var result = new Dictionary<string, DateTime>();
            var filters = new[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = GetFilesWithFilters(path, filters, false);
            
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                fileInfo.Refresh();
                result.Add(file, fileInfo.CreationTime);
            }

            return result;
        }
    }
}
