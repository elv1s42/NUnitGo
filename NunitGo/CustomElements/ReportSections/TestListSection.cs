using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    internal class TestListSection
    {
        public string HtmlCode;

        public TestListSection(List<NunitGoTest> tests, string height = "90%")
        {
            var tree = new Tree(tests);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(new CloseButton("Back", Output.Outputs.FullReport).ButtonHtml);
                writer.RenderEndTag(); //DIV

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "5%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(tree.HtmlCode);
                writer.RenderEndTag(); //DIV
                
                writer.RenderEndTag(); //DIV

            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
