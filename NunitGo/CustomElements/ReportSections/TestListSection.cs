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
                writer.Write(tree.HtmlCode);
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
