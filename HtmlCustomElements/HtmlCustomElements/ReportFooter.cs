using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class ReportFooter : HtmlBaseElement
    {
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        public string HtmlCode
        {
            get { return GetCode(); }
        }

        public ReportFooter()
        {
            Title = "NUnitGo Test Run Report";
            Id = "main-title";
            Style = GetStyle();
        }

        private static string GetStyle()
        {
            var barCssSet = new CssSet("footer-style");
            barCssSet.AddElement(new CssElement(".report-footer")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Position, "relative"),
                    new StyleAttribute(HtmlTextWriterStyle.MarginTop, "-3em"),
                    new StyleAttribute(HtmlTextWriterStyle.Height, "3em"),
                    new StyleAttribute(HtmlTextWriterStyle.ZIndex, "10"),
                    new StyleAttribute("clear", "both"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "15px")
				}
            });
            return barCssSet.ToString();
        }

        private string GetCode()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "report-footer");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("Copyright 2015 " + '\u00a9' + " NUnitGo");
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            return stringWriter.ToString();
        }
    }
}
