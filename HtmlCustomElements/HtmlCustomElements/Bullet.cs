using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Bullet : HtmlBaseElement
    {
        public static string HtmlCode
        {
            get { return GetHtmlCode(); }
        }
        public static string StyleString
        {
            get { return GetStyle(); }
        }
        
        public static string GetStyle()
        {
            var mainInfoCssSet = new CssSet("main-information-style");
            mainInfoCssSet.AddElement(new CssElement("em.bullet")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("border-radius", "3px"),
                    new StyleAttribute(HtmlTextWriterStyle.Width, "7px"),
                    new StyleAttribute(HtmlTextWriterStyle.Height, "7px"),
                    new StyleAttribute(HtmlTextWriterStyle.Margin, "0 5px"),
                    new StyleAttribute("background", Colors.Bullet),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "inline-block")
				}
            });
            return mainInfoCssSet.ToString();
        }

        public static string GetHtmlCode()
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "bullet");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "bullet-item");
                writer.RenderBeginTag(HtmlTextWriterTag.Em);
                writer.RenderEndTag();
            }

            return strWr.ToString();
        }
    }
}
