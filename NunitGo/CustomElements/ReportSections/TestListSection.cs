using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Extensions;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    internal class TestListSection
    {
        public string HtmlCode;

        public TestListSection(List<NunitGoTest> tests)
        {
            var tree = new Tree(tests);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.Css(HtmlTextWriterStyle.Height, "45px")
                    .Css(HtmlTextWriterStyle.BackgroundColor, Colors.BodyBackground)
                    .Tag(HtmlTextWriterTag.Div,
                    () => writer.Css("float", "right")
                        .Css("padding", "10px")
                        .Tag(HtmlTextWriterTag.Div,
                            () => writer.Write(new CloseButton("Back", Output.Files.FullReportFile).ButtonHtml)
                        )
                    );

                writer.CssShadow("0 0 20px 0 " + Colors.TestBorderColor)
                    .Css(HtmlTextWriterStyle.BackgroundColor, Colors.White)
                    .Tag(HtmlTextWriterTag.Div, () => writer.Write(tree.HtmlCode));

            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
