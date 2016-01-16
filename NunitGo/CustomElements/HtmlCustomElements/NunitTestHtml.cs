﻿using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements.HtmlCustomElements
{
	public class NunitTestHtml : HtmlBaseElement
    {
        public string ModalWindowsHtml;
		
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
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
					new StyleAttribute(HtmlTextWriterStyle.Top, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px"),
					new StyleAttribute("border", "10px solid " + Colors.ModalBorderColor),
					new StyleAttribute(HtmlTextWriterStyle.Position, "fixed")
				}
            });
			return testCssSet.ToString();
		}

        private static string GenerateTxtView(string txt)
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

        private static string GenerateHtmlView(string fileLocation, string height = "25em")
        {
            var sWr = new StringWriter();
            using (var wr = new HtmlTextWriter(sWr))
            {
                wr.AddStyleAttribute(HtmlTextWriterStyle.Height, height);
                wr.RenderBeginTag(HtmlTextWriterTag.Div);
                wr.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                wr.AddStyleAttribute("border", "0");
                wr.AddAttribute(HtmlTextWriterAttribute.Src, fileLocation);
                wr.RenderBeginTag(HtmlTextWriterTag.Iframe);
                wr.RenderEndTag();//IFRAME
                wr.RenderEndTag();//DIV
            }
            return sWr.ToString();
        }

        public NunitTestHtml(NunitGoTest nunitGoTest)
        {
            ModalWindowsHtml = "";

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
                writer.Write(new CloseButton("Back", "./../../" + Output.Outputs.FullReport).ButtonHtml);
                writer.RenderEndTag(); //DIV
                
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test name: ");
                writer.Write(nunitGoTest.Name);
                writer.RenderEndTag(); //P

                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, nunitGoTest.GetBackgroundColor());
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.RenderBeginTag(HtmlTextWriterTag.B);
                writer.Write("Test result: ");
                writer.RenderEndTag(); //B
                writer.Write(nunitGoTest.Result);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test duration: ");
                writer.Write(nunitGoTest.TestDuration);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Time period: ");
                var start = nunitGoTest.DateTimeStart.ToString("dd.MM.yy HH:mm:ss.fff");
                var end = nunitGoTest.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss.fff");
                writer.Write(start + " - " + end);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Screenshots: ");
                writer.Write(nunitGoTest.Screenshots.Count);
                writer.RenderEndTag(); //P

                foreach (var screenshot in nunitGoTest.Screenshots)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, @"./../../Screenshots/" + screenshot.Name);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, @"./../../Screenshots/" + screenshot.Name);
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, screenshot.Name);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();//IMG
                    writer.RenderEndTag();//A
                }

                if (nunitGoTest.HasOutput)
                {
                    var openButton = new OpenButton("View full log", nunitGoTest.LogHref, Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    //writer.Write(GenerateHtmlView(nunitGoTest.AttachmentsPath + Structs.Outputs.Out));
                }
                
                if (!nunitGoTest.IsSuccess())
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Stack trace: ");
                    writer.Write(GenerateTxtView(nunitGoTest.TestStackTrace));
                    writer.RenderEndTag(); //P
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Message: ");
                    writer.Write(GenerateTxtView(nunitGoTest.TestMessage));
                    writer.RenderEndTag(); //P
                }

                writer.RenderEndTag(); //DIV
                writer.RenderEndTag(); //DIV

            }

            HtmlCode = strWr.ToString();
        }
	}
}