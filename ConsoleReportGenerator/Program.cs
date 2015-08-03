using System;
using System.Linq;
using HtmlCustomElements;
using NunitResultAnalyzer;
using Utils;

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
                Console.WriteLine("One argument required");
                return;
            }

            var outputPath = Locator.Output;
            var xmlPath = args[0];
            var screenshotsPath = Locator.Screenshots;

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
