using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using Utils;

namespace HtmlCustomElements.HtmlCustomElements
{
	public class Tree : HtmlBaseElement
	{
        public string HtmlCode;
        public string HtmlCodeModalWindows;
		public static string StyleString
		{
			get { return GetStyle(); }
		}
		
		private new const string Id = "tests-tree";
		private const string IdString = "#" + Id + " ";
		private static int _idSuiteCounter;

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

        private void BuildTree(HtmlTextWriter writer, List<NunitGoTest> tests)
        {
            var id = GetSuiteId();
            var count = tests.Count(x => x.IsSuccess());
            var passedCount = tests.Count(x => x.IsSuccess());
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); //INPUT
            writer.AddAttribute(HtmlTextWriterAttribute.For, id);
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "110%");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            var start = tests.First().DateTimeStart.ToString("dd.MM.yy HH:mm:ss");
            var end = tests.Last().DateTimeFinish.ToString("dd.MM.yy HH:mm:ss");
            writer.Write("All tests: " + passedCount + @"/" + count + " " + start + " - " + end);
            writer.RenderEndTag(); //LABEL
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            foreach (var currentTest in tests)
            {
                var testId = Ids.GetTestId(currentTest.Guid.ToString());
                var testCase = currentTest;
                var test = new NunitTest(testCase);
                var modalId = Ids.GetTestModalId(currentTest.Guid.ToString());
                var modalWindow = new ModalWindow(modalId, test.HtmlCode, 1004, 90);
                var openButton = new JsOpenButton(testCase.FullName
                    + " " + testCase.DateTimeStart.ToString("dd.MM.yy HH:mm:ss") + " - " +
                    testCase.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss"),
                    modalId, modalWindow.BackgroundId, test.BackgroundColor);

                writer.AddAttribute(HtmlTextWriterAttribute.Id, testId);
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute(HtmlTextWriterAttribute.Title, testCase.FullName);
                writer.RenderBeginTag(HtmlTextWriterTag.A);

                HtmlCodeModalWindows += Environment.NewLine + modalWindow.ModalWindowHtml;
                HtmlCodeModalWindows += Environment.NewLine + test.ModalWindowsHtml;

                writer.Write(openButton.ButtonHtml);
                writer.RenderEndTag(); //A
                writer.RenderEndTag(); //LI
            }
            writer.RenderEndTag(); //UL
            writer.RenderEndTag(); //LI
            writer.RenderEndTag(); //UL
        }

        public Tree(List<NunitGoTest> tests)
		{
			_idSuiteCounter = 0;
			Style = GetStyle();

			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
                BuildTree(writer, tests);
                writer.RenderEndTag(); //DIV
			}

			HtmlCode = strWr.ToString();
		}
	}
}
