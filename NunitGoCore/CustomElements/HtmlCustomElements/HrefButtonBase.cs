using System.IO;
using System.Web.UI;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class HrefButtonBase : HtmlBaseElement
    {
        private readonly string _buttonText;
        private readonly string _href;
        public string HrefButtonHtml;

        public HrefButtonBase(string id, string buttonText, string href)
        {
            Id = id;
            _buttonText = buttonText;
            _href = href;
            HrefButtonHtml = GetHtml();
        }

        public HrefButtonBase(string buttonText, string href)
        {
            _buttonText = buttonText;
            _href = href;
            HrefButtonHtml = GetHtml();
        }

        private string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _href);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "href-button");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(_buttonText);
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }

    }
}
