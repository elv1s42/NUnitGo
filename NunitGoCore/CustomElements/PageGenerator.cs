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
                    PageStylePaths = new List<string>{ "./../../" + Output.Files.ReportStyleFile },
                    PageScriptString = script,
                    ScriptFilePaths = new List<string> { chartFile },
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
                var reportMenuTitle = new PageTitle("Main statistics", "main-statistics");
                var statisticsSection = new StatisticsSection(stats);
                var page = new HtmlPage("Main statistics page")
                {
                    PageStylePaths = new List<string> { Output.Files.ReportStyleFile, Output.Files.PrimerStyleFile },
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
                var reportMenuTitle = new PageTitle("Test list", "main-test-list");
                var testListSection = new TestListSection(tests);
                var page = new HtmlPage("Test list page")
                {
                    PageStylePaths = new List<string> { Output.Files.ReportStyleFile, Output.Files.PrimerStyleFile },
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
                var reportMenuTitle = new PageTitle("Tests timeline", "tests-timeline");
                var timeline = new TimelineSection(tests);
                var page = new HtmlPage("Timeline page")
                {
                    PageStylePaths = new List<string> { Output.Files.ReportStyleFile, Output.Files.PrimerStyleFile },
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
                    new ReportMenuItem("Main statistics", Output.Files.TestStatisticsFile),
                    new ReportMenuItem("Test list", Output.Files.TestListFile),
                    new ReportMenuItem("Timeline", Output.Files.TimelineFile)
                };
                var mainTitle = new PageTitle();
                var mainInformation = new MainInformationSection(mainStats);
                var reportMenu = new MenuSection(menuElements, "main-menu", "Report menu");
                var reportPageName = Output.Files.FullReportFile;
                var footer = new FooterSection();
                var report = new HtmlPage("NUnitGo Report")
				{
				    ScriptFilePaths = new List<string> { Output.Files.StatsScript },
                    PageStylePaths = new List<string> { Output.Files.ReportStyleFile, Output.Files.PrimerStyleFile },
                    PageBodyCode = mainTitle.HtmlCode + mainInformation.HtmlCode + reportMenu.ReportMenuHtml,
                    PageFooterCode = footer.HtmlCode
                };
				report.SavePage(Path.Combine(pathToSave, reportPageName));
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
                    PageTitle.StyleString,
                    HtmlPage.StyleString,
                    Tooltip.StyleString,
                    HorizontalBar.StyleString,
                    FooterSection.StyleString,
                    MainInformationSection.StyleString,
                    Bullet.StyleString,
                    HrefButtonBase.StyleString,
                    Tree.StyleString,
                    NunitTestHtml.NunitTestHtml.StyleString,
                    MenuSection.StyleString,
                    OpenButton.StyleString
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