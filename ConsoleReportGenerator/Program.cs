using System;
using System.Linq;
using HtmlCustomElements;
using NunitGoAddin;
using NunitResultAnalyzer;
using Utils;

namespace ConsoleReportGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                Console.WriteLine("No arguments needed");
                return;
            }
            GenerateReport();
        }

        public static void GenerateReport()
        {
            var outputPath = Locator.Output;
            var xmlPath = Locator.Results;
            var screenshotsPath = Locator.Screenshots;

            Console.WriteLine("XML file: '{0}'", xmlPath);
            Console.WriteLine("Screenshots: '{0}'", screenshotsPath);
            Console.WriteLine("Output: '{0}'", outputPath);

            var results = NunitXmlReader.Deserialize(xmlPath);
            //NunitXmlReader.Save(results, Path.Combine(Locator.Output, "ExportTest.xml"));
            //NunitXmlReader.Save(new TestResults(), Path.Combine(Locator.Output, "EmptyResults.xml"));

            var extraInfo = ExtraTestInfo.Get(Locator.Output + @"\ExtraInfo.xml");

            var fullSuite = ResultsAnalyzer.GetFullSuite(results, extraInfo);

            PageGenerator.GenerateReport(fullSuite, outputPath);
        }
    }
}
