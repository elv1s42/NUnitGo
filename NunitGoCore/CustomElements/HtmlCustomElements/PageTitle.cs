using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGoCore.CustomElements.CSSElements;
using NunitGoCore.Extensions;
using NunitGoCore.Utils;

namespace NunitGoCore.CustomElements.HtmlCustomElements
{
    public class PageTitle : HtmlBaseElement
    {
        public static string ClassName;

        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public string HtmlCode
        {
            get { return GetCode(); }
        }

        public PageTitle(string title = "Test Run Report", string id = "main-title")
        {
            Title = title;
            Id = id;
            Style = GetStyle();
        }

        private static string GetStyle()
        {
            var titleCssSet = new CssSet("title-style");
            titleCssSet.AddElement(new CssElement(".report-title")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.TestBorderColor)
				}
            });
            return titleCssSet.ToString();
        }

        private string GetCode()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Css(HtmlTextWriterStyle.BackgroundColor, Colors.TestBorderColor)
                    .Css(HtmlTextWriterStyle.TextAlign, "center")
                    .Css("padding", "20px")
                    .Css("margin", "0")
                    .Css(HtmlTextWriterStyle.Position, "relative")
                    .CssShadow("0 0 20px -5px black")
                    .Tag(HtmlTextWriterTag.H2,
                        () => writer.Text(Title));
            }
            return stringWriter.ToString();
        }
    }
}
