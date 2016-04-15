using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NUnitGoCore.CustomElements.CSSElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements
{
    public class HtmlPage
    {
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
                        //new StyleAttribute(HtmlTextWriterStyle.FontFamily, "Tahoma,Verdana,Segoe,sans-serif");
                        new StyleAttribute(HtmlTextWriterStyle.FontFamily,
                            "\"Lucida Grande\", \"Lucida Sans Unicode\", Arial, Helvetica, sans-serif")

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

        public string FullPage { get; private set; }

        public string PageTitle;
        public string PageBodyCode = "";
        public string PageScriptString = "";
        public List<string> PageStylePaths = new List<string>();
        public List<string> ScriptFilePaths = new List<string>();

        //public HtmlPage(string pageTitle, string styleFullPath = "", string scriptString = "", string localScriptFilePath = "")
        public HtmlPage(string pageTitle)
        {
            PageTitle = pageTitle;
        }

        private void GeneratePageString()
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Html);
                writer
                    .WriteString("<!DOCTYPE html>")
                    .WriteString(Environment.NewLine)
                    .Tag(HtmlTextWriterTag.Head, () => writer
                        .Tag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                        {
                            {"http-equiv", "X-UA-Compatible"},
                            {"content", @"IE=edge"},
                            {"charset", "utf-8"}
                        })
                        .Tag(HtmlTextWriterTag.Title, PageTitle)
                        .WithAttr(HtmlTextWriterAttribute.Src, "http://code.jquery.com/jquery-1.11.0.min.js")
                        .Tag(HtmlTextWriterTag.Script)
                        .WithAttr(HtmlTextWriterAttribute.Src, "https://code.highcharts.com/stock/highstock.js")
                        .Tag(HtmlTextWriterTag.Script)
                        .TagIf(!PageScriptString.Equals(""), HtmlTextWriterTag.Script, PageScriptString)
                        .Scripts(ScriptFilePaths)
                        .WithAttr(HtmlTextWriterAttribute.Type, @"text/css")
                        .Tag(HtmlTextWriterTag.Style)
                        .Stylesheets(PageStylePaths)
                    )
                    .Tag(HtmlTextWriterTag.Body, PageBodyCode)
                    .WriteString(Environment.NewLine)
                    .Tag("footer")
                    .WriteString(Environment.NewLine)
                    .WriteString("</html>")
                    .Write(Environment.NewLine);
            }
            FullPage = strWr.ToString();
        }

        public string AddInsideTag(string tagName, string stringToAdd)
        {
            var lines = FullPage.SplitToLines().ToList();
            foreach (var line in lines.Where(line => line.Contains(@"</" + tagName + @">")))
            {
                lines.Insert(lines.IndexOf(line), stringToAdd);
                FullPage = string.Join(Environment.NewLine, lines);
                return FullPage;
            }
            return FullPage;
        }

        public string AddToBody(string stringToAdd)
        {
            return AddInsideTag("body", stringToAdd);
        }

        public string AddScriptText(string script)
        {
            return AddInsideTag("script", script);
        }

        public void AddScript(string scriptFile)
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, scriptFile);
                writer.AddTag(HtmlTextWriterTag.Script);
            }
            AddInsideTag("head", strWr.ToString());
        }

        public string AddToHead(string text = "")
        {
            return AddInsideTag("head", text);
        }

        public void SavePage(string fullpath)
        {
            GeneratePageString();
            File.WriteAllText(fullpath, FullPage);
        }
    }
}
