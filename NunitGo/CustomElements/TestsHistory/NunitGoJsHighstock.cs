using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NunitGo.NunitGoItems;
using NunitGo.Utils;

namespace NunitGo.CustomElements.TestsHistory
{
    public class NunitGoJsHighstock
    {
        private readonly DateTime _lastTestFinishDateTime;

        public string JsCode;

        public void SaveScript(string scriptCode, string path)
        {
            var name = Output.GetTestScriptName(_lastTestFinishDateTime);
            var fullPath = Path.Combine(path, name);
            File.WriteAllText(fullPath, scriptCode);
        }

        public NunitGoJsHighstock(List<NunitGoTest> nunitGoTests, string id)
        {
            var orderedList = nunitGoTests.OrderBy(x => x.DateTimeFinish);
            _lastTestFinishDateTime = orderedList.Last().DateTimeFinish;

            var testsData = "";
            foreach (var nunitGoTest in orderedList)
            {
                testsData += string.Format(@"{{ x: Date.UTC({0}), y: {1}, marker:{{ fillColor: '{2}' }}, url: '{3}'}},",
                    nunitGoTest.DateTimeFinish.ToString("yyyy, MM, dd, HH, mm, ss"),
                    nunitGoTest.TestDuration.ToString(CultureInfo.InvariantCulture).Replace(",", "."),
                    nunitGoTest.GetBackgroundColor(),
                    Output.Files.GetTestHtmlName(nunitGoTest.DateTimeFinish));
            }

            var testsStartedData = "";
            foreach (var nunitGoTest in orderedList)
            {
                testsStartedData += string.Format(@"{{ x: Date.UTC({0}), title: 'Test started', text: 'start time'}},",
                    nunitGoTest.DateTimeStart.ToString("yyyy, MM, dd, HH, mm, ss"));
            }

            var testsScreenshotsData = "";
            foreach (var nunitGoTest in orderedList)
            {
                foreach (var screenshot in nunitGoTest.Screenshots)
                {
                    testsScreenshotsData += string.Format(@"{{ x: Date.UTC({0}), title: 'img', text: 'Screenshot'}},",
                    screenshot.Date.ToString("yyyy, MM, dd, HH, mm, ss"));
                }
            }
            
            JsCode = string.Format(@"
                    $(function () {{
                        $('#{0}').highcharts('StockChart', {{
        		
           	                chart: {{
            		                type: 'areaspline'
        		                }},
        		
                            rangeSelector: {{
                                buttons : [{{
                                        type : 'day',
                                        count : 1,
                                        text : '1d'
                                    }}, {{
                                        type : 'week',
                                        count : 1,
                                        text : '1w'
                                    }}, {{
                                        type : 'month',
                                        count : 1,
                                        text : '1m'
                                    }}, {{
                                        type : 'all',
                                        count : 1,
                                        text : 'All'
                                    }}],
                                    selected : 4
                            }},
                            title: {{
                                text: 'Test history'
                            }},
            
                            yAxis: {{
                                title: {{
                                    text: 'Time (minutes)'
                                }}
                            }},
                            legend: {{
                                enabled: true,
                                align: 'bottom',
       		                 }},
                           plotOptions: {{
           		                 areaspline: {{
              	                  fillOpacity: 0.5
            	                 }}
        		                }},
                            series: 
                            [{{
            		                marker: {{
                		                enabled: true,
                                    radius:  10
           		                  }},
                                point:{{
                                    events:{{
                                        click: function(){{
                                            var url = this.url;
                                            window.open(url,'_blank');
                                        }}
                                    }},
                                }},
                                name: 'Test durtion',
                                type: 'area',
                                data: [{1}],
                                id: 'dataseries',
                                tooltip: {{
                                    valueDecimals: 4
                                }}
                            }}, {{
                                name: 'Test started',
                                type: 'flags',
                                data: [{2}],
                                onSeries: 'dataseries',
                                shape: 'flag',
                                width: 70,                
                                fillColor : Highcharts.getOptions().colors[3]
                            }}, {{
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{3}],
                                shape: 'flag',
                                width: 16,
                                fillColor : Highcharts.getOptions().colors[3]
                            }}]
                        }});
                }});", id, testsData, testsStartedData, testsScreenshotsData);
        }
    }
}
