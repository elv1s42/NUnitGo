using System;
using System.IO;
using System.Web.UI;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class JsOpenButton : HrefButton
    {
        private readonly string _idToOpen;
        private readonly string _backgroundId;
        private readonly string _buttonText;
        private readonly string _href;
        public string ButtonHtml;
        
        public JsOpenButton(string buttonText, string idToOpen,
            string href = "javascript:void(0)", string backgroundId = "modal-background")
            : base(buttonText, href)
        {
            _idToOpen = idToOpen;
            _backgroundId = backgroundId;
            _buttonText = buttonText;
            _href = href;
            ButtonHtml = GetHtml();
        }

        private string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                var onClickString = String.Format("document.getElementById('{0}').style.display='block';" +
                                    "document.getElementById('{1}').style.display='block'",
                                    _idToOpen, _backgroundId);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _href);
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClickString);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "href-button");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(_buttonText);
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
