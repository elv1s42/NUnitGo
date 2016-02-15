using System.Web.UI;
using NunitGo.Extensions;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class TestResultSection
    {
        public static HtmlTextWriter AddTestResult(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddTag(HtmlTextWriterTag.B, "Test full name: ");
            writer.Write(nunitGoTest.FullName);
            writer.RenderEndTag(); //P

            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddTag(HtmlTextWriterTag.B, "Test name: ");
            writer.Write(nunitGoTest.Name);
            writer.RenderEndTag(); //P

            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddTag(HtmlTextWriterTag.B, "Test duration: ");
            writer.Write(nunitGoTest.TestDuration);
            writer.RenderEndTag(); //P

            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.AddTag(HtmlTextWriterTag.B, "Time period: ");
            var start = nunitGoTest.DateTimeStart.ToString("dd.MM.yy HH:mm:ss.fff");
            var end = nunitGoTest.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss.fff");
            writer.Write(start + " - " + end);
            writer.RenderEndTag(); //P

            writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, nunitGoTest.GetBackgroundColor());
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.RenderBeginTag(HtmlTextWriterTag.B);
            writer.Write("Test result: ");
            writer.RenderEndTag(); //B
            writer.Write(nunitGoTest.Result);
            writer.RenderEndTag(); //P

            writer.RenderEndTag(); //DIV
            return writer;
        }
    }
}
