using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Tooltip
    {
        public string StyleString;
        public string HtmlCode;

        public static string GetStyle()
        {
            var tooltipCssSet = new CssSet("tooltip-style");
            tooltipCssSet.AddElement(new CssElement("div:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            tooltipCssSet.AddElement(new CssElement(".tooltip")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("border-bottom", "1px dotted #0077AA"), 
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%") 
				}
            });
            tooltipCssSet.AddElement(new CssElement(".tooltip::after")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("background", "#BFBFBF"),
					new StyleAttribute("border-radius", "8px 8px 8px 0px"),
					new StyleAttribute("box-shadow", "1px 1px 10px rgba(0, 0, 0, 0.5)"),
					new StyleAttribute(HtmlTextWriterStyle.Color, "#FFF"),
					new StyleAttribute("content", "attr(data-tooltip)"),
					new StyleAttribute(HtmlTextWriterStyle.MarginTop, "-24px"),
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
            StyleString = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute("data-tooltip", tooltipText);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, backgroundColor);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString("N", CultureInfo.InvariantCulture) + @"%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.Write(innerText);
                writer.RenderEndTag(); //A

                writer.RenderEndTag(); //DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
