using System.IO;
using System.Web.UI;
using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
    public class SectionName : HtmlBaseElement
    {
        public static string ClassName;

        public string HtmlCode => GetCode();

        public SectionName(string name)
        {
            Title = name;
        }

        private string GetCode()
        {
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer
                    .Class("border-bottom p-3 mb-3")
                    .Div(() => writer
                        .H1(Title)
                    );
            }
            return stringWriter.ToString();
        }
    }
}
