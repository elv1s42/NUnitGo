using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBar : HtmlBaseElement
    {
        public List<HorizontalBarElement> Elements;
        public string Bar;

        public HorizontalBar(string id, string title, List<HorizontalBarElement> elements, string style = "")
        {
            Id = id;
            Style = style;
            Title = title;
            Elements = elements;
            Bar = GetBar();
        }

        private string GetBar()
        {
            var barCssSet = new CssSet("bar-style");
            barCssSet.AddElement(new CssElement(".horizontal-bar")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10% 10% 10% 10%"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "table"),
					new StyleAttribute("table-layout", "fixed")
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

                var sum = Elements.Sum(x => x.Value);

                var sortedItems = Elements.OrderByDescending(x => x.Value);
                foreach (var item in sortedItems)
                {
                    var value = item.Value;
                    var width = Math.Max((value/sum)*100, 0.0);
                    Console.WriteLine("width: " + width);
                    writer.AddAttribute("data-tooltip", item.TooltipText);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tooltip");
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, "horizontal-bar-item");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, item.BackgroundColor);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString("##.##") + @"%");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(item.InnerText);
                    writer.RenderEndTag(); //A
                    
                    writer.RenderEndTag(); //DIV
                }
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }

    }
}
