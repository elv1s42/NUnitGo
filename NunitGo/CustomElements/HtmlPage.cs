using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements
{
    public class HtmlPage
    {
        private string _page;

        public static string StyleString
        {
            get
            {
                var mainCssSet = new CssSet("main-style"); 
                mainCssSet.AddElement(new CssElement("body")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute("background", Colors.BodyBackground),
                        new StyleAttribute(HtmlTextWriterStyle.Margin, "0px"),
                        new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
                        new StyleAttribute(HtmlTextWriterStyle.FontFamily, "Tahoma,Verdana,Segoe,sans-serif")
                        //new StyleAttribute(HtmlTextWriterStyle.FontFamily, "\"Segoe UI\",Frutiger,\"Frutiger Linotype\"," +
                        //                                                   "\"Dejavu Sans\",\"Helvetica Neue\",Arial,sans-serif")
                        //new StyleAttribute(HtmlTextWriterStyle.FontFamily, "\"Franklin Gothic Medium\",\"Franklin Gothic\"," +
                        //    "\"ITC Franklin Gothic\",Arial,sans-serif")
                    }
                });
                mainCssSet.AddElement(new CssElement("html")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute(HtmlTextWriterStyle.Height, "100%")
                    }
                });
                mainCssSet.AddElement(new CssElement(".stop-scrolling")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
                        new StyleAttribute(HtmlTextWriterStyle.Overflow, "hidden")
                    }
                });
                mainCssSet.AddElement(new CssElement("div:hover")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
                    }
                });
                mainCssSet.AddElement(new CssElement("a:hover")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
                    }
                });
                return mainCssSet.ToString();
            }
        }

        public HtmlPage(string pageTitle = "NUnitGo Report", string styleFullPath = "")
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Html);
                writer.Write("<!DOCTYPE html>");
                writer.Write(Environment.NewLine);

                writer.RenderBeginTag(HtmlTextWriterTag.Head);

                writer.AddTag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                {
                    {"http-equiv", "X-UA-Compatible"},
                    {"content", @"IE=edge"},
                    {"charset", "utf-8"}
                });
                writer.AddTag(HtmlTextWriterTag.Title, pageTitle);
                writer.AddTag(HtmlTextWriterTag.Style, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Type, @"text/css"}
                }); 
                writer.AddTag(HtmlTextWriterTag.Link, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Rel, @"stylesheet"},
                    {HtmlTextWriterAttribute.Type, @"text/css"},
                    {HtmlTextWriterAttribute.Href, styleFullPath.Equals("") ? Output.Outputs.ReportStyle : styleFullPath}
                });

                writer.RenderEndTag(); //HEAD

                writer.AddTag(HtmlTextWriterTag.Body);

                writer.Write(Environment.NewLine);

                writer.AddTag("footer");

                //writer.RenderEndTag(); //HTML
                writer.Write(Environment.NewLine);
                writer.Write("</html>");
                writer.Write(Environment.NewLine);

            }
            _page = strWr.ToString();
        }

        public string GetFullPage()
        {
            return _page;
        }

        public string AddInsideTag(string tagName, string stringToAdd)
        {
            var lines = _page.SplitToLines().ToList();
            foreach (var line in lines.Where(line => line.Contains(@"</" + tagName + @">")))
            {
                lines.Insert(lines.IndexOf(line), stringToAdd);
                _page = String.Join(Environment.NewLine, lines);
                return _page;
            }
            return _page;
        }

        public string AddToBody(string stringToAdd)
        {
            return AddInsideTag("body", stringToAdd);
        }

        public string AddScripts(string scriptString)
        {
            return AddInsideTag("script", scriptString);
        }

        public void SavePage(string fullpath)
        {
            File.WriteAllText(fullpath, _page);
        }
    }
}
