using System;
using System.Linq;
using HtmlCustomElements;
using NunitResultAnalyzer;
using Utils;
using Utils.XmlTypes;

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
            GenerateReportFromResults();
        }

        public static void GenerateReportFromResults()
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

            var extraInfo = ExtraTestInfo.Load(Locator.Output + @"\ExtraInfo.xml");

            var fullSuite = ResultsAnalyzer.GetFullSuite(results, extraInfo);

            PageGenerator.GenerateReport(fullSuite, outputPath);
        }
    }
}
