using System.IO;
using System.Web.UI;
using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.ReportSections
{
    public static class FooterSection
    {
        public static string HtmlCode => GetCode();
        
        private static string GetCode()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Css(HtmlTextWriterStyle.TextAlign, "center")
                    .Class("border border-0 p-3 mb-3")
                    .Tag(HtmlTextWriterTag.Div, () => writer
                        .Css(HtmlTextWriterStyle.Position, "relative")
                        .Div(() => writer
                            .Text("Copyright 2015-2016 " + '\u00a9' + " NUnitGo")
                        )
                    );
            }
            return stringWriter.ToString();
        }
    }
}
