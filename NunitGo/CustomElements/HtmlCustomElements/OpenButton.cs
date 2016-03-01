﻿using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGoCore.CustomElements.CSSElements;

namespace NunitGoCore.CustomElements.HtmlCustomElements
{
    public class OpenButton : HrefButtonBase
    {
        private readonly string _buttonText;
        private readonly string _href;
        private readonly string _backgroundColor = "white";
        public string ButtonHtml;
        public new static string StyleString
        {
            get { return GetStyle(); }
        }
        
        public OpenButton(string buttonText, string href, string bcgColor = "white")
            : base(buttonText, href)
        {
            Id = "";
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
                    new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
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
