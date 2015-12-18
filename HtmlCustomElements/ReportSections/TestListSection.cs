using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.HtmlCustomElements;
using Utils;

namespace HtmlCustomElements.ReportSections
{
    public class TestListSection
    {
        public string HtmlCode;
        public string ModalsHtml;

        public TestListSection(List<NunitGoTest> tests)
        {
            var tree = new Tree(tests);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.Write(tree.HtmlCode);
            }
            HtmlCode = stringWriter.ToString();
            ModalsHtml = tree.HtmlCodeModalWindows;
        }
    }
}
