using System.Web.UI;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class TestHistorySection
    {
        public static void AddTestHistory(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Output.GetChartId(nunitGoTest.Guid, nunitGoTest.DateTimeFinish));
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();//DIV
            writer.RenderEndTag();//DIV
        }
    }
}
