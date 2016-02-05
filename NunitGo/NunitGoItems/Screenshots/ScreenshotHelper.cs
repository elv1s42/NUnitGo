using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NunitGo.NunitGoItems.Screenshots
{
    public class ScreenshotHelper
    {
        public static string[] GetFilesWithFilters(string searchFolder, string[] filters, bool isRecursive)
        {
            var filesFound = new List<string>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, string.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        public static List<Screenshot> GetScreenshots(string path = "")
        {
            if (path.Equals("")) path = Taker.GetPath();
            var result = new List<Screenshot>();
            var filters = new[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var files = GetFilesWithFilters(path, filters, false);

            foreach (var fileInfo in files.Select(file => new FileInfo(file)))
            {
                fileInfo.Refresh();

                result.Add(new Screenshot { Name = fileInfo.Name, Date = fileInfo.CreationTime });
            }

            return result;
        }
    }
}
