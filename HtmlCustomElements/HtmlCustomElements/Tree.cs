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
        private const string IdString = "#" + Id + " ";
        private static int _idSuiteCounter;
        private static int _idTestsCounter;

        public static string GetStyle()
        {
            var treeCssSet = new CssSet("tests-tree-style");
            treeCssSet.AddElement(new CssElement(IdString + "ul, " + IdString + "li")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute("list-style", "none")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "input")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute"),
					new StyleAttribute("opacity", "0")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "a")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "none")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "a:hover")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.TextDecoration, "underline")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "input + label + ul")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0 0 0 22px"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "label, " + IdString + "label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Cursor, "pointer")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "input:disabled + label")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Cursor, "default"),
					new StyleAttribute("opacity", ".6")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "input:checked:not(:disabled) + label + ul")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "label, " + IdString + "a, " + IdString + "label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "label")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("background-position", "18px 0")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("content", "\"\""),
					new StyleAttribute(HtmlTextWriterStyle.Width, "16px"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0 22px 0 0"),
					new StyleAttribute(HtmlTextWriterStyle.VerticalAlign, "middle"),
					new StyleAttribute("background-position", "0 -32px")
				}
            });
            treeCssSet.AddElement(new CssElement(IdString + "input:checked + label::before")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute("background-position", "0 -16px")
				}
            });
            return treeCssSet.ToString();
        }

        private static string GetSuiteId()
        {
            _idSuiteCounter++;
            return "test-suite-" + _idSuiteCounter.ToString("D");
        }

        private static string GetTestId()
        {
            _idTestsCounter++;
            return "test-case-" + _idTestsCounter.ToString("D");
        }

        private static void BuildTree(HtmlTextWriter writer, IEnumerable<TestSuite> testSuites)
        {
            foreach (var suite in testSuites)
            {
                var id = GetSuiteId();
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();//INPUT
                writer.AddAttribute(HtmlTextWriterAttribute.For, id);
                writer.AddAttribute(HtmlTextWriterAttribute.Title, suite.Name);
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                writer.Write(suite.Type + ": " + suite.Name);
                writer.RenderEndTag();//LABEL
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                var testCases = suite.Results.TestCases;
                if (testCases.Any())
                {
                    foreach (var testCase in testCases)
                    {
                        var testId = GetTestId();
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, testId);
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);

                        writer.AddAttribute(HtmlTextWriterAttribute.Title, testCase.Name);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);

                        var test = new NunitTest(testCase);
                        var modalId = "modal-" + testId;
                        var modalWindow = new ModalWindow("modal-" + testId, test.HtmlCode);
                        writer.Write(modalWindow.ModalWindowHtml);
                        var openButton = new JsOpenButton(testCase.Name, modalId);
                        writer.Write(openButton.ButtonHtml);

                        writer.RenderEndTag(); //A
                        writer.RenderEndTag(); //LI
                    }
                }
                if (suite.Results.TestSuites.Any())
                {
                    BuildTree(writer, suite.Results.TestSuites);
                }
                writer.RenderEndTag(); //UL
                writer.RenderEndTag(); //LI
                writer.RenderEndTag(); //UL
            }
        }

        public Tree(TestResults results)
        {
            _idSuiteCounter = 0;
            _idTestsCounter = 0;
            Style = GetStyle();

            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                BuildTree(writer, new List<TestSuite>{results.TestSuite});
                
                writer.RenderEndTag();//DIV
            }

            HtmlCode = strWr.ToString();
        }
    }
}
