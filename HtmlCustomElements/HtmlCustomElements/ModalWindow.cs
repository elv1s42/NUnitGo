using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class ModalWindow : HtmlBaseElement
    {
        public string InnerHtml;
        public string ModalWindowHtml;
        public static string StyleString
        {
            get { return GetStyle(); }
        }
        
        public ModalWindow(string id, string innerHtml)
        {
            Id = id;
            InnerHtml = innerHtml;
            Style = GetStyle();
            ModalWindowHtml = GetWindow();
        }

        private static string GetStyle()
        {
            var modalWindowCssSet = new CssSet("modal-window-style");
            modalWindowCssSet.AddElement(new CssElement(".modal-window")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.ZIndex, "1002"),
					new StyleAttribute(HtmlTextWriterStyle.Overflow, "auto"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
					new StyleAttribute(HtmlTextWriterStyle.Top, "25%"),
					new StyleAttribute(HtmlTextWriterStyle.Left, "25%"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "50%"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "50%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px"),
					new StyleAttribute("border", "10px solid " + Colors.ModalBorderColor),
					new StyleAttribute(HtmlTextWriterStyle.Position, "fixed"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            return modalWindowCssSet.ToString();
        }

        private string GetWindow()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "modal-window");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.Write(InnerHtml);
                var closeButton = new JsCloseButton(Id);
                writer.Write(closeButton.ButtonHtml);

                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
