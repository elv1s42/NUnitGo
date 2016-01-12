using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.HtmlCustomElements.CSSElements;

namespace NunitGo.HtmlCustomElements.HtmlCustomElements
{
    public class ModalBackground : HtmlBaseElement
    {
        private static string _id;
        private static string _zIndex;
        public string ModalBackgroundHtml;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public ModalBackground(string id = "modal-background", string zIndex = "1001")
        {
            _id = id;
            _zIndex = zIndex;
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
					new StyleAttribute("filter", "alpha(opacity=30)"),
					new StyleAttribute("-moz-opacity", "0.3"),
					new StyleAttribute("opacity", ".30"),
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
                writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, _zIndex);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, _id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-background");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
