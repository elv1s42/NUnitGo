using System.Web.UI;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class TestHistorySection
    {
        public static HtmlTextWriter AddTestHistory(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Output.GetHistoryChartId(nunitGoTest.Guid, nunitGoTest.DateTimeFinish));
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();//DIV
            writer.RenderEndTag();//DIV
            return writer;
        }
    }
}
