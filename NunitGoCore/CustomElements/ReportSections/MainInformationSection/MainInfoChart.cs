using System.IO;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.ReportSections.MainInformationSection
{
    public class MainInfoChart
    {
        public string JsCode;

        public void SaveScript(string path)
        {
            var fullPath = Path.Combine(path, Output.Files.StatsScript);
            File.WriteAllText(fullPath, JsCode);
        }

        public MainInfoChart(MainStatistics stats, string id)
        {
            var data = "[" + $"{{name: 'Passed', y: {stats.TotalPassed}, color: '" + Colors.TestPassed + "'}," +
                       $"{{name: 'Failed', y: {stats.TotalFailed}, color: '" + Colors.TestFailed + "'}," +
                       $"{{name: 'Broken', y: {stats.TotalBroken}, color: '" + Colors.TestBroken + "'}," +
                       $"{{name: 'Ignored', y: {stats.TotalIgnored}, color: '" + Colors.TestIgnored + "'}," +
                       $"{{name: 'Inconclusive', y: {stats.TotalInconclusive}, color: '" + Colors.TestInconclusive +
                       "'}" + "]";
            
            JsCode =
                $@"
                    $(function () {{
                        $('#{id}').highcharts({{       		
           	                chart: {{
            		                type: 'pie'
        		                }},
                            title: {{
                                text: 'Test results'
                            }},
                            series: 
                            [{{
                                name: 'Results',
                                data: {data}
                            }}]
                        }});
                }});";
        }
    }
}
