using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class ReportMenuItem : HtmlBaseElement
    {
        public string Href;
        public double Value;

        public ReportMenuItem(string title, string href, string id = "")
        {
            Title = title;
            Id = id.Equals("") ? title.ToCamelCase() : id;
            Href = href;
        }
    }
}
