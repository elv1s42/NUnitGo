using System.Web.UI;
using NunitGo.Extensions;
using NunitGo.NunitGoItems;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class FailureSection
    {
        public static void AddFailure(this HtmlTextWriter writer, NunitGoTest nunitGoTest)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "table-cell");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!nunitGoTest.IsSuccess())
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Stack trace: ");
                writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestStackTrace));
                writer.RenderEndTag(); //P
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Message: ");
                writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestMessage));
                writer.RenderEndTag(); //P
            }
            writer.RenderEndTag();//DIV
        }
    }
}
