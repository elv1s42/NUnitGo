using System.Collections.Generic;
using System.IO;
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
                writer.Write("Test name: " + testCase.Name);
                writer.RenderEndTag(); //DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
