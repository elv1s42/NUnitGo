using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.CustomElements.ReportSections;
using NunitGo.Utils;

namespace NunitGo.CustomElements
{
	public static class PageGenerator
    {
        public static void GenerateOutputPage(string fullPath, string outputText)
        {
            var page = new HtmlPage("Output page", "./../../" + Output.Outputs.ReportStyle);
            var sWr = new StringWriter();
            using (var wr = new HtmlTextWriter(sWr))
            {
                wr.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-line");
                wr.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                wr.RenderBeginTag(HtmlTextWriterTag.Div);
                wr.Write(outputText);
                wr.RenderEndTag();//DIV
            }
            page.AddToBody(sWr.ToString());
            
            page.SavePage(fullPath);
        }

        public static void GenerateTestPage(this NunitGoTest nunitGoTest, string fullPath)
        {
            var page = new HtmlPage("Test page", "./../../" + Output.Outputs.ReportStyle);

            var htmlTest = new NunitTestHtml(nunitGoTest);
            page.AddToBody(htmlTest.HtmlCode);

            page.SavePage(fullPath);
        }

        public static void GenerateTestList(this List<NunitGoTest> tests, string fullPath)
        {
            var page = new HtmlPage("Test list page");

            var reportMenuTitle = new PageTitle("Test list", "main-test-list", "10%");
            page.AddToBody(reportMenuTitle.HtmlCode);

            var testListSection = new TestListSection(tests);
            page.AddToBody(testListSection.HtmlCode);

            page.SavePage(fullPath);
        }

        public static void GenerateReport(this List<NunitGoTest> tests, 
            string pathToSave)
        {
            var cssPage = new CssPage();
            cssPage.AddStyles(new List<string>
            {
                PageTitle.StyleString,
                HtmlPage.StyleString,
                Tooltip.StyleString,
                HorizontalBar.StyleString,
                ReportFooter.StyleString,
                MainInformation.StyleString,
                Bullet.StyleString,
                HrefButtonBase.StyleString,
                Tree.StyleString,
                NunitTestHtml.StyleString,
                Accordion.StyleString,
                ReportMenu.StyleString,
                OpenButton.StyleString
            });

            var report = new HtmlPage();
            
			var mainTitle = new PageTitle();
			report.AddToBody(mainTitle.HtmlCode);

            var mainStats = new MainStatistics(tests);

			var mainInformation = new MainInformation(tests, mainStats);
			report.AddToBody(mainInformation.HtmlCode);

			var reportMenuTitle = new PageTitle("Report menu", "report-main-menu");
			report.AddToBody(reportMenuTitle.HtmlCode);

            var statisticsSection = new StatisticsSection(mainStats);
            var testListSection = new TestListSection(tests);
            var timeline = new Timeline(tests);

            var accElements = new List<AccordionElement>
			{
				new AccordionElement(statisticsSection.HtmlCode, "Main statistics"),
				new AccordionElement(testListSection.HtmlCode, "Test list"),
				new AccordionElement(timeline.HtmlCode, "Timeline")
			};
            var accordion = new Accordion(accElements);
            report.AddToBody(accordion.AccordionHtml);

            var menuElements = new List<ReportMenuItem>
			{
				new ReportMenuItem(statisticsSection.HtmlCode, "Main statistics", ""),
				new ReportMenuItem(testListSection.HtmlCode, "Test list", Output.Outputs.TestList),
				new ReportMenuItem(timeline.HtmlCode, "Timeline", "")
			};
            var reportMenu = new ReportMenu(menuElements, "main-menu", "Main Menu");
            report.AddToBody(reportMenu.ReportMenuHtml);

			var footer = new ReportFooter();
            report.AddInsideTag("footer", footer.HtmlCode);

            var cssPageName = Output.Outputs.ReportStyle;
            cssPage.SavePage(Path.Combine(pathToSave, cssPageName));

            var reportPageName = Output.Outputs.FullReport;
			report.SavePage(Path.Combine(pathToSave, reportPageName));
		}
	}
}