using System;
using System.Linq;
using HtmlCustomElements;
using NunitResultAnalyzer;
using NunitResultAnalyzer.XmlClasses;
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
            GenerateReport();
        }

        public static void GenerateReport()
        {
            var outPath = Locator.Output;
            var extraInfo = ExtraTestInfo.Load(outPath + @"\ExtraInfo.xml");
            var loadedXmlReults = TestResultXml.Load(outPath + @"\Result.xml");
            var testResults = new TestResults(loadedXmlReults);
            var fullSuite = ResultsAnalyzer.GetFullSuite(testResults, extraInfo);

            PageGenerator.GenerateReport(fullSuite, outPath);
        }
    }
}
