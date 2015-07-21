using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;

namespace HtmlCustomElements
{
    public class HtmlPage
    {
        private string _page;

        public static string PageStyle
        {
            get
            {
                var mainCssSet = new CssSet("main-style"); 
                mainCssSet.AddElement(new CssElement("body")
                {
                    StyleFields = new List<StyleAttribute>
                    {
                        new StyleAttribute("background", Colors.BodyBackground),
                        new StyleAttribute(HtmlTextWriterStyle.Height, "100%"),
                        new StyleAttribute(HtmlTextWriterStyle.FontFamily, "Tahoma,Verdana,Segoe,sans-serif")
                        //new StyleAttribute(HtmlTextWriterStyle.FontFamily, "\"Segoe UI\",Frutiger,\"Frutiger Linotype\"," +
                        //                                                   "\"Dejavu Sans\",\"Helvetica Neue\",Arial,sans-serif")
                        //new StyleAttribute(HtmlTextWriterStyle.FontFamily, "\"Franklin Gothic Medium\",\"Franklin Gothic\"," +
                        //    "\"ITC Franklin Gothic\",Arial,sans-serif")
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

        public HtmlPage(string pageTitle)
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Html);

                writer.RenderBeginTag(HtmlTextWriterTag.Head);

                writer.AddTag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                {
                    {"http-equiv", "Content-Type"},
                    {"content", @"text/html"},
                    {"charset", "utf-8"}
                });
                writer.AddTag(HtmlTextWriterTag.Title, pageTitle);
                writer.AddTag(HtmlTextWriterTag.Style, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Type, @"text/css"}
                });

                writer.RenderEndTag(); //HEAD

                writer.AddTag(HtmlTextWriterTag.Body);
                
                writer.RenderEndTag(); //HTML

                writer.AddTag("footer");
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

        public void SavePage(string path, string name = "index")
        {
            File.WriteAllText(path + @"\" + name + ".html", _page);
        }
    }
}
