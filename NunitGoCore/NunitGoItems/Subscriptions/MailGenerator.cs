using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;
using NUnitGoCore.CustomElements.NunitTestHtml;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems.Events;
using NUnitGoCore.Utils;

namespace NUnitGoCore.NunitGoItems.Subscriptions
{
    internal static class MailGenerator
    {
        public static List<Attachment> GetAttachmentsFromScreenshots(NunitGoTest nunitGoTest, string screenshotsPath)
        {
            return nunitGoTest.Screenshots.Select(
                screenshot =>
                    new Attachment(Path.Combine(screenshotsPath, screenshot.Name))
                    {
                        ContentId = screenshot.Name
                    })
                    .ToList();
        }

        public static string GetMailSubject(NunitGoTest nunitGoTest, bool isEventEmail = false, string eventName = "")
        {
            if (isEventEmail)
            {
                return string.Format("Test '{0}' has wrong event duration! Event '{1}'", 
                    nunitGoTest.Name, 
                    nunitGoTest.Events.First(x => x.Name.Equals(eventName)).Name);
            } 
            return nunitGoTest.IsSuccess() 
                ? string.Format("Test '{0}' was finished successfully", nunitGoTest.Name) 
                : (nunitGoTest.IsFailed() 
                ? string.Format("Test '{0}' was failed", nunitGoTest.Name) 
                : (nunitGoTest.IsBroken() 
                ? string.Format("Test '{0}' was broken", nunitGoTest.Name)
                : (nunitGoTest.IsIgnored()
                ? string.Format("Test '{0}' was ignored", nunitGoTest.Name)
                : (nunitGoTest.IsInconclusive()
                ? string.Format("Test '{0}' is inconclusive", nunitGoTest.Name)
                : string.Format("Test '{0}' was not successfully finished", nunitGoTest.Name)))));
        }

        public static string GetMailBody(NunitGoTest nunitGoTest, bool addLinks, 
            bool isEventEmail = false, string eventName = "", TestEvent previousRunEvent = null)
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer.Write("<!DOCTYPE html>");
                writer.Write(Environment.NewLine);
                writer.RenderBeginTag(HtmlTextWriterTag.Head);
                writer.Tag(HtmlTextWriterTag.Meta, new Dictionary<string, string>
                {
                    {"http-equiv", "X-UA-Compatible"},
                    {"content", @"IE=edge"},
                    {"charset", "utf-8"}
                });
                writer.Tag(HtmlTextWriterTag.Title, "NUnitGo Email");
                writer.Tag(HtmlTextWriterTag.Style, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Type, @"text/css"}
                });
                writer.Tag(HtmlTextWriterTag.Link, new Dictionary<HtmlTextWriterAttribute, string>
                {
                    {HtmlTextWriterAttribute.Rel, @"stylesheet"},
                    {HtmlTextWriterAttribute.Type, @"text/css"}
                });
                writer.RenderEndTag(); //HEAD

                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, Colors.White);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "10px");
                writer.AddStyleAttribute("border", "10px solid " + Colors.TestBorderColor);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Margin, "0px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Tahoma,Verdana,Segoe,sans-serif");
                writer.RenderBeginTag(HtmlTextWriterTag.Body);
                
                writer.AddStyleAttribute("box-sizing", "border-box");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0%");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "10px");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "test-window");
                writer.AddAttribute(HtmlTextWriterAttribute.Title, "Test");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "10px");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, nunitGoTest.Guid.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test full name: ");
                writer.Write(nunitGoTest.FullName);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test name: ");
                writer.Write(nunitGoTest.Name);
                writer.RenderEndTag(); //P

                if (isEventEmail && previousRunEvent != null)
                {
                    var currentEvent = nunitGoTest.Events.First(x => x.Name.Equals(eventName));
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Event name: ");
                    writer.Write(currentEvent.Name);
                    writer.RenderEndTag(); //P
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, string.Format("Current duration (event finished at {0}): ", currentEvent.Finished));
                    writer.Write(TimeSpan.FromSeconds(currentEvent.Duration).ToString(@"hh\:mm\:ss\:fff"));
                    writer.RenderEndTag(); //P
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, string.Format("Previous duration (event finished at {0}): ", previousRunEvent.Finished));
                    writer.Write(TimeSpan.FromSeconds(previousRunEvent.Duration).ToString(@"hh\:mm\:ss\:fff"));
                    writer.RenderEndTag(); //P
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Difference: ");
                    writer.Write(TimeSpan.FromSeconds(currentEvent.Duration - previousRunEvent.Duration).ToString(@"hh\:mm\:ss\:fff"));
                    writer.RenderEndTag(); //P
                }

                writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, nunitGoTest.GetBackgroundColor());
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.RenderBeginTag(HtmlTextWriterTag.B);
                writer.Write("Test result: ");
                writer.RenderEndTag(); //B
                writer.Write(nunitGoTest.Result);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Test duration: ");
                writer.Write(TimeSpan.FromSeconds(nunitGoTest.TestDuration).ToString(@"hh\:mm\:ss\:fff"));
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Time period: ");
                var start = nunitGoTest.DateTimeStart.ToString("dd.MM.yy HH:mm:ss.fff");
                var end = nunitGoTest.DateTimeFinish.ToString("dd.MM.yy HH:mm:ss.fff");
                writer.Write(start + " - " + end);
                writer.RenderEndTag(); //P

                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.AddTag(HtmlTextWriterTag.B, "Screenshots: ");
                writer.Write(nunitGoTest.Screenshots.Count);
                writer.RenderEndTag(); //P

                var screens = nunitGoTest.Screenshots.OrderBy(x => x.Date);
                foreach (var screenshot in screens)
                {
                    writer.Write("Screenshot (Date: " + screenshot.Date.ToString("dd.MM.yy HH:mm:ss.fff") + "):");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "inherited");
                    writer.AddAttribute(HtmlTextWriterAttribute.Src, @"cid:" + screenshot.Name);
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, screenshot.Name);
                    writer.RenderBeginTag(HtmlTextWriterTag.Img);
                    writer.RenderEndTag();//IMG
                    writer.RenderEndTag();//DIV
                }

                if (!nunitGoTest.IsSuccess())
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Stack trace: ");
                    writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestStackTrace));
                    writer.RenderEndTag(); //P
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Message: ");
                    writer.Write(NunitTestHtml.GenerateTxtView(nunitGoTest.TestMessage));
                    writer.RenderEndTag(); //P
                }

                if (addLinks)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    writer.AddTag(HtmlTextWriterTag.B, "Test page: ");
                    writer.AddStyleAttribute("background", Colors.OpenLogsButtonBackground);
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "black");
                    writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none !important");
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, nunitGoTest.TestHrefAbsolute);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(Environment.NewLine + "View on site");
                    writer.RenderEndTag(); //A
                    writer.RenderEndTag(); //P
                    
                }
                
                writer.RenderEndTag(); //DIV
                writer.RenderEndTag(); //DIV

                writer.RenderEndTag(); //BODY
                
                writer.Write(Environment.NewLine);
                writer.Write("</html>");
                writer.Write(Environment.NewLine);

            }

            return strWr.ToString();

        }
    }
}
