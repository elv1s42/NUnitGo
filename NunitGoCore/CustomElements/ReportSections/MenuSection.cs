using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.ReportSections
{
    public class MenuSection : HtmlBaseElement
    {
        public List<ReportMenuItem> Elements;
        public string ReportMenuHtml;

        public MenuSection(List<ReportMenuItem> elements, string id = "", string title = "")
        {
            Id = id;
            Title = title;
            Elements = elements;
            ReportMenuHtml = GetReportMenuHtml();
        }
        
        private string GetReportMenuHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {

                var menuName = new SectionName("Main menu");
                writer
                    .WriteString(menuName.HtmlCode)
                    .Class("border border-0 p-3 mb-3")
                    .H2("Report sections: ")
                    .Class("menu")
                    .RenderBeginTag("nav");
                foreach (var element in Elements)
                {
                    writer
                        .Class("menu-item")
                        .Href(element.Href)
                        .OpenTag(HtmlTextWriterTag.A)
                        .Class(element.Octicon)
                        .Tag(HtmlTextWriterTag.Span)
                        .Text(element.Title)
                        .CloseTag();//A
                }
                writer.CloseTag();//NAV
            }
            return stringWriter.ToString();
        }
    }
}
