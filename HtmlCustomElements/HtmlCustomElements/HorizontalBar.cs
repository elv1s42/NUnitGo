﻿using System;
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
        public static string StyleString
        {
            get { return GetStyle(); }
        }
        
        public HorizontalBar(string id, string title, List<HorizontalBarElement> elements)
        {
            Id = id;
            Style = GetStyle();
            Title = title;
            Elements = elements;
            Bar = GetBar();
        }

        private static string GetStyle()
        {
            var barCssSet = new CssSet("bar-style");
            barCssSet.AddElement(new CssElement(".horizontal-bar")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("-webkit-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("-moz-box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
                    new StyleAttribute("box-shadow", "3px 3px 5px 0px rgba(166,166,166,0.75)"),
					new StyleAttribute("border-bottom", "1px solid #BFBFBF"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"),
					new StyleAttribute(HtmlTextWriterStyle.Width, "80%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "5% 10% 5% 10%"),
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
            return barCssSet.ToString();
        }

        private string GetBar()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "horizontal-bar");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                var sum = Elements.Sum(x => x.Value);

                var sortedItems = Elements.OrderByDescending(x => x.Value);
                foreach (var tooltip in
                    from item in sortedItems
                    let value = item.Value
                    let width = Math.Max((value / sum) * 100, 0.0)
                    select new Tooltip(item.TooltipText, item.InnerText, item.BackgroundColor, "horizontal-bar-item", width))
                {
                    writer.Write(tooltip.HtmlCode);
                }
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }

    }
}
