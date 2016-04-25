using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.CSSElements;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class OpenButton : HrefButtonBase
    {
        private readonly string _buttonText;
        private readonly string _href;
        private readonly string _backgroundColor = "white";
        public string ButtonHtml;

        public OpenButton(string buttonText, string href, string bcgColor = "white")
            : base(buttonText, href)
        {
            Id = "";
            _buttonText = buttonText;
            _href = href;
            _backgroundColor = bcgColor;
            ButtonHtml = GetHtml();
        }
        
        private string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _href);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "href-open-button");
                writer.AddStyleAttribute("background", _backgroundColor);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(_buttonText);
                writer.RenderEndTag(); //A
            }
            return stringWriter.ToString();
        }
    }
}
