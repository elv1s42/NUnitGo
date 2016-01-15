using System;
using System.Collections.Generic;

namespace NunitGo.CustomElements
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (input == null)
            {
                yield break;
            }

            using (var reader = new System.IO.StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static string ToCamelCase(this string targetString)
        {
            // If there are 0 or 1 characters, just return the string:
            if (targetString == null || targetString.Length < 2)
                return targetString;

            // Split the string into words:
            var words = targetString.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words:
            var result = words[0].ToLower();
            for (var i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
    }
}
