using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class HrefButton : HtmlBaseElement
    {
        private readonly string _buttonText;
        private readonly string _href;
        public string HrefButtonHtml;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public HrefButton(string id, string buttonText, string href)
        {
            Id = id;
            _buttonText = buttonText;
            _href = href;
            Style = GetStyle();
            HrefButtonHtml = GetHtml();
        }

        public HrefButton(string buttonText, string href)
        {
            _buttonText = buttonText;
            _href = href;
            Style = GetStyle();
            HrefButtonHtml = GetHtml();
        }

        private static string GetStyle()
        {
            var hrefButtonCssSet = new CssSet("href-button-style");
            hrefButtonCssSet.AddElement(new CssElement(".href-button")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            hrefButtonCssSet.AddElement(new CssElement(".href-button:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Color, "red"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none !important")
				}
            });
            return hrefButtonCssSet.ToString();
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
