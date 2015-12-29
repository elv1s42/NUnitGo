using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using NunitGo.Utils;

namespace NunitGo.HtmlCustomElements.HtmlCustomElements
{
    public class Timeline
    {
        public string HtmlCode;

        public Timeline(List<NunitGoTest> tests)
        {
            var testResultsList = (from test in tests 
                                   let start = test.DateTimeStart.ToString("HH:mm:ss") 
                                   let finish = test.DateTimeFinish.ToString("HH:mm:ss") 
                                   let toolitipText = "Test: " + test.FullName + ", " + 
                                                       "Time: " + start + " - " + finish + ", " + 
                                                       Environment.NewLine + "Result: " + test.Result 
                                   let bcgColor = test.GetBackgroundColor() 
                                   select new HorizontalBarElement("", toolitipText, bcgColor, test.TestDuration, 
                                       Ids.GetTestModalId(test.Guid.ToString()))).ToList();
            var timelineBar = new HorizontalBar("timeline-bar", "", testResultsList, false);
            var stringWriter = new StringWriter();
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "30px");
                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                writer.Write("Timeline (" + tests.First().DateTimeStart 
                    + "-" + tests.Last().DateTimeFinish + "):");
                writer.RenderEndTag();
                writer.Write(timelineBar.BarHtml);
            }
            HtmlCode = stringWriter.ToString();
        }
    }
}
