using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using HtmlCustomElements;
using NunitResultAnalyzer;

namespace ConsoleReportGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("No arguments specified");
                return;
            }
            if (args.Count() <= 1)
            {
                Console.WriteLine("Two arguments required");
                return;
            }

            //TODO: Remove this, use ConfigBase:
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            var outputPath =
                XDocument.Load(path + "/config.xml")
                    .Descendants()
                    .First(x => x.Name.LocalName.Equals("output-path"))
                    .Value + @"\";
            //------------

            var xmlPath = args[0];
            var screenshotsPath = args[1];

            Console.WriteLine("XML file: '{0}'", xmlPath);
            Console.WriteLine("Screenshots: '{0}'", screenshotsPath);
            Console.WriteLine("Output: '{0}'", outputPath);

            var reader = new NunitXmlReader(xmlPath);
            var results = reader.Deserialize();
            var resultAnalyzer = new ResultsAnalyzer(results, screenshotsPath);
            var fullSuite = resultAnalyzer.GetFullSuite();

            reader.Save(fullSuite, outputPath + "Test.xml");

            PageGenerator.GenerateReport(fullSuite, outputPath);

        }
    }
}
