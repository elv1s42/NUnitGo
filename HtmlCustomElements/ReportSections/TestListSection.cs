using System.IO;
using System.Web.UI;
using HtmlCustomElements.HtmlCustomElements;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements.ReportSections
{
    public class TestListSection
    {
        public string HtmlCode;
        public string ModalsHtml;

        public TestListSection(TestResults testResults, bool hierarchical = true)
        {
            var tree = new Tree(testResults, hierarchical);
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
