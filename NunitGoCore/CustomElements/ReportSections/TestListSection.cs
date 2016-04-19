using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.ReportSections
{
    internal class TestListSection
    {
        public string HtmlCode;

        public TestListSection(List<NunitGoTest> tests)
        {
            var treeCode = Tree.GetTreeCode(tests);
            var backButton = new CloseButton("Back", Output.Files.FullReportFile);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Tag(HtmlTextWriterTag.Div,
                        () => writer
                            .Css("float", "right")
                            .Tag(HtmlTextWriterTag.Div,
                                () => writer.Write(backButton.ButtonHtml)
                            )
                    )
                    .Tag(HtmlTextWriterTag.Div, () => writer
                        .Write(treeCode)
                    );
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
