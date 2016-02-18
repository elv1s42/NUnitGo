using System.IO;
using System.Linq;
using NunitGo.CustomElements;
using NunitGo.CustomElements.ReportSections.MainInformationSection;
using NunitGo.Utils;

namespace NunitGoReport
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = NunitGoHelper.Configuration;
            var outputPath = config.LocalOutputPath;
            var attachmentsPath = outputPath + @"\Attachments\";

            PageGenerator.GenerateStyleFile(outputPath);

            var tests = NunitGoTestHelper.GetNewestTests(attachmentsPath).OrderBy(x => x.DateTimeFinish).ToList();
            var stats = new MainStatistics(tests);
            var statsChart = new MainInfoChart(stats, Output.GetStatsPieId());
            statsChart.SaveScript(outputPath);
            tests.GenerateTimelinePage(Path.Combine(outputPath, Output.Files.TimelineFile));
            stats.GenerateMainStatisticsPage(Path.Combine(outputPath, Output.Files.TestStatisticsFile));
            tests.GenerateTestListPage(Path.Combine(outputPath, Output.Files.TestListFile));
            tests.GenerateReportMainPage(outputPath, stats);
        }
    }
}
