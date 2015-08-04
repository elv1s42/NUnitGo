using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
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

            var reader = new NunitXmlReader(xmlPath);
            var results = reader.Deserialize();

            List<ExtraTestInfo> extraInfo;
            var xs = new XmlSerializer(typeof(List<ExtraTestInfo>));
            using(var sr = new StreamReader(Locator.Output + @"\ExtraInfo.xml"))
            {
               extraInfo = (List<ExtraTestInfo>)xs.Deserialize(sr);
            }
            
            var resultAnalyzer = new ResultsAnalyzer(results, extraInfo);
            var fullSuite = resultAnalyzer.GetFullSuite();

            PageGenerator.GenerateReport(fullSuite, outputPath);
        }
    }
}
