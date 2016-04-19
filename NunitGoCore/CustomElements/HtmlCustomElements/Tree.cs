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
		private static int _idSuiteCounter;
        private static string GetSuiteId()
		{
			_idSuiteCounter++;
			return "test-suite-" + _idSuiteCounter.ToString("D");
		}
        
        private static void BuildTreeFromSuites(this HtmlTextWriter writer, IEnumerable<NunitGoSuite> suites)
        {
            foreach (var suite in suites)
            {
                var tests = suite.Tests;
                var id = GetSuiteId();
                var allSuiteTests = suite.GetTests();
                var count = allSuiteTests.Count;
                var passedCount = allSuiteTests.Count(x => x.IsSuccess());
                var labelName = suite.Name + " (Tests: " + passedCount + @"/" + count + ")";
                writer.OpenTreeItem(labelName, id, "110%", suite.Tests.Count.Equals(0));
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                foreach (var nunitGoTest in tests)
                {
                    var testId = nunitGoTest.Guid.ToString();
                    var buttonText = nunitGoTest.Name
                                     + " (" + nunitGoTest.DateTimeStart.ToString("dd.MM.yy HH:mm:ss") + " - " +
                                     nunitGoTest.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss") + ")";
                    var openButton = new OpenButton(buttonText, nunitGoTest.TestHrefRelative,  nunitGoTest.GetColor());
                    writer
                        .Id(testId)
                        .Li(() => writer
                            .Title(nunitGoTest.Name)
                            .A(openButton.ButtonHtml)
                        );
                }
                if (suite.Suites.Any())
                {
                    writer.BuildTreeFromSuites(suite.Suites);
                }
                writer.RenderEndTag(); //UL
                writer.CloseTreeItem();
                //writer.RenderEndTag(); //LI
                //writer.RenderEndTag(); //UL
            }
        }

        public static string GetTreeCode(List<NunitGoTest> tests)
		{
			_idSuiteCounter = 0;
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
