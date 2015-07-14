using System.Collections.Generic;
using HtmlCustomElements.HtmlCustomElements;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements
{
	public class PageGenerator
	{
		public static void GenerateReport(TestResults testResults, string pathToSave)
		{
			var report = new HtmlPage("NUnitGo Report");

            report.AddInsideTag("style", ReportTitle.StyleString);
            report.AddInsideTag("style", HtmlPage.PageStyle);
            report.AddInsideTag("style", Tooltip.StyleString);
            report.AddInsideTag("style", HorizontalBar.StyleString);
            report.AddInsideTag("style", ReportFooter.StyleString);

		    report.AddToBody(new ReportTitle().HtmlCode);

            var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("test1", "tooltip1", "red", 12.0),
                new HorizontalBarElement("test2", "tooltip2", "green", 3.0),
                new HorizontalBarElement("test3", "tooltip3", "orange", 6.0),
                new HorizontalBarElement("test4", "tooltip4", "yellow", 6.0),
                new HorizontalBarElement("test5", "tooltip5", "blue", 0.1),
                new HorizontalBarElement("test5", "tooltip5", "purple", 0.2)
		    };
            report.AddToBody(new HorizontalBar("main-bar", "Main bar", list).Bar);

            report.AddInsideTag("footer", new ReportFooter().HtmlCode);

			report.SavePage(pathToSave);
		}
	}
}