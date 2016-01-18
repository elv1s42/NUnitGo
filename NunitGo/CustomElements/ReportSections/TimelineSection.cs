using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NunitGo.CustomElements.HtmlCustomElements;
using NunitGo.Utils;

namespace NunitGo.CustomElements.ReportSections
{
    public class TimelineSection
    {
        public string HtmlCode;

        public TimelineSection(List<NunitGoTest> tests, string height = "90%")
        {
            var testResultsList = (from test in tests 
                                   let start = test.DateTimeStart.ToString("HH:mm:ss") 
                                   let finish = test.DateTimeFinish.ToString("HH:mm:ss") 
                                   let toolitipText = "Test: " + test.FullName + ", " + 
                                                       "Time: " + start + " - " + finish + ", " + 
                                                       Environment.NewLine + "Result: " + test.Result 
                                   let bcgColor = test.GetBackgroundColor() 
                                   select new HorizontalBarElement("", toolitipText, bcgColor, test.TestDuration, 
                                       test.TestHref)).ToList();
            var timelineBar = new HorizontalBar("timeline-bar", "", testResultsList, false);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height);
                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "scroll");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "1% 2% 3% 97%");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(new CloseButton("Back", Output.Outputs.FullReport).ButtonHtml);
                writer.RenderEndTag(); //DIV

                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Timeline (" + tests.First().DateTimeStart 
                    + "-" + tests.Last().DateTimeFinish + "):");
                writer.RenderEndTag();
                writer.Write(timelineBar.BarHtml);
                
                writer.RenderEndTag(); //DIV
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
