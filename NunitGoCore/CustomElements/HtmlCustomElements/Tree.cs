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
                                .Li(() => writer
                                    .Title(test.Name)
                                    .A(new OpenButton(test.Name
                                                      + " (" + test.DateTimeStart.ToString("dd.MM.yy HH:mm:ss") + " - " +
                                                      test.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss") + ")",
                                        test.TestHrefRelative, test.GetColor()).ButtonHtml)
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
