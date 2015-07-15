using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Tooltip : HtmlBaseElement
    {
        public string HtmlCode;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public static string GetStyle()
        {
            var tooltipCssSet = new CssSet("tooltip-style");
            tooltipCssSet.AddElement(new CssElement(".tooltip")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%") 
				}
            });
            tooltipCssSet.AddElement(new CssElement("#tooltip-item-inner-text")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextAlign, "center"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%") 
				}
            });
            tooltipCssSet.AddElement(new CssElement(".tooltip::after")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("background", "#BFBFBF"),
					new StyleAttribute("border-radius", "8px 8px 8px 0px"),
                    new StyleAttribute("-webkit-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("-moz-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
					new StyleAttribute(HtmlTextWriterStyle.Color, "#FFF"),
					new StyleAttribute("content", "attr(data-tooltip)"),
					new StyleAttribute(HtmlTextWriterStyle.MarginTop, "-50px"),
					new StyleAttribute("opacity", "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "3px 7px"),
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute"),
					new StyleAttribute(HtmlTextWriterStyle.Visibility, "hidden"),
					new StyleAttribute("transition", "all 0.1s ease-in-out")
				}
            });
            tooltipCssSet.AddElement(new CssElement(".tooltip:hover::after")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("opacity", "1"),
                    new StyleAttribute(HtmlTextWriterStyle.Visibility, "visible")
				}
            });
            return tooltipCssSet.ToString();
        }

        public Tooltip(string tooltipText, string innerText, string backgroundColor, string id,  double width)
        {
            Style = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute("data-tooltip", tooltipText);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, backgroundColor);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString("N", CultureInfo.InvariantCulture) + @"%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "tooltip-item-inner-text");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(innerText);
                writer.RenderEndTag(); //A

                writer.RenderEndTag(); //DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
