using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class BarItem
    {
        public string BackgroundColor;
        //public string 
    }

    public class HorizontalBar : HtmlBaseElement
    {
        public HorizontalBar(string id, string style, string title)
        {
            Id = id;
            Style = style;
            Title = title;
        }

        static string GetBar(Dictionary<string, int> items)
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                var sortedItems = items.OrderBy(x => x.Value);

                //TODO: foreach items
            }
            return stringWriter.ToString();
        }

    }
}
