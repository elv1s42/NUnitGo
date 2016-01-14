using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NunitGo.HtmlCustomElements.CSSElements;

namespace NunitGo.HtmlCustomElements.HtmlCustomElements
{
    public class HorizontalBar : HtmlBaseElement
    {
        public List<HorizontalBarElement> Elements;
        public string BarHtml;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        private readonly bool _orderByDescending;


        public HorizontalBar(string id, string title, List<HorizontalBarElement> elements, bool orderByDescending = true)
        {
            Id = id;
            _orderByDescending = orderByDescending;
            Style = GetStyle();
            Title = title;
            Elements = elements;
            BarHtml = GetBar();
        }

        private static string GetStyle()
        {
            var barCssSet = new CssSet("bar-style");
            barCssSet.AddElement(new CssElement(".horizontal-bar")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "94%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "1% 3% 1% 3%"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "table"),
					new StyleAttribute("table-layout", "fixed")
				}
            });
            barCssSet.AddElement(new CssElement("#horizontal-bar-item")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("-webkit-box-shadow", "0px 5px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("-moz-box-shadow", "0px 5px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("box-shadow", "0px 5px 5px 0px rgba(166,166,166,0.75)"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "table-cell"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px")
				}
            });
            barCssSet.AddElement(new CssElement("#horizontal-bar-item:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("-webkit-box-shadow", "0px 10px 10px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("-moz-box-shadow", "0px 10px 10px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("box-shadow", "0px 10px 10px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute(HtmlTextWriterStyle.MarginBottom, "10px"),
					//new StyleAttribute("opacity", "0.75")
				}
            });
            return barCssSet.ToString();
        }

        private string GetBar()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "horizontal-bar");
                if (!Title.Equals("")) 
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                var sum = Elements.Sum(x => x.Value);

                var sortedItems = Elements.Where(x => x.Value >= 0.0000001);
                if(_orderByDescending) sortedItems = sortedItems.OrderByDescending(x => x.Value);
                foreach (var item in sortedItems)
                {
                    var value = item.Value;
                    var width = Math.Max((value / sum) * 100, 0.01);
                    var tooltip = new Tooltip(item.TooltipText, item.InnerText, item.BackgroundColor, "horizontal-bar-item",
                        width, item.Href);
                    writer.Write(tooltip.HtmlCode);
                }
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }

    }
}
