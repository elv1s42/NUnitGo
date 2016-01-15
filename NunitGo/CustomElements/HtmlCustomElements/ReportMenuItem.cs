﻿namespace NunitGo.CustomElements.HtmlCustomElements
{
    public class ReportMenuItem : HtmlBaseElement
    {
        public string InnerHtml;
        public string Href;
        public double Value;

        public ReportMenuItem(string innerHtml, string title, string href, string id = "")
        {
            InnerHtml = innerHtml;
            Title = title;
            Id = id.Equals("") ? title.ToCamelCase() : id;
            Href = href;
        }
    }
}
