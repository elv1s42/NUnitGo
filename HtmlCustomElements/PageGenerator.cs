using System.IO;
using System.Web.UI;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements
{
	public class PageGenerator
	{
		public static void GenerateReport(TestResults testResults, string pathToSave)
		{
			var report = new HtmlPage("NUnitGo Report");

			//TODO: report generation here

			report.AddInsideTag("style", 
@"
a:hover {
	text-decoration	: none;
}
a.tooltip span {
	display			: none; 
	padding			: 2px 3px; 
	margin-left		: 8px; 
	width			: 130px;
}
a.tooltip:hover span {
	display			: inline; 
	position		: absolute;
	border			: 1px solid #cccccc;
}");
			
			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write("Simple");
				
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
				//writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write("Tooltip");

				writer.RenderBeginTag(HtmlTextWriterTag.Span);

				writer.Write("Tooltip text");

				writer.RenderEndTag(); //SPAN
				writer.RenderEndTag(); //A
				writer.RenderEndTag(); //DIV

			}

			report.AddToBody(strWr.ToString());

			report.SavePage(pathToSave);
		}
	}
}