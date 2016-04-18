namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class ReportMenuItem : HtmlBaseElement
    {
        public string Href;
        public string Octicon;

        public ReportMenuItem(string title, string href, string octicon = "")
        {
            Title = title;
            Octicon = octicon;
            Href = href;
        }
    }
}
