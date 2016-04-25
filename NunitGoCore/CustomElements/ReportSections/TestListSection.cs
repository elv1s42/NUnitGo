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
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Tag(HtmlTextWriterTag.Div,
                        () => writer
                            .Float("right")
                            .Div(() => writer
                                .DangerButton("Back", Output.Files.FullReportFile)
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
