using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.XmlClasses;
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

        private static string GetBackgroundColor(TestCase testCase)
        {
            string res;
            switch (testCase.Result)
            {
                case "Failure":
                    res = Colors.TestFailed;
                    break;
                case "Success":
                    res = Colors.TestPassed;
                    break;
                case "Error":
                    res = Colors.TestBroken;
                    break;
                case "Ignored":
                    res = Colors.TestIgnored;
                    break;
                default:
                    res = Colors.TestUnknown;
                    break;
            }
            return res;
        }

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
                wr.RenderEndTag();
            }
            return sWr.ToString();
        }

        public NunitTest(TestCase testCase)
        {
            var hasOutput = !testCase.Out.Equals("");
            var hasError = !testCase.Error.Equals("");
            var hasLog = !testCase.Log.Equals("");
            var hasTrace = !testCase.Trace.Equals("");

            ModalWindowsHtml = "";

            Style = GetStyle();
            BackgroundColor = GetBackgroundColor(testCase);

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test name: ");
                writer.Write(testCase.Name.Split('.').Last());
                writer.RenderEndTag(); //P

                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, GetBackgroundColor(testCase));
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
                writer.Write(testCase.StartDateTime.ToString("dd.MM.yy HH:mm:ss.fff") + " - " + 
                    testCase.EndDateTime.ToString("dd.MM.yy HH:mm:ss.fff"));
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Screenshots: ");
                writer.Write(testCase.Screenshots.Count);
                writer.RenderEndTag(); //P

                if (hasError)
                {
                    var modalErrorId = "modal-error-" + testCase.Guid;
                    var modalError = new ModalWindow(modalErrorId, GenerateTxtView(testCase.Error), "1004", 80, "1003");
                    var openButton = new JsOpenButton("Veiw error", modalErrorId, modalError.BackgroundId,
                        Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    ModalWindowsHtml += modalError.ModalWindowHtml + Environment.NewLine;
                }
                if (hasOutput)
                {
                    var modalOutId = "modal-out-" + testCase.Guid;
                    var modalOut = new ModalWindow(modalOutId, GenerateTxtView(testCase.Out), "1004", 80, "1003");
                    var openButton = new JsOpenButton("Veiw output", modalOutId, modalOut.BackgroundId,
                        Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    ModalWindowsHtml += modalOut.ModalWindowHtml + Environment.NewLine;
                }
                if (hasTrace)
                {
                    var modalTraceId = "modal-trace-" + testCase.Guid;
                    var modalTrace = new ModalWindow(modalTraceId, GenerateTxtView(testCase.Trace), "1004", 80, "1003");
                    var openButton = new JsOpenButton("Veiw trace", modalTraceId, modalTrace.BackgroundId,
                        Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    ModalWindowsHtml += modalTrace.ModalWindowHtml + Environment.NewLine;
                }
                if (hasLog)
                {
                    var modalLogId = "modal-log-" + testCase.Guid;
                    var modalLog = new ModalWindow(modalLogId, GenerateTxtView(testCase.Log), "1004", 80, "1003");
                    var openButton = new JsOpenButton("Veiw log", modalLogId, modalLog.BackgroundId, 
                        Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    ModalWindowsHtml += modalLog.ModalWindowHtml + Environment.NewLine;
                }

                foreach (var screenshot in testCase.Screenshots)
                {
                    var sWr = new StringWriter();
                    using (var wr = new HtmlTextWriter(sWr))
                    {
                        wr.AddAttribute(HtmlTextWriterAttribute.Src, @"./Screenshots/" + screenshot.Key);
                        wr.AddAttribute(HtmlTextWriterAttribute.Alt, screenshot.Key);
                        wr.RenderBeginTag(HtmlTextWriterTag.Img);
                        wr.RenderEndTag(); //IMG
                    }
                    var screenCode = sWr.ToString();
                    var modalScreenshotId = "modal-screenshot-" + screenshot.Key;
                    var modalScreenshot = new ModalWindow(modalScreenshotId, screenCode, "1004", 100, "1003");
                    var openButton = new JsOpenButton("Veiw screenshot " + screenshot.Value.ToString("dd.MM.yy HH:mm:ss"), 
                        modalScreenshotId, modalScreenshot.BackgroundId,
                        Colors.OpenLogsButtonBackground);
                    writer.Write(openButton.ButtonHtml);
                    ModalWindowsHtml = ModalWindowsHtml + modalScreenshot.ModalWindowHtml + Environment.NewLine;
                }

                writer.RenderEndTag(); //DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
