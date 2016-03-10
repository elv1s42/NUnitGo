using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.Utils;

namespace NUnitGoCore.CustomElements.NunitTestHtml
{
    public class NunitGoJsHighstock
    {
        private readonly DateTime _lastTestFinishDateTime;

        public string JsCode;

        public void SaveScript(string path)
        {
            var name = Output.GetTestHistoryScriptName(_lastTestFinishDateTime);
            var fullPath = Path.Combine(path, name);
            File.WriteAllText(fullPath, JsCode);
        }

        public NunitGoJsHighstock(List<NunitGoTest> nunitGoTests, string id)
        {
            var orderedList = nunitGoTests.OrderBy(x => x.DateTimeFinish);
            var lastTest = orderedList.Last();
            _lastTestFinishDateTime = lastTest.DateTimeFinish;
            
            var testsData = orderedList
                .Aggregate("", 
                (current, nunitGoTest) => current + string.Format(@"{{ x: Date.UTC({0}), y: {1}, marker:{{ fillColor: '{2}'}}, url: '{3}'}},", 
                    nunitGoTest.DateTimeFinish.ToString("yyyy, MM, dd, HH, mm, ss"), 
                    nunitGoTest.TestDuration.ToString(CultureInfo.InvariantCulture).Replace(",", "."), 
                    nunitGoTest.GetBackgroundColor(), Output.Files.GetTestHtmlName(nunitGoTest.DateTimeFinish)));
            var testsScreenshotsData = orderedList
                .Aggregate("", 
                (current1, nunitGoTest) => nunitGoTest
                    .Screenshots
                    .Aggregate(current1, 
                    (current, screenshot) => current + string.Format(@"{{ x: Date.UTC({0}), title: 'img', text: 'Screenshot'}},", 
                        screenshot.Date.ToString("yyyy, MM, dd, HH, mm, ss"))));
            var lastTestEvents = lastTest.Events;
            var allEvents = lastTestEvents
                .Select(testEvent => 
                    (from nunitGoTest in orderedList 
                     where nunitGoTest.Events.Any(x => x.Name.Equals(testEvent.Name)) 
                     select nunitGoTest.Events.First(x => x.Name.Equals(testEvent.Name))).ToList()).ToList();
            var testEventsData = "";
            foreach (var eventList in allEvents)
            {
                var eventData = eventList
                    .Aggregate("",
                    (current, testEvent) => current + string.Format(@"{{ x: Date.UTC({0}), y: {1}, text: '{2}'}},", 
                        testEvent.Finished.ToString("yyyy, MM, dd, HH, mm, ss"), 
                        testEvent.Duration.ToString(CultureInfo.InvariantCulture).Replace(",", "."),
                        "Event duration: " + testEvent.DurationString));
                testEventsData += string.Format(@"{{
                                marker: {{
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '{3}'
           		                }},
                                name: '{0}',
                                type: 'spline',
                                data: [{1}],
                                fillColor : '{2}',
                                color : '{2}'
                            }}, " + Environment.NewLine, eventList.First().Name, eventData, Colors.Black, Colors.TestBorderColor);
            }

            JsCode = string.Format(@"
                    $(function () {{
                        $('#{0}').highcharts('StockChart', {{       		
           	                chart: {{
            		                type: 'spline'
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
                                    text: 'Test duration (seconds)'
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
                                        radius:  10,
                                        lineColor: '{2}',
                                        lineWidth: 3
           		                  }},
                                point:{{
                                    events:{{
                                        click: function(){{ var url = this.url; window.open(url,'_blank');}}
                                    }},
                                }},
                                name: 'Test durtion',
                                type: 'spline',
                                data: [{1}],
                                id: 'dataseries',
                                tooltip: {{
                                    valueDecimals: 4
                                }},
                                fillColor : '{2}',
                                color : '{2}'
                            }}, {{
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{3}],
                                shape: 'flag',
                                width: 16,
                                fillColor : '{4}',
                                color : '{2}'
                            }},
                            {5}
                            ]
                        }});
                }});", id, testsData, Colors.TestBorderColor, testsScreenshotsData, Colors.BodyBackground, testEventsData);
        }
    }
}
