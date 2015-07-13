using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBar : HtmlBaseElement
    {
        public HorizontalBar(string id, string title, string style = "")
        {
            Id = id;
            Style = style;
            Title = title;
        }

        public string GetBar(List<HorizontalBarElement> elements)
        {
            var barCssSet = new CssSet("bar-style");
            barCssSet.AddElement(new CssElement(".horizontal-bar")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "table"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10% 10%"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "80%")
				}
            }); 
            barCssSet.AddElement(new CssElement("#horizontal-bar-item")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "table-cell")
				}
            });

            Style = barCssSet.ToString();

            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "horizontal-bar");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                var sortedItems = elements.OrderByDescending(x => x.Value);
                foreach (var item in sortedItems)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "horizontal-bar-item");
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, item.InnerText);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, item.BackgroundColor);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(item.InnerText);
                    
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(item.TooltipText);
                    
                    writer.RenderEndTag(); //SPAN
                    writer.RenderEndTag(); //A
                    writer.RenderEndTag(); //DIV
                }
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }

    }
}
