using System;
using System.Collections.Generic;
using System.IO;
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
        public string PageFooterCode = "";
        public string PageScriptString = "";
        public List<string> PageStylePaths = new List<string>();
        public List<string> ScriptFilePaths = new List<string>();
        
        public HtmlPage(string pageTitle)
        {
            PageTitle = pageTitle;
        }

        private void GeneratePageString()
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
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
                    .Tag("footer", PageFooterCode)
                    .WriteString(Environment.NewLine)
                    .WriteString("</html>")
                    .Write(Environment.NewLine);
            }
            FullPage = strWr.ToString();
        }
        
        public void SavePage(string fullpath)
        {
            GeneratePageString();
            File.WriteAllText(fullpath, FullPage);
        }
    }
}
