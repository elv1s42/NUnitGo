using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.HtmlCustomElements.HtmlCustomElements;
using NunitGo.HtmlCustomElements.ReportSections;
using NunitGo.Utils;

namespace NunitGo.HtmlCustomElements
{
	public static class PageGenerator
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

        public static void GenerateTestPage(this NunitGoTest nunitGoTest, string fullPath)
        {
            var page = new HtmlPage("Test page");
            
            var htmlTest = new NunitTestHtml(nunitGoTest);
            page.AddToBody(htmlTest.HtmlCode);
            
            page.AddInsideTag("style", ReportTitle.StyleString);
            page.AddInsideTag("style", HtmlPage.PageStyle);
            page.AddInsideTag("style", NunitTestHtml.StyleString);
            page.AddInsideTag("style", ReportFooter.StyleString);
            page.AddInsideTag("style", ModalBackground.StyleString);
            page.AddInsideTag("style", ModalWindow.StyleString);
            page.AddInsideTag("style", HrefButtonBase.StyleString);
            page.AddInsideTag("style", OpenButton.StyleString);

            page.SavePage(fullPath);
        }

        public static void GenerateReport(List<NunitGoTest> tests, 
            string pathToSave, string pageName = "index")
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
			report.AddInsideTag("style", HrefButtonBase.StyleString);
			report.AddInsideTag("style", Tree.StyleString);
			report.AddInsideTag("style", NunitTestHtml.StyleString);
			report.AddInsideTag("style", OpenButton.StyleString);
            
            var openModalWindowScript = new OpenModalWindowScript();
            report.AddScripts(openModalWindowScript.Script);

			var mainTitle = new ReportTitle();
			report.AddToBody(mainTitle.HtmlCode);

            var mainStats = new MainStatistics(tests);

			var mainInformation = new MainInformation(tests, mainStats);
			report.AddToBody(mainInformation.HtmlCode);

			var reportMenuTitle = new ReportTitle("Report menu", "report-main-menu");
			report.AddToBody(reportMenuTitle.HtmlCode);

            var statisticsSection = new StatisticsSection(mainStats);
            var testListSection = new TestListSection(tests);
            var timeline = new Timeline(tests);

			var accElements = new List<AccordionElement>
			{
				new AccordionElement(statisticsSection.HtmlCode, "Main statistics", "tab1"),
				new AccordionElement(testListSection.HtmlCode, "Test list", "tab2"),
				new AccordionElement(timeline.HtmlCode, "Timeline", "tab3")
			};
			var accordion = new Accordion("main-accordion", "Main Accordion", accElements);
			report.AddInsideTag("style", accordion.GetStyleString());
			report.AddToBody(accordion.AccordionHtml);
			report.AddToBody(testListSection.ModalsHtml);

			var footer = new ReportFooter();
			report.AddInsideTag("footer", footer.HtmlCode);

			report.SavePage(pathToSave, pageName);
		}
	}
}