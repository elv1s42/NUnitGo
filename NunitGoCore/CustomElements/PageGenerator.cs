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
		public static void GenerateTestPage(this NunitGoTest nunitGoTest, string fullPath, string testOutput = "", string chartFile = "")
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
				var page = new HtmlPage("Test page", "./../../" + Output.Files.ReportStyleFile, script);
				var htmlTest = new NunitTestHtml.NunitTestHtml(nunitGoTest, testOutput);
				page.AddScript(chartFile);
				page.AddToBody(htmlTest.HtmlCode);
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
				var page = new HtmlPage("Main statistics page");
				var reportMenuTitle = new PageTitle("Main statistics", "main-statistics");
				page.AddToBody(reportMenuTitle.HtmlCode);
				var statisticsSection = new StatisticsSection(stats);
				page.AddToBody(statisticsSection.HtmlCode);
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
				var page = new HtmlPage("Test list page");
				var reportMenuTitle = new PageTitle("Test list", "main-test-list");
				page.AddToBody(reportMenuTitle.HtmlCode);
				var testListSection = new TestListSection(tests);
				page.AddToBody(testListSection.HtmlCode);
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
				var page = new HtmlPage("Timeline page");
				var reportMenuTitle = new PageTitle("Tests timeline", "tests-timeline");
				page.AddToBody(reportMenuTitle.HtmlCode);
				var timeline = new TimelineSection(tests);
				page.AddToBody(timeline.HtmlCode);
				page.SavePage(fullPath);
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating timeline page");
			}
		}

		public static void GenerateStyleFile(string pathToSave)
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

				var cssPageName = Output.Files.ReportStyleFile;
				cssPage.SavePage(Path.Combine(pathToSave, cssPageName));
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating css style page");
			}
		}

		public static void GenerateReportMainPage(this List<NunitGoTest> tests, 
			string pathToSave, MainStatistics mainStats)
		{
			try
			{
				var report = new HtmlPage("NUnitGo Report", "", "", Output.Files.StatsScript);
				var mainTitle = new PageTitle();
				report.AddToBody(mainTitle.HtmlCode);
				var mainInformation = new MainInformationSection(mainStats);
				report.AddToBody(mainInformation.HtmlCode);
				var menuElements = new List<ReportMenuItem>
				{
					new ReportMenuItem("Main statistics", Output.Files.TestStatisticsFile),
					new ReportMenuItem("Test list", Output.Files.TestListFile),
					new ReportMenuItem("Timeline", Output.Files.TimelineFile)
				};
				var reportMenu = new MenuSection(menuElements, "main-menu", "Report menu");
				report.AddToBody(reportMenu.ReportMenuHtml);
				var footer = new FooterSection();
				report.AddInsideTag("footer", footer.HtmlCode);
				var reportPageName = Output.Files.FullReportFile;
				report.SavePage(Path.Combine(pathToSave, reportPageName));
			}
			catch (Exception ex)
			{
				Log.Exception(ex, "Exception while generating full report page");
			}
		}
	}
}