using System.IO;
using System.Web.UI;

namespace NunitGo.CustomElements.HtmlCustomElements
{
    public class CloseButton : HrefButtonBase
    {
        private readonly string _buttonText = "Close";
        private readonly string _href;
        public string ButtonHtml;

        public CloseButton(string buttonText, string href) 
            : base(buttonText, href)
        {
            Id = "";
            _buttonText = buttonText.Equals("") ? _buttonText : buttonText;
            _href = href;
            ButtonHtml = GetHtml();
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
