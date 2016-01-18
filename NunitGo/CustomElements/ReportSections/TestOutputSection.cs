using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    public class TestOutputSection
    {
        public string HtmlCode;

        public TestOutputSection(string testOutput, string backHref, string height = "90%")
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "5%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "pre-line");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(testOutput);
                writer.RenderEndTag();//DIV
                writer.RenderEndTag(); //DIV

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(new CloseButton("Back", backHref).ButtonHtml);
                writer.RenderEndTag(); //DIV

                writer.RenderEndTag(); //DIV

            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
