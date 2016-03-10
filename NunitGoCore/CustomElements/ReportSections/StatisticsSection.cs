using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.ReportSections
{
    internal class StatisticsSection
    {
        public string HtmlCode;

        public StatisticsSection(MainStatistics mainStats, string height = "90%")
        {
            var testResultsList = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("Passed", "Passed (" + mainStats.TotalPassed + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestPassed, 
                    mainStats.TotalPassed/(double)mainStats.TotalAll),
                new HorizontalBarElement("Failed", "Failed (" + mainStats.TotalFailed + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestFailed, 
                    mainStats.TotalFailed/(double)mainStats.TotalAll),
                new HorizontalBarElement("Broken", "Broken (" + mainStats.TotalBroken + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestBroken, 
                    mainStats.TotalBroken/(double)mainStats.TotalAll),
                new HorizontalBarElement("Ignored", "Ignored (" + mainStats.TotalIgnored + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestIgnored, 
                    mainStats.TotalIgnored/(double)mainStats.TotalAll),
                new HorizontalBarElement("Inconclusive", "Iconclusive (" + mainStats.TotalInconclusive + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestInconclusive, 
                    mainStats.TotalInconclusive/(double)mainStats.TotalAll),
                new HorizontalBarElement("Unknown", "Unknown (" + mainStats.TotalUnknown + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestUnknown, 
                    mainStats.TotalUnknown/(double)mainStats.TotalAll)
		    };
            var testResultsBar = new HorizontalBar("test-results-bar", "Test results bar", testResultsList);

            var testExecutedList = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("Executed", "Executed (" + mainStats.TotalExecuted + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestPassed, 
                    mainStats.TotalExecuted/(double)mainStats.TotalAll),
                new HorizontalBarElement("Not executed", "Not executed (" + 
                    (mainStats.TotalAll-mainStats.TotalExecuted).ToString("D") + @"/" + mainStats.TotalAll + ")", 
                    Colors.TestIgnored, 
                    (mainStats.TotalAll-mainStats.TotalExecuted)/(double)mainStats.TotalAll)
		    };
            var testExecutedBar = new HorizontalBar("test-success-bar", "Test success bar", testExecutedList);

            var testSuccessList = new List<HorizontalBarElement>
		    {
                new HorizontalBarElement("True", "True (" + mainStats.TotalSuccessTrue + @"/" + mainStats.TotalExecuted + ")", 
                    Colors.TestPassed, 
                    mainStats.TotalSuccessTrue/(double)mainStats.TotalExecuted),
                new HorizontalBarElement("False", "False (" + mainStats.TotalSuccessFalse + @"/" + mainStats.TotalExecuted + ")", 
                    Colors.TestFailed, 
                    mainStats.TotalSuccessFalse/(double)mainStats.TotalExecuted)
		    };
            var testSuccessBar = new HorizontalBar("test-success-bar", "Test success bar", testSuccessList);

            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(new CloseButton("Back", Output.Files.FullReportFile).ButtonHtml);
                writer.RenderEndTag(); //DIV

                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Test cases results:");
                writer.RenderEndTag();
                writer.Write(testResultsBar.BarHtml);

                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Test cases success:");
                writer.RenderEndTag();
                writer.Write(testSuccessBar.BarHtml);

                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Executed test cases:");
                writer.RenderEndTag();
                writer.Write(testExecutedBar.BarHtml);
                
                writer.RenderEndTag(); //DIV
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
