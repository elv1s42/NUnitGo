using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using HtmlCustomElements.HtmlCustomElements;
using NunitResultAnalyzer.XmlClasses;
using Environment = System.Environment;

namespace HtmlCustomElements
{
	public class PageGenerator
	{
		public static void GenerateReport(TestResults testResults, string pathToSave)
		{
			var report = new HtmlPage("NUnitGo Report");

			var mainCssSet = new CssSet("main-style");
			mainCssSet.AddElement(new CssElement("div:hover")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
			});

            report.AddInsideTag("style", HorizontalBar.StyleString);
            report.AddInsideTag("style", Tooltip.GetStyle());

            report.AddToBody(@"<h1>NUnitGo Test Run Report</h1>");

            var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("test1", "tooltip1", "red", 10.0),
                new HorizontalBarElement("test2", "tooltip2", "green", 3.0),
                new HorizontalBarElement("test3", "tooltip3", "orange", 6.0),
                new HorizontalBarElement("test4", "tooltip4", "yellow", 6.0),
                new HorizontalBarElement("test5", "tooltip5", "blue", 1.0)
		    };
		    var hb = new HorizontalBar("main-bar", "Main bar", list);
            report.AddToBody(hb.Bar);
            report.AddInsideTag("style", hb.Style);

			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write("Simple ");
				
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write("Tooltip");

				writer.RenderBeginTag(HtmlTextWriterTag.Span);

                writer.Write("Tooltip text" + Environment.NewLine + "");

				writer.RenderEndTag(); //SPAN
				writer.RenderEndTag(); //A
				writer.RenderEndTag(); //DIV

			}

			report.AddToBody(strWr.ToString());

			report.SavePage(pathToSave);
		}
	}
}