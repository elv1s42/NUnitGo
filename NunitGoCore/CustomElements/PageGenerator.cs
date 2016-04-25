using System;
using System.Collections.Generic;
using System.IO;
using NUnitGoCore.CustomElements.CSSElements;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.CustomElements.ReportSections;
using NUnitGoCore.CustomElements.ReportSections.MainInformationSection;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements
{
	public static class PageGenerator
	{
		public static void GenerateTestPage(this NunitGoTest nunitGoTest, string fullPath, string testOutput, string chartFile)
		{
			try
			{
				const string script = @"
					$(document).ready(function() {
						$("".tabs-menu a"").click(function(event) {
							event.preventDefault();
							$(this).parent().addClass(""current"");
							$(this).parent().siblings().removeClass(""current"");
							var tab = $(this).attr(""href"");
							$("".tab-content"").not(tab).css(""display"", ""none"");
							$(tab).fadeIn();
						});
					});
				";
                var htmlTest = new NunitTestHtml.NunitTestHtml(nunitGoTest, testOutput);
                var page = new HtmlPage("Test page")
                {
                    PageStylePaths = new List<string>
                    {
                        "./../../" + Output.Files.ReportStyleFile,
                        "./../../" + Output.Files.PrimerStyleFile
                    },
                    PageScriptString = script,
                    ScriptFilePaths = new List<string>
                    {
                        "./../../" + Output.Files.JQueryScriptFile,
                        Output.Files.HighstockScriptFile,
                        chartFile
                    },
                    PageBodyCode = htmlTest.HtmlCode
                };
                page.SavePage(fullPath);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating test html page");
			}
		}

		public static void GenerateMainStatisticsPage(this MainStatistics stats, string fullPath)
		{
			try
            {
                var reportMenuTitle = new SectionName("Main statistics");
                var statisticsSection = new StatisticsSection(stats);
                var page = new HtmlPage("Main statistics page")
                {
                    PageStylePaths = new List<string>
                    {
                        Output.Files.ReportStyleFile,
                        Output.Files.PrimerStyleFile
                    },
                    PageBodyCode = reportMenuTitle.HtmlCode + statisticsSection.HtmlCode
                };
				page.SavePage(fullPath);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating main statistics page");
			}
		}

		public static void GenerateTestListPage(this List<NunitGoTest> tests, string fullPath)
		{
			try
            {
                var reportMenuTitle = new SectionName("Test list");
                var testListSection = new TestListSection(tests);
                var page = new HtmlPage("Test list page")
                {
                    PageStylePaths = new List<string>
                    {
                        Output.Files.ReportStyleFile,
                        Output.Files.PrimerStyleFile,
                        Output.Files.OcticonStyleFile
                    },
                    PageBodyCode = reportMenuTitle.HtmlCode + testListSection.HtmlCode
                };
				page.SavePage(fullPath);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating test list page");
			}
		}

		public static void GenerateTimelinePage(this List<NunitGoTest> tests, string fullPath)
		{
			try
            {
                var reportMenuTitle = new SectionName("Tests timeline");
                var timeline = new TimelineSection(tests);
                var page = new HtmlPage("Timeline page")
                {
                    PageStylePaths = new List<string>
                    {
                        Output.Files.ReportStyleFile,
                        Output.Files.PrimerStyleFile
                    },
                    PageBodyCode = reportMenuTitle.HtmlCode + timeline.HtmlCode
                };
				page.SavePage(fullPath);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating timeline page");
			}
		}

		public static void GenerateReportMainPage(this List<NunitGoTest> tests, 
			string pathToSave, MainStatistics mainStats)
		{
			try
            {
                var menuElements = new List<ReportMenuItem>
                {
                    new ReportMenuItem("Main statistics", Output.Files.TestStatisticsFile , "octicon octicon-graph"),
                    new ReportMenuItem("Test list", Output.Files.TestListFile, "octicon octicon-checklist"),
                    new ReportMenuItem("Timeline", Output.Files.TimelineFile, "octicon octicon-clock")
                };
                var mainTitle = new SectionName("Test Run Report");
                var mainInformation = new MainInformationSection(mainStats);
                var reportMenu = new MenuSection(menuElements, "main-menu", "Report menu");
                var report = new HtmlPage("NUnitGo Report")
				{
				    ScriptFilePaths = new List<string>
                    {
                        Output.Files.JQueryScriptFile,
                        Output.Files.HighstockScriptFile,
                        Output.Files.StatsScript
                    },
                    PageStylePaths = new List<string>
                    {
                        Output.Files.ReportStyleFile,
                        Output.Files.PrimerStyleFile,
                        Output.Files.OcticonStyleFile
                    },
                    PageBodyCode = mainTitle.HtmlCode + mainInformation.HtmlCode + reportMenu.ReportMenuHtml
                };
				report.SavePage(Path.Combine(pathToSave, Output.Files.FullReportFile));
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating full report page");
			}
        }

        public static void GenerateStyleFile(string cssFullPath)
        {
            try
            {
                var cssPage = new CssPage();
                cssPage.AddStyles(new List<string>
                {
                    HtmlPage.StyleString,
                    Tooltip.StyleString,
                    HorizontalBar.StyleString,
                    Bullet.StyleString,
                    NunitTestHtml.NunitTestHtml.StyleString
                });
                cssPage.SavePage(cssFullPath);
            }
            catch (Exception ex)
            {
                Log.Exception(ex, "Exception while generating css style page");
            }
        }
    }
}