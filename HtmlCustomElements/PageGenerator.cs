using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.XmlClasses;
using Environment = System.Environment;

namespace HtmlCustomElements
{
	public class PageGenerator
	{
		public static void GenerateReport(TestResults testResults, string pathToSave)
		{
			var report = new HtmlPage("NUnitGo Report");

			var cssSet = new CssSet();
			cssSet.AddElement(new CssElement("a:hover")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute{Style = HtmlTextWriterStyle.TextDecoration, Value = "none"} 
				}
			});
			cssSet.AddElement(new CssElement("a.tooltip span")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute{Style = HtmlTextWriterStyle.Display, Value = "none"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Padding, Value = "2px 3px"},
					new StyleAttribute{Style = HtmlTextWriterStyle.MarginLeft, Value = "8px"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Width, Value = "130px"} 
				}
			});
			cssSet.AddElement(new CssElement("a.tooltip:hover span")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute{Style = HtmlTextWriterStyle.Display, Value = "inline"},
					new StyleAttribute{Style = HtmlTextWriterStyle.Padding, Value = "absolute"},
                    new StyleAttribute{Style = HtmlTextWriterStyle.BackgroundColor, Value = "#BFBFBF"}
				}
			});

			report.AddInsideTag("style", cssSet.ToString());
			
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