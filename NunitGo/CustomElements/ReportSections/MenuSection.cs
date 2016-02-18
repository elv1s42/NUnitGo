using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Extensions;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    public class MenuSection : HtmlBaseElement
    {
        public List<ReportMenuItem> Elements;
        public string ReportMenuHtml;
        public static string StyleString
        {
            get { return GetStyleString(); }
        }

        public MenuSection(List<ReportMenuItem> elements, string id = "", string title = "")
        {
            Id = id;
            Style = GetStyleString();
            Title = title;
            Elements = elements;
            ReportMenuHtml = GetReportMenuHtml();
        }

        private static string GetStyleString()
        {
            var barCssSet = new CssSet("reportmenu-style");
            barCssSet.AddElement(new CssElement(".reportmenu")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.MarginBottom, "10%")
				}
            });
            barCssSet.AddElement(new CssElement(".reportmenu .tab")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "25px"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingTop, "0.25em"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10.25em"),
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(".reportmenu .tab-close")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingBottom, "1%"),
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(".reportmenu .reportmenu-tab")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Display, "block"),
					new StyleAttribute(HtmlTextWriterStyle.Height, "35px"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            barCssSet.AddElement(new CssElement(".reportmenu .tab:hover,.reportmenu div:target .tab")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("-o-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-ms-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-moz-transition:color", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("-webkit-transition", "0.5s ease-out, background 0.5s ease-in"),
                    new StyleAttribute("transition", "0.5s ease-out, background 0.5s ease-in"),
					new StyleAttribute("background", Colors.ReportMenuHoverBackground),
					new StyleAttribute(HtmlTextWriterStyle.PaddingTop, "0.25em"),
					new StyleAttribute(HtmlTextWriterStyle.PaddingLeft, "10.75em"),
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            return barCssSet.ToString();
        }

        private string GetReportMenuHtml()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "reportmenu");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer
                    .Css(HtmlTextWriterStyle.TextAlign, "center")
                    .Css("padding", "20px")
                    .Css("margin", "0")
                    .Css(HtmlTextWriterStyle.Position, "relative")
                    .Tag(HtmlTextWriterTag.H2,
                        () => writer.Text(Title));

                foreach (var element in Elements)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, element.Id);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "reportmenu-tab");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, element.Href);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "tab");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(element.Title);
                    writer.RenderEndTag();

                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
