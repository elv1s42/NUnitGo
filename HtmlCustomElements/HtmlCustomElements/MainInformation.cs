using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.TestResultClasses;

namespace HtmlCustomElements.HtmlCustomElements
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

        public MainInformation(TestResults fullResults, TestResults currentResults)
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
                writer.Write(Bullet.HtmlCode + "Date: " + fullResults.Date);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Time: " + fullResults.Time);
                writer.RenderEndTag();
                var splitedName = fullResults.Name.Split('\\');
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "File Name: " + splitedName.Last());
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "File location: " + String.Join(@"\", splitedName.Take(splitedName.Count() - 1)));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "File type: " + fullResults.TestSuite.Type);
                writer.RenderEndTag();
                writer.RenderEndTag();

                var currentTestCases = currentResults.TestSuite.Results.TestCases;

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table-cell");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-2");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Main results:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Total: " + currentTestCases.Count);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Success: " + currentTestCases.Count(x => x.Result.Equals("Success")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Errors: " + currentTestCases.Count(x => x.Result.Equals("Error")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Failures: " + currentTestCases.Count(x => x.Result.Equals("Failure")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Not run: " + currentTestCases.Count(x => x.Executed.Equals("False")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Inconclusive: " + currentTestCases.Count(x => x.Result.Equals("Inconclusive")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Ignored: " + currentTestCases.Count(x => x.Result.Equals("Ignored")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Skipped: " + currentTestCases.Count(x => x.Result.Equals("Ignored")));
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Invalid: " + currentTestCases.Count(x => x.Result.Equals("Unknown")));
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "table-cell");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-3");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Environment:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "NUnit version: " + fullResults.Environment.NunitVersion);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "CLR version: " + fullResults.Environment.ClrVersion);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "OS version: " + fullResults.Environment.OsVersion);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Platform: " + fullResults.Environment.Platform);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Machine name: " + fullResults.Environment.MachineName);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "User domain: " + fullResults.Environment.UserDomain);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "User: " + fullResults.Environment.User);
                writer.RenderEndTag();
                writer.RenderEndTag();
                
                writer.RenderEndTag();//DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
