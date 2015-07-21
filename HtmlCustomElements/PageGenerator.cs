using System.Collections.Generic;
using HtmlCustomElements.HtmlCustomElements;
using NunitResultAnalyzer;
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

		    var mainInformation = new MainInformation(testResults);

            var modalTest = new ModalWindow("modal-test", "Test!");
            report.AddToBody(modalTest.ModalWindowHtml);
            var modalBackground = new ModalBackground();
            report.AddToBody(modalBackground.ModalBackgroundHtml);

            report.AddToBody(new ReportTitle().HtmlCode);
            report.AddToBody(mainInformation.HtmlCode);
            report.AddToBody(new ReportTitle("Report menu", "report-main-menu").HtmlCode);

		    var mainStats = new MainStatistics(testResults.TestSuite);
            var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("Passed", "Passed (" + mainStats.TotalPassed + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestPassed, 
                    mainStats.TotalPassed/(double)mainStats.TotalAll),
                new HorizontalBarElement("Failed", "Failed (" + mainStats.TotalFailed + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestFailed, 
                    mainStats.TotalFailed/(double)mainStats.TotalAll),
                new HorizontalBarElement("Broken", "Broken (" + mainStats.TotalBroken + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestBroken, 
                    mainStats.TotalBroken/(double)mainStats.TotalAll),
                new HorizontalBarElement("Ignored", "Ignored (" + mainStats.TotalIgnored + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestIgnored, 
                    mainStats.TotalIgnored/(double)mainStats.TotalAll),
                new HorizontalBarElement("Unknown", "Unknown (" + mainStats.TotalUnknown + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestUnknown, 
                    mainStats.TotalUnknown/(double)mainStats.TotalAll)
		    };
		    var bar = new HorizontalBar("main-bar", "Main bar", list);
            var hrefButton = new HrefButton("test-href-button", "Test it!",
                "#tab1"); 
            var openButton = new JsOpenButton("Open modal", "modal-test");
		    var tree = new Tree(testResults);
            var accElements = new List<AccordionElement>
		    {
                new AccordionElement(bar.BarHtml, "Main statistics", "tab1"),
                new AccordionElement(hrefButton.HrefButtonHtml, "element2", "tab2"),
                new AccordionElement(openButton.ButtonHtml, "element3", "tab3"),
                new AccordionElement(tree.HtmlCode, "Test list", "tab4"),
                new AccordionElement("test5", "element5", "tab5"),
                new AccordionElement("test6", "element6", "tab6")
		    };
		    var accordion = new Accordion("main-accordion", "Main Accordion", accElements);
            report.AddInsideTag("style", accordion.GetStyleString());
		    report.AddToBody(accordion.AccordionHtml);

            report.AddInsideTag("footer", new ReportFooter().HtmlCode);

			report.SavePage(pathToSave);
		}
	}
}