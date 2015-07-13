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
            mainCssSet.AddElement(new CssElement(".tooltip")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("border-bottom", "1px dotted #0077AA"), 
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%") 
				}
            });
            mainCssSet.AddElement(new CssElement(".tooltip::after")
            {
                StyleFields = new List<StyleAttribute>
				{                                 
                    new StyleAttribute("background", "#BFBFBF"),
					new StyleAttribute("border-radius", "8px 8px 8px 0px"),
					new StyleAttribute("box-shadow", "1px 1px 10px rgba(0, 0, 0, 0.5)"),
					new StyleAttribute(HtmlTextWriterStyle.Color, "#FFF"),
					new StyleAttribute("content", "attr(data-tooltip)"),
					new StyleAttribute(HtmlTextWriterStyle.MarginTop, "-24px"),
					new StyleAttribute("opacity", "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "3px 7px"),
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute"),
					new StyleAttribute(HtmlTextWriterStyle.Visibility, "hidden"),
					new StyleAttribute("transition", "all 0.4s ease-in-out")
				}
            });
            mainCssSet.AddElement(new CssElement(".tooltip:hover::after")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("opacity", "1"),
                    new StyleAttribute(HtmlTextWriterStyle.Visibility, "visible")
				}
            });
            report.AddInsideTag("style", mainCssSet.ToString());

            report.AddToBody(@"<h1>NUnitGo Test Run Report</h1>");

            var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("test1", "tooltip1", "red", 10.0),
                new HorizontalBarElement("test2", "tooltip2", "green", 3.0),
                new HorizontalBarElement("test3", "tooltip3", "yellow", 6.0),
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