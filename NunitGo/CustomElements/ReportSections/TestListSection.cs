using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    public class TestListSection
    {
        public string HtmlCode;

        public TestListSection(List<NunitGoTest> tests)
        {
            var tree = new Tree(tests);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "90%");
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.Write(tree.HtmlCode);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(new CloseButton("Back", Output.Outputs.FullReport).ButtonHtml);
                writer.RenderEndTag(); //DIV

                writer.RenderEndTag(); //DIV

            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
