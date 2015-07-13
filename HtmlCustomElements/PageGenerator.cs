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
			/*mainCssSet.AddElement(new CssElement("div.tooltip span")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute{Style = HtmlTextWriterStyle.Display, Value = "none"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Padding, Value = "2px 3px"},
					new StyleAttribute{Style = HtmlTextWriterStyle.MarginLeft, Value = "8px"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Width, Value = "130px"} 
				}
			});
			mainCssSet.AddElement(new CssElement("div.tooltip:hover span")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute{Style = HtmlTextWriterStyle.Display, Value = "inline"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Padding, Value = "absolute"},
                    new StyleAttribute{Style = HtmlTextWriterStyle.BackgroundColor, Value = "#BFBFBF"}
				}
			});*/

            mainCssSet.AddElement(new CssElement(".tooltip")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%") 
				}
            });
            mainCssSet.AddElement(new CssElement(".tooltip::after")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "inline"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "absolute"),
                    new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#BFBFBF")
				}
            });
            mainCssSet.AddElement(new CssElement(".tooltip:hover::after")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "inline"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "absolute"),
                    new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#BFBFBF")
				}
            });

			report.AddInsideTag("style", mainCssSet.ToString());

		    var statisticBar = new HorizontalBar("main-bar", "Main bar");
		    var list = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("test1", "tooltip1", "red", 10),
                new HorizontalBarElement("test2", "tooltip2", "green", 3),
                new HorizontalBarElement("test3", "tooltip3", "yellow", 6),
		    };
		    var bar = statisticBar.GetBar(list);
            report.AddToBody(bar);
            report.AddInsideTag("style", statisticBar.Style);

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