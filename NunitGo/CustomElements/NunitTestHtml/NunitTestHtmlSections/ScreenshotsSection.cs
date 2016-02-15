using System.Linq;
using System.Web.UI;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class ScreenshotsSection
    {
        public static void AddScreenshots(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            var screens = nunitGoTest.Screenshots.OrderBy(x => x.Date);
            foreach (var screenshot in screens)
            {
                writer.Write("Screenshot (Date: " + screenshot.Date.ToString("dd.MM.yy HH:mm:ss.fff") + "):");
                writer.AddAttribute(HtmlTextWriterAttribute.Href, @"./../../Screenshots/" + screenshot.Name);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                writer.AddAttribute(HtmlTextWriterAttribute.Src, @"./../../Screenshots/" + screenshot.Name);
                writer.AddAttribute(HtmlTextWriterAttribute.Alt, screenshot.Name);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();//IMG
                writer.RenderEndTag();//A
            }
            writer.RenderEndTag();//DIV
        }
    }
}
