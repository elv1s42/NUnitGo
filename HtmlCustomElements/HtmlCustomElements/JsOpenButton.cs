using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class JsOpenButton : HrefButton
    {
        private readonly string _idToOpen;
        private readonly string _backgroundId;
        private readonly string _buttonText;
        private readonly string _href;
        private readonly string _backgroundColor;
        public string ButtonHtml;
        public new static string StyleString
        {
            get { return GetStyle(); }
        }
        
        public JsOpenButton(string buttonText, string idToOpen, string backgroundId = "modal-background", string bcgColor = "white",
            string href = "javascript:void(0)")
            : base(buttonText, href)
        {
            _idToOpen = idToOpen;
            _backgroundId = backgroundId;
            _buttonText = buttonText;
            _href = href;
            _backgroundColor = bcgColor;
            ButtonHtml = GetHtml();
            Style = GetStyle();
        }

        private static string GetStyle()
        {
            var hrefButtonCssSet = new CssSet("href-open-button-style");
            hrefButtonCssSet.AddElement(new CssElement(".href-open-button")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none"),
                    new StyleAttribute(HtmlTextWriterStyle.Padding, "2px")
				}
            });
            hrefButtonCssSet.AddElement(new CssElement(".href-open-button:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("filter", "alpha(opacity=85)"),
					new StyleAttribute("-moz-opacity", "0.85"),
					new StyleAttribute("opacity", ".85"),
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
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
                var onClickString = String.Format("document.getElementById('{0}').style.display='block';" +
                                    "document.getElementById('{1}').style.display='block'",
                                    _idToOpen, _backgroundId);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, _href);
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClickString);
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
