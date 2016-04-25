using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.HtmlCustomElements
{
	internal static class Tree
	{
        private static void BuildTreeFromSuites(this HtmlTextWriter writer, IEnumerable<NunitGoSuite> suites)
        {
            foreach (var suite in suites)
            {
                var tests = suite.Tests;
                var allSuiteTests = suite.GetTests();
                var count = allSuiteTests.Count;
                var passedCount = allSuiteTests.Count(x => x.IsSuccess());
                var suiteName = suite.Name + " (Tests: " + passedCount + @"/" + count + ")";

                writer
                    .TreeItem(suiteName, () => writer
                        .Css(HtmlTextWriterStyle.PaddingLeft, "1em")
                        .Ul(() => writer
                            .ForEach(tests, test => writer
                                .Id(test.Guid.ToString())
                                .Class("border-bottom")
                                .Li(() => writer
                                    .Text("Test: ")
                                    .TooltippedSpan("Test result: " + Environment.NewLine + test.Result, () => writer
                                            .Class("octicon octicon-primitive-square")
                                            .Color(test.GetColor())
                                            .Span()
                                        )
                                    .Text(" ")
                                    .Href(test.TestHrefRelative)
                                        .A(() => writer
                                            .TooltippedSpan("View test page", () => writer
                                                .Class("octicon octicon-eye")
                                                .Color("black")
                                                .Span()
                                            )
                                        )
                                    .Span("  " + test.Name + " (" + test.DateTimeStart.ToString("dd.MM.yy HH:mm:ss") + " - " +
                                        test.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss") + ")")
                                    .Float("right")
                                    .Class("border-left border-right px-3")
                                    .BackgroundColor(test.GetColor())
                                    .Width("15%")
                                    .B(test.Result)
                                    .Float("right")
                                    .Class("border-left px-3")
                                    .B("Result: ")
                                )
                            )
                            .If(suite.Suites.Any(), () => writer
                                .BuildTreeFromSuites(suite.Suites)
                            )
                        )
                    );
            }
        }

        public static string GetTreeCode(List<NunitGoTest> tests)
		{
			var strWr = new StringWriter();
			using (var writer = new HtmlTextWriter(strWr))
			{
			    writer
			        .Id("tests-tree")
			        .Div(() => writer
			            .BuildTreeFromSuites(new List<NunitGoSuite>
			            {
			                tests.GetSuite("All tests")
			            })
			        );
			}
			return strWr.ToString();
		}
	}
}
