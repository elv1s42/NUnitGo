using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements.HtmlCustomElements
{
    public class MainInformation : HtmlBaseElement
    {
        public string HtmlCode;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        private new const string Id = "main-information";
        private const string _id = "#" + Id + " ";

        public static string GetStyle()
        {
            var mainInfoCssSet = new CssSet("main-information-style");
            mainInfoCssSet.AddElement(new CssElement(_id)
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "1% 0% 0% 0%"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "1% 2% 2% 2%"),
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, "white"), 
					new StyleAttribute(HtmlTextWriterStyle.Width, "80%"), 
					new StyleAttribute(HtmlTextWriterStyle.Height, "20%"), 
					new StyleAttribute(HtmlTextWriterStyle.MarginLeft, "10%"), 
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none") 
				}
            }); 
            mainInfoCssSet.AddElement(new CssElement(_id + "h1")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute("font", "22px/36px Arial, sans-serif")
				}
            });
            mainInfoCssSet.AddElement(new CssElement(_id + "p")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Margin, "10px 0"),
                    new StyleAttribute("font", "15px/19px Arial, sans-serif")
				}
            });
            mainInfoCssSet.AddElement(new CssElement(_id + "span")
            {
                StyleFields = new List<StyleAttribute>
				{
                    new StyleAttribute(HtmlTextWriterStyle.Color, "#4f4f4f"),
                    new StyleAttribute("font", "italic 11px/12px Georgia, Arial, sans-serif")
				}
            });
            return mainInfoCssSet.ToString();
        }

        public MainInformation(MainStatistics stats)
        {
            Style = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table-cell");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-1");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Main information:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Start Date: " + stats.StartDate);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Finish Date: " + stats.EndDate);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Time: " + stats.Duration);
                writer.RenderEndTag();
                writer.RenderEndTag();//DIV
                
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table-cell");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-2");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Main results:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Total: " + stats.TotalAll);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Success: " + stats.TotalPassed);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Errors: " + stats.TotalBroken);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Failures: " + stats.TotalFailed);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Inconclusive: " + stats.TotalInconclusive);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Ignored: " + stats.TotalIgnored);
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table-cell");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-3");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Environment:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "CLR version: " + System.Environment.Version);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "OS version: " + System.Environment.OSVersion.VersionString);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Platform: " + System.Environment.OSVersion.Platform);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Machine name: " + System.Environment.MachineName);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "User domain: " + System.Environment.UserName);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "User: " + System.Environment.UserDomainName);
                writer.RenderEndTag();
                writer.RenderEndTag();//DIV
                
                writer.RenderEndTag();//DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
