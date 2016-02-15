using System;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Extensions;

namespace NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections
{
    public static class EnvironmentSection
    {
        public static void AddEnvironment(this HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "table-cell");
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
        }
    }
}
