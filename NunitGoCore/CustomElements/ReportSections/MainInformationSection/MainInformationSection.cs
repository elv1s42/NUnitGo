using System.IO;
using System.Web.UI;
using NUnitGoCore.CustomElements.HtmlCustomElements;
using NUnitGoCore.Extensions;
using NUnitGoCore.Utils;
// ReSharper disable AccessToDisposedClosure

namespace NUnitGoCore.CustomElements.ReportSections.MainInformationSection
{
    internal class MainInformationSection : HtmlBaseElement
    {
        public string HtmlCode;
        
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

            }

            HtmlCode = strWr.ToString();
        }
    }
}
