using System.Web.UI;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems;

namespace NUnitGoCore.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class FailureSection
    {
        public static HtmlTextWriter AddFailure(this HtmlTextWriter writer, NunitGoTest nunitGoTest, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!nunitGoTest.IsSuccess())
            {
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Message: ");
                writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestMessage));
                writer.RenderEndTag(); //P
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Stack trace: ");
                writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestStackTrace));
                writer.RenderEndTag(); //P
            }
            else
            {
                writer.Write("Test was successful, there is no failure message");
            }
            writer.RenderEndTag();//DIV
            return writer;
        }
    }
}
