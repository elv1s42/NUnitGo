using System.Linq;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems;

namespace NUnitGoCore.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class TestEventsSection
    {
        public static HtmlTextWriter AddTestEvents(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            var events = nunitGoTest.Events.OrderBy(x => x.Started);
            foreach (var testEvent in events)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test event: ");
                writer.Write(testEvent.Name);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Started: " + testEvent.Started.ToString("dd.MM.yy HH:mm:ss.fff"));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Finished: " + testEvent.Finished.ToString("dd.MM.yy HH:mm:ss.fff"));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Duration: " + testEvent.DurationString);
                writer.RenderEndTag();

            }
            if (!events.Any())
                writer.Write("There are no test events in this test");
            writer.RenderEndTag();//DIV
            return writer;
        }
    }
}
