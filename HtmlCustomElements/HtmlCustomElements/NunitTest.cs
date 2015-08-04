using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class NunitTest : HtmlBaseElement
    {
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

        public NunitTest(TestCase testCase)
        {
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
                writer.Write(testCase.StartDateTime.ToString("dd.MM.yy hh:mm:ss.fff") + " - " + 
                    testCase.EndDateTime.ToString("dd.MM.yy hh:mm:ss.fff"));
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Screenshots: ");
                writer.Write(testCase.Screenshots.Count);
                writer.RenderEndTag(); //P

                writer.RenderEndTag(); //DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
