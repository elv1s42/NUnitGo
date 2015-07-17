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
            report.AddInsideTag("style", MainInformation.StyleString);
            report.AddInsideTag("style", Bullet.StyleString);
            report.AddInsideTag("style", ModalBackground.StyleString);
            report.AddInsideTag("style", ModalWindow.StyleString);
            report.AddInsideTag("style", HrefButton.StyleString);

		    var mainInformation = new MainInformation(testResults);

            var modalTest = new ModalWindow("modal-test", "Test!");
            report.AddToBody(modalTest.ModalWindowHtml);
            var modalBackground = new ModalBackground();
            report.AddToBody(modalBackground.ModalBackgroundHtml);

            report.AddToBody(new ReportTitle().HtmlCode);
            report.AddToBody(mainInformation.HtmlCode);
            report.AddToBody(new ReportTitle("Main menu", "report-main-menu").HtmlCode);
            
            var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("test1", "tooltip1", "red", 12.0),
                new HorizontalBarElement("test2", "tooltip2", "green", 3.0),
                new HorizontalBarElement("test3", "tooltip3", "orange", 6.0),
                new HorizontalBarElement("test4", "tooltip4", "yellow", 6.0),
                new HorizontalBarElement("test5", "tooltip5", "blue", 0.1),
                new HorizontalBarElement("test6", "tooltip6", "purple", 0.2)
		    };
		    var bar = new HorizontalBar("main-bar", "Main bar", list);
            var hrefButton = new HrefButton("test-href-button", "Test it!",
                "#tab1"); 
            var openButton = new JsOpenButton("Open modal", "modal-test");
            var accElements = new List<AccordionElement>
		    {
                new AccordionElement(bar.BarHtml, "element1", "tab1"),
                new AccordionElement(hrefButton.HrefButtonHtml, "element2", "tab2"),
                new AccordionElement(openButton.ButtonHtml, "element3", "tab3"),
                new AccordionElement("test4", "element4", "tab4"),
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