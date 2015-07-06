using System;
using System.IO;
using System.Linq;
using System.Reflection;
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

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            var page = new HtmlPage("NUnitGo Report");
            page.AddToBody("TEST1!!!!!!!");
            page.AddToBody("TEST2!!!!!!!");
            page.AddToBody("TEST3!!!!!!!");
            page.AddToBody("TEST1: тест!!!!!!!");
            page.AddToBody("TEST2: тест тест!!!!!!!");
            page.AddToBody("TEST3: тест тест тест!!!!!!!");
            page.SavePage(path, "testingPage"); //TODO: change to output path

            //Console.ReadKey();
        }
    }
}
