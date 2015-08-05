using System.Collections.Generic;
using HtmlCustomElements.HtmlCustomElements;
using HtmlCustomElements.ReportSections;
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
            report.AddInsideTag("style", MainInformation.StyleString);
            report.AddInsideTag("style", Bullet.StyleString);
            report.AddInsideTag("style", ModalBackground.StyleString);
            report.AddInsideTag("style", ModalWindow.StyleString);
            report.AddInsideTag("style", HrefButton.StyleString);
            report.AddInsideTag("style", Tree.StyleString);
            report.AddInsideTag("style", NunitTest.StyleString);
            report.AddInsideTag("style", JsOpenButton.StyleString);
            
		    var mainTitle = new ReportTitle();
            report.AddToBody(mainTitle.HtmlCode);

            var mainInformation = new MainInformation(testResults);
            report.AddToBody(mainInformation.HtmlCode);

		    var reportMenuTitle = new ReportTitle("Report menu", "report-main-menu");
            report.AddToBody(reportMenuTitle.HtmlCode);

            var statisticsSection = new StatisticsSection(testResults);
		    var testListSection = new TestListSection(testResults);

            var accElements = new List<AccordionElement>
		    {
                new AccordionElement(statisticsSection.HtmlCode, "Main statistics", "tab1"),
                new AccordionElement(testListSection.HtmlCode, "Test list", "tab2"),
                new AccordionElement("timeline goes here", "Timeline", "tab3"),
                new AccordionElement("top defects list goes here", "Top Defects", "tab4")
		    };
		    var accordion = new Accordion("main-accordion", "Main Accordion", accElements);
            report.AddInsideTag("style", accordion.GetStyleString());
            report.AddToBody(accordion.AccordionHtml);
            report.AddToBody(testListSection.ModalsHtml);

		    var footer = new ReportFooter();
            report.AddInsideTag("footer", footer.HtmlCode);

			report.SavePage(pathToSave);
		}
	}
}