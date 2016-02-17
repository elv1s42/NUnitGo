﻿using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitGo.CustomElements.CSSElements;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.CustomElements.NunitTestHtml.NunitTestHtmlSections;
using NunitGo.Extensions;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.NunitTestHtml
{
	internal class NunitTestHtml : HtmlBaseElement
	{
		public string BackgroundColor;
		public string HtmlCode;
		public static string StyleString
		{
			get { return GetStyle(); }
		}
		
		private new const string Id = "testcase-element";
		
		public static string GetStyle()
		{
            var testCssSet = new CssSet("testcase-element");
            testCssSet.AddElement(new CssElement("#" + Id)
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "16px")
				}
            });
			testCssSet.AddElement(new CssElement("#" + Id + " b")
			{
				StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Margin, "0"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "0"),
					new StyleAttribute(HtmlTextWriterStyle.FontSize, "18px")
				}
            });
            testCssSet.AddElement(new CssElement(".test-window")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Overflow, "auto"),
					new StyleAttribute(HtmlTextWriterStyle.Top, "0%"),
					new StyleAttribute(HtmlTextWriterStyle.Position, "absolute")
				}
            });
            testCssSet.AddElement(new CssElement(".tabs-menu")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.ListStyleType, "none")
				}
            });
            testCssSet.AddElement(new CssElement(".tabs-menu li")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.TestBorderColor),
					new StyleAttribute(HtmlTextWriterStyle.Height, "30px"),
					new StyleAttribute("float", "left"),
					new StyleAttribute("margin-right", "10px")
				}
            });
            testCssSet.AddElement(new CssElement(".tabs-menu li.current")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White),
					new StyleAttribute(HtmlTextWriterStyle.ZIndex, "5")
				}
            });
            testCssSet.AddElement(new CssElement(".tabs-menu li a")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Color, "black"),
					new StyleAttribute(HtmlTextWriterStyle.Padding, "10px"),
					new StyleAttribute("text-decoration", "none")
				}
            });
            testCssSet.AddElement(new CssElement(".test-tab")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White),
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute("float", "left"),
					new StyleAttribute("margin-bottom", "20px")
				}
            });
            testCssSet.AddElement(new CssElement(".tab-content")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Width, "100%"),
					new StyleAttribute(HtmlTextWriterStyle.Display, "none")
				}
            });
            testCssSet.AddElement(new CssElement("#test-environment-href")
            {
                StyleFields = new List<StyleAttribute>
				{
					new StyleAttribute(HtmlTextWriterStyle.Display, "block")
				}
            });
			return testCssSet.ToString();
		}

		public static string GenerateTxtView(string txt)
		{
			var sWr = new StringWriter();
			using (var wr = new HtmlTextWriter(sWr))
			{
				wr.Css(HtmlTextWriterStyle.WhiteSpace, "pre-line")
                    .Tag(HtmlTextWriterTag.Div, txt);
			}
			return sWr.ToString();
		}

		public NunitTestHtml(NunitGoTest nunitGoTest, string testOutput = "")
		{
			Style = GetStyle();
			BackgroundColor = nunitGoTest.GetBackgroundColor();

			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Left, "0%");
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "test-window");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, Title);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

				writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);

			    writer.Css("float", "right")
			        .Tag(HtmlTextWriterTag.Div,
			            () => writer.Write(new CloseButton("Back", "./../../" + Output.Files.TestListFile).ButtonHtml))
			        .Css(HtmlTextWriterStyle.Display, "table")
			        .Css(HtmlTextWriterStyle.Width, "100%")
			        .Css(HtmlTextWriterStyle.BackgroundColor, Colors.BodyBackground)
			        .Tag(HtmlTextWriterTag.Table,
			            () => writer.Tag(HtmlTextWriterTag.Tr,
			                () => writer
			                    .Css(HtmlTextWriterStyle.Width, "50%")
			                    .Css(HtmlTextWriterStyle.BackgroundColor, Colors.White)
			                    .Tag(HtmlTextWriterTag.Td,
			                        () => writer.AddTestResult(nunitGoTest))
			                    .Css(HtmlTextWriterStyle.Width, "50%")
			                    .Css(HtmlTextWriterStyle.BackgroundColor, Colors.White)
			                    .Tag(HtmlTextWriterTag.Td,
			                        () => writer.AddTestHistory(nunitGoTest))
			                ))
                            .WithAttr(HtmlTextWriterAttribute.Id, "tabs-container")
                    .OpenTag(HtmlTextWriterTag.Div)
                        .WithAttr(HtmlTextWriterAttribute.Class, "tabs-menu")
                        .OpenTag(HtmlTextWriterTag.Ul)
                            .WithAttr(HtmlTextWriterAttribute.Class, "current")
                            .OpenTag(HtmlTextWriterTag.Li)
                                .WithAttr(HtmlTextWriterAttribute.Href, "#test-environment-href")
                                .Tag(HtmlTextWriterTag.A, "Test environment")
                            .CloseTag()
                            .OpenTag(HtmlTextWriterTag.Li)
                                .WithAttr(HtmlTextWriterAttribute.Href, "#test-failure-href")
                                .Tag(HtmlTextWriterTag.A, "Failure")
                            .CloseTag()
                            .OpenTag(HtmlTextWriterTag.Li)
                                .WithAttr(HtmlTextWriterAttribute.Href, "#test-screenshots-href")
                                .Tag(HtmlTextWriterTag.A, "Screenshots")
                            .CloseTag()
                            .OpenTag(HtmlTextWriterTag.Li)
                                .WithAttr(HtmlTextWriterAttribute.Href, "#test-output-href")
                                .Tag(HtmlTextWriterTag.A, "Output")
                            .CloseTag()
                        .CloseTag()
                        .WithAttr(HtmlTextWriterAttribute.Class, "test-tab")
                        .OpenTag(HtmlTextWriterTag.Div)
                            .WithAttr(HtmlTextWriterAttribute.Id, "test-environment-href")
                            .WithAttr(HtmlTextWriterAttribute.Class, "tab-content")
                            .OpenTag(HtmlTextWriterTag.Div)
                                .AddEnvironment()
                            .CloseTag()
                            .WithAttr(HtmlTextWriterAttribute.Id, "test-failure-href")
                            .WithAttr(HtmlTextWriterAttribute.Class, "tab-content")
                            .OpenTag(HtmlTextWriterTag.Div)
                                .AddFailure(nunitGoTest)
                            .CloseTag()
                            .WithAttr(HtmlTextWriterAttribute.Id, "test-screenshots-href")
                            .WithAttr(HtmlTextWriterAttribute.Class, "tab-content")
                            .OpenTag(HtmlTextWriterTag.Div)
                                .AddScreenshots(nunitGoTest)
                            .CloseTag()
                            .WithAttr(HtmlTextWriterAttribute.Id, "test-output-href")
                            .WithAttr(HtmlTextWriterAttribute.Class, "tab-content")
                            .OpenTag(HtmlTextWriterTag.Div)
                                .AddOutput(nunitGoTest, testOutput)
                            .CloseTag()
                        .CloseTag()
                    .CloseTag();

				writer.RenderEndTag(); //DIV
				writer.RenderEndTag(); //DIV

			}

			HtmlCode = strWr.ToString();
		}
	}
}
