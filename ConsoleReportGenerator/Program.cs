using System;
using System.Linq;
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
            if (args.Count() <= 2)
            {
                Console.WriteLine("Three arguments required");
                return;
            }

            var xmlPath = args[0];
            var screenshotPath = args[1];
            var outputPath = args[2];

            Console.WriteLine("XML file: '{0}'", xmlPath);
            Console.WriteLine("Screenshots: '{0}'", screenshotPath);
            Console.WriteLine("Output: '{0}'", outputPath);

            var reader = new NunitXmlReader(xmlPath);
            var results = reader.Deserialize();
            var resultAnalyzer = new ResultsAnalyzer(results, screenshotPath);
            var fullSuite = resultAnalyzer.GetFullSuite();
            
            PageGenerator.GenerateReport(fullSuite, outputPath);
            
        }
    }
}
