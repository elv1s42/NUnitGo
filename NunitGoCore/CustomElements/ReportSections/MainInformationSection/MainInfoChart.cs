using System.IO;
using NunitGoCore.Utils;

namespace NunitGoCore.CustomElements.ReportSections.MainInformationSection
{
    public class MainInfoChart
    {
        public string JsCode;

        public void SaveScript(string path)
        {
            var fullPath = Path.Combine(path, Output.GetMainStatsScriptName());
            File.WriteAllText(fullPath, JsCode);
        }

        public MainInfoChart(MainStatistics stats, string id)
        {
            var data = string
                .Format("[" +
                        "{{name: 'Passed', y: {0}, color: '" + Colors.TestPassed + "'}}," +
                        "{{name: 'Failed', y: {1}, color: '" + Colors.TestFailed + "'}}," +
                        "{{name: 'Broken', y: {2}, color: '" + Colors.TestBroken + "'}}," +
                        "{{name: 'Ignored', y: {3}, color: '" + Colors.TestIgnored + "'}}," +
                        "{{name: 'Inconclusive', y: {4}, color: '" + Colors.TestInconclusive + "'}}" +
                        "]", stats.TotalPassed, stats.TotalFailed, stats.TotalBroken, stats.TotalIgnored,
                    stats.TotalInconclusive);
            
            JsCode = string.Format(@"
                    $(function () {{
                        $('#{0}').highcharts({{       		
           	                chart: {{
            		                type: 'pie'
        		                }},
                            title: {{
                                text: 'Test results'
                            }},
                            series: 
                            [{{
                                name: 'Results',
                                data: {1}
                            }}]
                        }});
                }});", id, data);
        }
    }
}
