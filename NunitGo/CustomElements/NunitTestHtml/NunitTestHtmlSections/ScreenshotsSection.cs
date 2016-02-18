using System.Linq;
using System.Web.UI;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class ScreenshotsSection
    {
        public static HtmlTextWriter AddScreenshots(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");
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
            if(!screens.Any())
                writer.Write("There are no screenshots in this test");
            writer.RenderEndTag();//DIV
            return writer;
        }
    }
}
