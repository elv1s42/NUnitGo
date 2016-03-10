using System;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;

namespace NUnitGoCore.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class EnvironmentSection
    {
        public static HtmlTextWriter AddEnvironment(this HtmlTextWriter writer, string id = "")
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id.Equals("") ? "table-cell" : id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddTag(HtmlTextWriterTag.B, "Environment information: ");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "CLR version: " + Environment.Version);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "OS version: " + Environment.OSVersion.VersionString);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "Platform: " + Environment.OSVersion.Platform);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "Machine name: " + Environment.MachineName);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "User domain: " + Environment.UserName);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Bullet.HtmlCode + "User: " + Environment.UserDomainName);
            writer.RenderEndTag();
            writer.RenderEndTag();//DIV
            return writer;
        }
    }
}
