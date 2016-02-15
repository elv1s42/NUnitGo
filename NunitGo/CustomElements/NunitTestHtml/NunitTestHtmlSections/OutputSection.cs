using System.Web.UI;
using NunitGo.Extensions;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class OutputSection
    {
        public static void AddOutput(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string testOutput)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "table-cell");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (nunitGoTest.HasOutput)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test output: ");
                writer.Write(NunitTestHtml.GenerateTxtView(testOutput));
                writer.RenderEndTag(); //P
            }
            writer.RenderEndTag();//DIV
        }
    }
}
