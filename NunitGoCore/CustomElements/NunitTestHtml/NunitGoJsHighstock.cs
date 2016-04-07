using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnitGoCore.Extensions;
using NUnitGoCore.NunitGoItems;
using NUnitGoCore.NunitGoItems.Remarks;
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

        public NunitGoJsHighstock(List<NunitGoTest> nunitGoTests, List<Remark> testRemarks, string chartId)
        {
            var orderedList = nunitGoTests.OrderBy(x => x.DateTimeFinish);
            var lastTest = orderedList.Last();
            _lastTestFinishDateTime = lastTest.DateTimeFinish;
            
            var testsData = orderedList
                .Aggregate("", 
                (current, nunitGoTest) => current +
                                          $@"{{ x: {nunitGoTest.DateTimeFinish.ToJsString()}, y: {nunitGoTest.TestDuration.ToJsString()}, marker:{{ fillColor: '{nunitGoTest
                                                      .GetBackgroundColor()}'}}, url: '{Output.Files.GetTestHtmlName(
                                                          nunitGoTest.DateTimeFinish)}'}},");
            var testsScreenshotsData = orderedList
                .Aggregate("", 
                (current1, nunitGoTest) => nunitGoTest
                    .Screenshots
                    .Aggregate(current1, 
                    (current, screenshot) => current +
                                             $@"{{ x: {screenshot.Date.ToJsString()}, title: 'img', text: 'Screenshot'}},"));
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
                    (current, testEvent) => current +
                                            $@"{{ x: {testEvent.Finished.ToJsString()}, y: {testEvent
                                                .Duration.ToJsString()}, text: '{"Event duration: " +
                                                                                                                             testEvent
                                                                                                                                 .DurationString}'}},");
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

            var testRemarksData = testRemarks.Aggregate("",
                (current, remark) =>
                    current +
                    $@"{{ x: {remark.RemarkDate.ToJsString()}, title: 'Test remark', text: '{remark
                        .RemarkMessage}'}},");

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
                            }}, {{
                                name: 'Remarks',
                                type: 'flags',
                                data: [{6}],
                                shape: 'flag',
                                fillColor : '{4}',
                                color : '{7}'
                            }},
                            {5}
                            ]
                        }});
                }});", chartId, testsData, Colors.TestBorderColor, testsScreenshotsData, Colors.BodyBackground, testEventsData, testRemarksData, Colors.Remarks);
        }
    }
}
