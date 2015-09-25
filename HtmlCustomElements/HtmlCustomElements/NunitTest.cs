using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.XmlClasses;
using Utils;
using Environment = System.Environment;

namespace HtmlCustomElements.HtmlCustomElements
{
	public class NunitTest : HtmlBaseElement
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
			var treeCssSet = new CssSet("testcase-element");
			treeCssSet.AddElement(new CssElement("#" + Id)
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "16px")
				}
			});
			treeCssSet.AddElement(new CssElement("#" + Id + " b")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "18px")
				}
			});
			return treeCssSet.ToString();
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

        private static string GenerateHtmlView(string txt)
        {
            var sWr = new StringWriter();
            using (var wr = new HtmlTextWriter(sWr))
            {
                wr.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Height, "90%");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                wr.AddStyleAttribute("border", "0");
                wr.AddAttribute(HtmlTextWriterAttribute.Src, txt);
                wr.RenderBeginTag(HtmlTextWriterTag.Iframe);
                wr.RenderEndTag();//IFRAME
            }
            return sWr.ToString();
        }

        private static string GenerateTxtFileView(string txt)
        {
            var sWr = new StringWriter();
            using (var wr = new HtmlTextWriter(sWr))
            {
                wr.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                wr.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-line");
                wr.RenderBeginTag(HtmlTextWriterTag.Div);

                wr.AddAttribute("data", txt);
                wr.AddAttribute(HtmlTextWriterAttribute.Type, "text/html");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
                wr.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                wr.RenderBeginTag(HtmlTextWriterTag.Object);
                wr.Write("alt : ");

                wr.AddAttribute(HtmlTextWriterAttribute.Href, txt);
                wr.RenderBeginTag(HtmlTextWriterTag.A);
                wr.Write(txt);
                wr.RenderEndTag();//A

                wr.RenderEndTag();//OBJECT

                wr.RenderEndTag();//DIV
            }
            return sWr.ToString();
        }

		public NunitTest(TestCase testCase)
		{
			Log.Write("NunitTest constructor: testCase.Name = " + testCase.Name);
			var hasOutput = testCase.Out != null && !testCase.Out.Equals("");
			var hasError = testCase.Error != null && !testCase.Error.Equals("");
			var hasLog = testCase.Log != null && !testCase.Log.Equals("");
			var hasTrace = testCase.Trace != null && !testCase.Trace.Equals("");
			Log.Write("Test case output checked.");

            ModalWindowsHtml = "";

			Style = GetStyle();
			BackgroundColor = testCase.GetBackgroundColor();

			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

				writer.RenderBeginTag(HtmlTextWriterTag.P);
				writer.AddTag(HtmlTextWriterTag.B, "Test name: ");
				writer.Write(testCase.Name.Split('.').Last());
				writer.RenderEndTag(); //P

				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, testCase.GetBackgroundColor());
				writer.RenderBeginTag(HtmlTextWriterTag.P);
				writer.RenderBeginTag(HtmlTextWriterTag.B);
				writer.Write("Test result: ");
				writer.RenderEndTag(); //B
				writer.Write(testCase.Result);
				writer.RenderEndTag(); //P

				writer.RenderBeginTag(HtmlTextWriterTag.P);
				writer.AddTag(HtmlTextWriterTag.B, "Test duration: ");
				writer.Write(testCase.Time);
				writer.RenderEndTag(); //P
				
				writer.RenderBeginTag(HtmlTextWriterTag.P);
				writer.AddTag(HtmlTextWriterTag.B, "Time period: ");
				var start = testCase.StartDateTime.ToString("dd.MM.yy HH:mm:ss.fff");
				var end = testCase.EndDateTime.ToString("dd.MM.yy HH:mm:ss.fff");
				Log.Write(start + " - " + end);
				writer.Write(start + " - " + end);
				writer.RenderEndTag(); //P

				Log.Write("Screenshots count: " + testCase.Screenshots.Count);
				writer.RenderBeginTag(HtmlTextWriterTag.P);
				writer.AddTag(HtmlTextWriterTag.B, "Screenshots: ");
				writer.Write(testCase.Screenshots.Count);
				writer.RenderEndTag(); //P

				if (hasError)
				{
					var modalErrorId = "modal-error-" + testCase.Guid;
					var error = testCase.Error;
					var modalError = new ModalWindow(modalErrorId, GenerateHtmlView(error), "1004", 80, "1003");
                    var onClickString = "openModalWindow(\""
                        + error + "\",\""
                        + modalErrorId + "\",\""
                        + modalErrorId + "-inner" + "\",\""
                        + modalError.BackgroundId + "\")";
					var openButton = new JsOpenButton("Veiw error", modalErrorId, modalError.BackgroundId,
						Colors.OpenLogsButtonBackground, onClickString);

					writer.Write(openButton.ButtonHtml);
					ModalWindowsHtml += modalError.ModalWindowHtml + Environment.NewLine;
				}
				if (hasOutput)
				{
					var modalOutId = "modal-out-" + testCase.Guid;
					var output = testCase.Out;
                    var modalOut = new ModalWindow(modalOutId, GenerateHtmlView(output), "1004", 80, "1003");
                    var onClickString = "openModalWindow(\""
                        + output + "\",\""
                        + modalOutId + "\",\""
                        + modalOutId + "-inner" + "\",\"" 
                        + modalOut.BackgroundId + "\")";
					var openButton = new JsOpenButton("Veiw output", modalOutId, modalOut.BackgroundId,
						Colors.OpenLogsButtonBackground, onClickString);

                    writer.Write(openButton.ButtonHtml);
					ModalWindowsHtml += modalOut.ModalWindowHtml + Environment.NewLine;
				}
				if (hasTrace)
				{
					var modalTraceId = "modal-trace-" + testCase.Guid;
					var trace = testCase.Trace;
					var modalTrace = new ModalWindow(modalTraceId, GenerateHtmlView(trace), "1004", 80, "1003");
                    var onClickString = "openModalWindow(\""
                        + trace + "\",\""
                        + modalTraceId + "\",\""
                        + modalTraceId + "-inner" + "\",\""
                        + modalTrace.BackgroundId + "\")";
					var openButton = new JsOpenButton("Veiw trace", modalTraceId, modalTrace.BackgroundId,
						Colors.OpenLogsButtonBackground, onClickString);
					writer.Write(openButton.ButtonHtml);
					ModalWindowsHtml += modalTrace.ModalWindowHtml + Environment.NewLine;
				}
				if (hasLog)
				{
					var modalLogId = "modal-log-" + testCase.Guid;
					var log = testCase.Log;
					var modalLog = new ModalWindow(modalLogId, GenerateHtmlView(log), "1004", 80, "1003");
                    var onClickString = "openModalWindow(\""
                        + log + "\",\""
                        + modalLogId + "\",\""
                        + modalLogId + "-inner" + "\",\""
                        + modalLog.BackgroundId + "\")";
					var openButton = new JsOpenButton("Veiw log", modalLogId, modalLog.BackgroundId, 
						Colors.OpenLogsButtonBackground, onClickString);
					writer.Write(openButton.ButtonHtml);
					ModalWindowsHtml += modalLog.ModalWindowHtml + Environment.NewLine;
				}

				Log.Write("Adding screenshots...");
				foreach (var screenshot in testCase.Screenshots)
				{
					var sWr = new StringWriter();
					using (var wr = new HtmlTextWriter(sWr))
					{
						wr.AddAttribute(HtmlTextWriterAttribute.Src, @"./Screenshots/" + screenshot.Name);
						wr.AddAttribute(HtmlTextWriterAttribute.Alt, screenshot.Name);
						wr.RenderBeginTag(HtmlTextWriterTag.Img);
						wr.RenderEndTag(); //IMG
					}
					var screenCode = sWr.ToString();
					var modalScreenshotId = "modal-screenshot-" + screenshot.Name;
					var modalScreenshot = new ModalWindow(modalScreenshotId, screenCode, "1004", 100, "1003");
					var openButton = new JsOpenButton("Veiw screenshot " + screenshot.Date.ToString("dd.MM.yy HH:mm:ss"), 
						modalScreenshotId, modalScreenshot.BackgroundId,
						Colors.OpenLogsButtonBackground);
					writer.Write(openButton.ButtonHtml);
					ModalWindowsHtml = ModalWindowsHtml + modalScreenshot.ModalWindowHtml + Environment.NewLine;
				}
				Log.Write("Adding screenshots DONE.");
				
				if (!bool.Parse(testCase.Success))
				{
					writer.RenderBeginTag(HtmlTextWriterTag.P);
					writer.AddTag(HtmlTextWriterTag.B, "Failure stack trace: ");
					writer.Write(GenerateTxtView(testCase.Failure.StackTrace.Value));
					writer.RenderEndTag(); //P
					writer.RenderBeginTag(HtmlTextWriterTag.P);
					writer.AddTag(HtmlTextWriterTag.B, "Failure message: ");
					writer.Write(GenerateTxtView(testCase.Failure.Message.Value));
					writer.RenderEndTag(); //P
				}

				writer.RenderEndTag(); //DIV
			}

			HtmlCode = strWr.ToString();

			Log.Write("NunitTest constructor: testCase.Name = " + testCase.Name + " DONE.");
		}
	}
}
