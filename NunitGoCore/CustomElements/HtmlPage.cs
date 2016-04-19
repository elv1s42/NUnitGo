using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.CSSElements;
using NUnitGoCore.CustomElements.ReportSections;
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
                    .NewLine()
                    .Tag(HtmlTextWriterTag.Head, () => writer
                        .Tag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                        {
                            {"http-equiv", "X-UA-Compatible"},
                            {"content", @"IE=edge"},
                            {"charset", "utf-8"}
                        })
                        .Title(PageTitle)
                        .Scripts(ScriptFilePaths)
                        .TagIf(!PageScriptString.Equals(""), HtmlTextWriterTag.Script, PageScriptString)
                        .Type(@"text/css")
                        .Tag(HtmlTextWriterTag.Style)
                        .Stylesheets(PageStylePaths)
                        
                    )
                    .Tag(HtmlTextWriterTag.Body, () => writer
                        .Class("border-bottom p-3 mb-3 bg-gray")
                        .Div(() => writer
                            .Class("container")
                            .Div(() => writer
                                .TextAlign("center")
                                .Div(() => writer
                                    .H1(PageTitle)
                                )
                            )    
                        )
                        .Class("container")
                        .Tag(HtmlTextWriterTag.Div, () => writer
                            .Write(PageBodyCode)
                        )
                    )
                    .NewLine()
                    .Footer(FooterSection.HtmlCode)
                    .NewLine()
                    .WriteString("</html>")
                    .NewLine();
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
