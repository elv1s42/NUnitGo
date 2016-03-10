using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.CSSElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
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
            tooltipCssSet.AddElement(new CssElement(".tooltip:after")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("background", Colors.TooltipBackground),
                    new StyleAttribute("-webkit-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("-moz-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
					new StyleAttribute(HtmlTextWriterStyle.Color, "white"),
					new StyleAttribute("content", "attr(data-tooltip)"),
					new StyleAttribute(HtmlTextWriterStyle.MarginTop, "50px"),
					new StyleAttribute(HtmlTextWriterStyle.Left, "-9999px"),
					new StyleAttribute("opacity", "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px 15px"),
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute"),
					new StyleAttribute(HtmlTextWriterStyle.Visibility, "hidden"),
					new StyleAttribute("transition", "all 0.5s ease-out")
				}
            });
            tooltipCssSet.AddElement(new CssElement(".tooltip:hover:after")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("opacity", "1"),
					new StyleAttribute(HtmlTextWriterStyle.Left, "20%"),
                    new StyleAttribute(HtmlTextWriterStyle.Visibility, "visible")
				}
            });
            return tooltipCssSet.ToString();
        }

        public Tooltip(string tooltipText, string innerText, string backgroundColor, string id,  double width, string href = "")
        {
            Style = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute("data-tooltip", tooltipText);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, backgroundColor);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString("00.000", CultureInfo.InvariantCulture) + @"%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                if (!href.Equals(""))
                {
                    var button = new OpenButton("", href, backgroundColor);
                    writer.Write(button.ButtonHtml);
                }

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "hidden");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, "tooltip-item-inner-text");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                if (innerText.Contains(Environment.NewLine))
                {
                    var lines = innerText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        writer.Write(line);
                        writer.AddTag(HtmlTextWriterTag.Br);
                    }
                }
                else
                {
                    writer.Write(innerText);
                }
                writer.RenderEndTag(); //A

                writer.RenderEndTag(); //DIV

            }

            HtmlCode = strWr.ToString();
        }
    }
}
