using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.NunitTestHtml
{
	internal class NunitTestHtml : HtmlBaseElement
	{
		public string BackgroundColor;
		public string HtmlCode;
		public static string StyleString
		{
			get { return GetStyle(); }
		}
		
		private new const string Id = "testcase-element";
		
		public static string GetStyle()
		{
            var testCssSet = new CssSet("testcase-element");
            testCssSet.AddElement(new CssElement("#" + Id)
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "16px")
				}
            });
			testCssSet.AddElement(new CssElement("#" + Id + " b")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "18px")
				}
			}); 
			testCssSet.AddElement(new CssElement(".test-window")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("box-sizing", "border-box"),
					new StyleAttribute(HtmlTextWriterStyle.Overflow, "auto"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White),
					new StyleAttribute(HtmlTextWriterStyle.Top, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px"),
					new StyleAttribute("border", "10px solid " + Colors.ModalBorderColor),
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute")
				}
			});
			return testCssSet.ToString();
		}

		public static string GenerateTxtView(string txt)
		{
			var sWr = new StringWriter();
			using (var wr = new HtmlTextWriter(sWr))
			{
				wr.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-line");
				wr.RenderBeginTag(HtmlTextWriterTag.Div);
				wr.Write(txt);
				wr.RenderEndTag();//DIV
			}
			return sWr.ToString();
		}

		public NunitTestHtml(NunitGoTest nunitGoTest, string testOutput = "")
		{
			Style = GetStyle();
			BackgroundColor = nunitGoTest.GetBackgroundColor();

			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0%");
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "test-window");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

				writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

				writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(new CloseButton("Back", "./../../" + Output.Files.TestListFile).ButtonHtml);
				writer.RenderEndTag(); //DIV
                
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);

                writer.AddStyleAttribute("align", "center");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddTestResult(nunitGoTest);
                writer.RenderEndTag();//TD
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.AddTestHistory(nunitGoTest);
                writer.RenderEndTag();//TD
                writer.RenderEndTag();//TR

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddEnvironment();
                writer.RenderEndTag();//TD
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddScreenshots(nunitGoTest);
                writer.RenderEndTag();//TD
                writer.RenderEndTag();//TR

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Td); 
                writer.AddFailure(nunitGoTest);
                writer.RenderEndTag();//TD
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.AddOutput(nunitGoTest, testOutput);
                writer.RenderEndTag();//TD
                writer.RenderEndTag();//TR

                writer.RenderEndTag();//TABLE

				writer.RenderEndTag(); //DIV
				writer.RenderEndTag(); //DIV

			}

			HtmlCode = strWr.ToString();
		}
	}
}
