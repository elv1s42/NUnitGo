using System;
using System.IO;
using System.Web.UI;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class JsCloseButton : HrefButton
    {
        private readonly string _idToClose;
        private readonly string _backgroundId;
        private const string ButtonText = "Close";
        private readonly string _href;
        public string ButtonHtml;

        public JsCloseButton(string idToClose, string backgroundId = "modal-background",
            string href = "javascript:void(0)") 
            : base(ButtonText, href)
        {
            _idToClose = idToClose;
            _backgroundId = backgroundId;
            _href = href;
            ButtonHtml = GetHtml();
        }

        private string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                var onClickString = String.Format("document.getElementById('{0}').style.display='none';" +
                                    "document.getElementById('{1}').style.display='none'",
                                    _idToClose, _backgroundId);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _href);
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClickString);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "href-button");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(ButtonText);
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
