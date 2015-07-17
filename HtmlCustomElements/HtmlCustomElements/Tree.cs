using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Tree : HtmlBaseElement
    {
        public string HtmlCode;
        public static string StyleString
        {
            get { return GetStyle(); }
        }

        private new const string Id = "tests-tree";
        private const string _id = "#" + Id + " ";

        public static string GetStyle()
        {
            var treeCssSet = new CssSet("tests-tree-style");
            treeCssSet.AddElement(new CssElement(_id + "ul, " + _id + "li")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute("list-style", "none")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "input")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute"),
					new StyleAttribute("opacity", "0")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "a")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "a:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "underline")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "input + label + ul")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0 0 0 22px"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "label, " + _id + "label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Cursor, "pointer")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "input:disabled + label")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Cursor, "default"),
					new StyleAttribute("opacity", ".6")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "input:checked:not(:disabled) + label + ul")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
            treeCssSet.AddElement(new CssElement(_id + "label, " + _id + "a, " + _id + "label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
            return treeCssSet.ToString();
        }

        public Tree(TestResults results)
        {
            Style = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                
                writer.RenderEndTag();//DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
