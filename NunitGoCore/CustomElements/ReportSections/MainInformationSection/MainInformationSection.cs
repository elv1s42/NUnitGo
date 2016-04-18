using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.ReportSections.MainInformationSection
{
    internal class MainInformationSection : HtmlBaseElement
    {
        public string HtmlCode;

        private new const string Id = "main-information";
        
        public MainInformationSection(MainStatistics stats)
        {
            var strWr = new StringWriter();
            using (var writer = new HtmlTextWriter(strWr))
            {
                writer
                    .Class("columns")
                    .Div(() => writer
                        .Class("one-third column")
                        .Div(() => writer
                            .Div(() => writer
                                .Class("border-bottom p-3 mb-3")
                                .H2("Time: ")
                                .Class("border border-0 p-3 mb-3")
                                .Div(() => writer
                                    .Ul(() => writer
                                        .Li("Start datetime: " + stats.StartDate)
                                        .Li("Finish datetime: " + stats.EndDate)
                                        .Li("Duration: " + stats.Duration)
                                    )
                                )
                            )
                            .Div(() => writer
                                .Class("border-bottom p-3 mb-3")
                                .H2("Summary: ")
                                .Class("border border-0 p-3 mb-3")
                                .Div(() => writer
                                    .Ul(() => writer
                                        .Li("Total: " + stats.TotalAll)
                                        .Li("Success: " + stats.TotalPassed)
                                        .Li("Errors: " + stats.TotalBroken)
                                        .Li("Failures: " + stats.TotalFailed)
                                        .Li("Inconclusive: " + stats.TotalInconclusive)
                                        .Li("Ignored: " + stats.TotalIgnored)
                                    )
                                )
                            )
                        )
                        .Class("two-thirds column")
                        .Div(() => writer
                            .WithAttr(HtmlTextWriterAttribute.Id, Output.GetStatsPieId())
                            .Tag(HtmlTextWriterTag.Div)
                        )
                    );
                /*

                writer.AddAttribute(HtmlTextWriterAttribute.Id, Id);
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-1");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Main information:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Start datetime: " + stats.StartDate);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Finish datetime: " + stats.EndDate);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Duration: " + stats.Duration);
                writer.RenderEndTag();
                writer.RenderEndTag();//Td
                
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-2");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.RenderBeginTag(HtmlTextWriterTag.H1);
                writer.Write("Main results:");
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Total: " + stats.TotalAll);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Success: " + stats.TotalPassed);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Errors: " + stats.TotalBroken);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Failures: " + stats.TotalFailed);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Inconclusive: " + stats.TotalInconclusive);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.Write(Bullet.HtmlCode + "Ignored: " + stats.TotalIgnored);
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "column-3");
                writer.Css(HtmlTextWriterStyle.Width, "50%")
                    .WithAttr(HtmlTextWriterAttribute.Id, Output.GetStatsPieId())
                    .Tag(HtmlTextWriterTag.Td);

                writer.RenderEndTag();//TR
                writer.RenderEndTag();//TABLE

                */

            }

            HtmlCode = strWr.ToString();
        }
    }
}
