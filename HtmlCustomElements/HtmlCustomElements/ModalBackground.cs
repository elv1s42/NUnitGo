using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class ModalBackground : HtmlBaseElement
    {
        private static string _id;
        public string ModalBackgroundHtml;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public ModalBackground(string id = "modal-background")
        {
            _id = id;
            Style = GetStyle();
            ModalBackgroundHtml = GetHtml();
        }

        private static string GetStyle()
        {
            var modalBcgCssSet = new CssSet("modal-background-style");
            modalBcgCssSet.AddElement(new CssElement(".modal-background")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("filter", "alpha(opacity=70)"),
					new StyleAttribute("-moz-opacity", "0.7"),
					new StyleAttribute("opacity", ".70"),
					new StyleAttribute(HtmlTextWriterStyle.ZIndex, "1001"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "black"),
					new StyleAttribute(HtmlTextWriterStyle.Top, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Left, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Position, "fixed"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            return modalBcgCssSet.ToString();
        }

        private static string GetHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, _id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-background");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
