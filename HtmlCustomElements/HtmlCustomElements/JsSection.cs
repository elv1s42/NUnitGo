using System.IO;
using System.Web.UI;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class JsSection : HtmlBaseElement
    {
        public string Html;

        public JsSection()
        {
            Html = GetHtml();
        }

        private static string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Type, @"text/javascript");
                writer.RenderBeginTag(HtmlTextWriterTag.Script);

                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
