using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using NunitResultAnalyzer.XmlClasses;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class Timeline
    {
        public string HtmlCode;

        public Timeline(TestResults currentTestResults)
        {
            var testResultsList = new List<HorizontalBarElement>();
            var allTests = currentTestResults.TestSuite.Results.TestCases;
            foreach (var test in allTests)
            {
                var toolitipText = "Test: " + test.Name + ", time: "
                    + test.StartDateTime.ToString("HH:mm:ss") + " - " 
                    + test.EndDateTime.ToString("HH:mm:ss");
                var bcgColor = test.GetBackgroundColor();
                var horizontalTestElement = new HorizontalBarElement("", toolitipText, bcgColor, 
                    (test.EndDateTime - test.StartDateTime).TotalSeconds);
                testResultsList.Add(horizontalTestElement);
            }
            var timelineBar = new HorizontalBar("timeline-bar", "Timeline bar", testResultsList, false);

            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Timeline (" + currentTestResults.TestSuite.StartDateTime 
                    + "-" + currentTestResults.TestSuite.EndDateTime + "):");
                writer.RenderEndTag();
                writer.Write(timelineBar.BarHtml);
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
