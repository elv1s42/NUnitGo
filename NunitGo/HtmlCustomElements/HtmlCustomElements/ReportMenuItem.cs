namespace NunitGo.HtmlCustomElements.HtmlCustomElements
{
    public class ReportMenuItem : HtmlBaseElement
    {
        public string InnerHtml;
        public string BackgroundColor;
        public double Value;

        public ReportMenuItem(string innerHtml, string title, string id = "")
        {
            InnerHtml = innerHtml;
            Title = title;
            Id = id.Equals("") ? title.ToCamelCase() : id;
        }
    }
}
