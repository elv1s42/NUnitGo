using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using HtmlCustomElements.CSSElements;
using NunitResultAnalyzer;
using NunitResultAnalyzer.XmlClasses;
using Utils;
using Environment = System.Environment;

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

		private void BuildTree(HtmlTextWriter writer, IEnumerable<TestSuite> testSuites)
		{
			foreach (var suite in testSuites)
			{
				var id = GetSuiteId();
				var type = suite.Type;
				var name = suite.Name;
				var results = suite.Results;
				var currentTestCases = results.TestCases;
				var passedCountString = suite.CountPassed();

				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");

				if (type.Equals("Namespace") 
                    || type.Equals("Assembly") 
                    || type.Equals("Project")
                    || type.Equals("Unknown"))
					writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");

				writer.AddAttribute(HtmlTextWriterAttribute.Id, id);
				writer.RenderBeginTag(HtmlTextWriterTag.Input);
				writer.RenderEndTag(); //INPUT
				writer.AddAttribute(HtmlTextWriterAttribute.For, id);
				writer.AddAttribute(HtmlTextWriterAttribute.Title, name);
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "110%");
                writer.RenderBeginTag(HtmlTextWriterTag.Label);
                var start = suite.StartDateTime.Equals(new DateTime()) ? "" : suite.StartDateTime.ToString("dd.MM.yy HH:mm:ss");
                var end = suite.EndDateTime.Equals(new DateTime()) ? "" : suite.EndDateTime.ToString("dd.MM.yy HH:mm:ss");
				writer.Write(type + ": " + name + " " + passedCountString + " " + start + " - " + end);
				writer.RenderEndTag(); //LABEL
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				Log.Write("TestCases count = " + currentTestCases.Count);
				foreach (var currentTest in currentTestCases)
				{
					var testId = GetTestId();
					var testCase = currentTest;
					Log.Write("Generating tree: TestCase.Name = " + testCase.Name);
					var test = new NunitTest(testCase);
					var modalId = "modal-" + testId;
					var modalWindow = new ModalWindow(modalId, test.HtmlCode);
					var openButton = new JsOpenButton(testCase.Name.Split('.').Last()
						+ " " + testCase.StartDateTime.ToString("dd.MM.yy HH:mm:ss") + " - " +
						testCase.EndDateTime.ToString("dd.MM.yy HH:mm:ss"), 
						modalId, modalWindow.BackgroundId, test.BackgroundColor);

					writer.AddAttribute(HtmlTextWriterAttribute.Id, testId);
					writer.RenderBeginTag(HtmlTextWriterTag.Li);

					writer.AddAttribute(HtmlTextWriterAttribute.Title, testCase.Name);
					writer.RenderBeginTag(HtmlTextWriterTag.A);

					HtmlCodeModalWindows += Environment.NewLine + modalWindow.ModalWindowHtml;
					HtmlCodeModalWindows += Environment.NewLine + test.ModalWindowsHtml;

					writer.Write(openButton.ButtonHtml);

					writer.RenderEndTag(); //A
					writer.RenderEndTag(); //LI
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

				Log.Write("Building tree...");
				var list = new List<TestSuite> {results.TestSuite};
				Log.Write("List tree count = " + list.Count);
				BuildTree(writer, list);
				Log.Write("Building tree: done.");
				
				writer.RenderEndTag(); //DIV
			}

			HtmlCode = strWr.ToString();
		}
	}
}
