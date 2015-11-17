using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.HtmlCustomElements;
using HtmlCustomElements.ReportSections;
using NunitResultAnalyzer;
using NunitResultAnalyzer.TestResultClasses;
using Utils.XmlTypes;

namespace HtmlCustomElements
{
	public class PageGenerator
	{
        public static void GenerateOutputPage(string fullPath, string outputText)
	    {
            var page = new HtmlPage("Output page");
            var sWr = new StringWriter();
            using (var wr = new HtmlTextWriter(sWr))
            {
                wr.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-line");
                wr.RenderBeginTag(HtmlTextWriterTag.Div);
                wr.Write(outputText);
                wr.RenderEndTag();//DIV
            }
            page.AddToBody(sWr.ToString());
            page.SavePage(fullPath);
	    }

        public static void GenerateReport(TestResults fullTestResults, TestResults currentTestResults, List<ExtraTestInfo> allTests, 
            string pathToSave, string pageName = "index")
        {
            var fullTestsResults = ResultsAnalyzer.GetFullSuite(fullTestResults, allTests);
            var currentTestsResults = ResultsAnalyzer.GetFullSuite(currentTestResults, allTests);
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

            var jsSection = new JsSection();
            report.AddToBody(jsSection.Html);

            var openModalWindowScript = new OpenModalWindowScript();
            report.AddScripts(openModalWindowScript.Script);

			var mainTitle = new ReportTitle();
			report.AddToBody(mainTitle.HtmlCode);

			var mainInformation = new MainInformation(fullTestsResults, currentTestResults);
			report.AddToBody(mainInformation.HtmlCode);

			var reportMenuTitle = new ReportTitle("Report menu", "report-main-menu");
			report.AddToBody(reportMenuTitle.HtmlCode);

            var statisticsSection = new StatisticsSection(fullTestsResults);
            var testListHierarchicalSection = new TestListSection(fullTestsResults);
            var testListNonHierarchicalSection = new TestListSection(currentTestsResults, false);
            var timeline = new Timeline(currentTestResults);

			var accElements = new List<AccordionElement>
			{
				new AccordionElement(statisticsSection.HtmlCode, "Main statistics", "tab1"),
				new AccordionElement(testListHierarchicalSection.HtmlCode, "Hierarchical test list", "tab2"),
				new AccordionElement(testListNonHierarchicalSection.HtmlCode, "Non-hierarchical test list", "tab3"),
				new AccordionElement(timeline.HtmlCode, "Timeline", "tab4"),
				new AccordionElement("top defects list goes here", "Top Defects", "tab5")
			};
			var accordion = new Accordion("main-accordion", "Main Accordion", accElements);
			report.AddInsideTag("style", accordion.GetStyleString());
			report.AddToBody(accordion.AccordionHtml);
			report.AddToBody(testListHierarchicalSection.ModalsHtml);

			var footer = new ReportFooter();
			report.AddInsideTag("footer", footer.HtmlCode);

			report.SavePage(pathToSave, pageName);
		}
	}
}