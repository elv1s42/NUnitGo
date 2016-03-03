
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111215-16-03-03-12-41-14-218').highcharts('StockChart', {       		
           	                chart: {
            		                type: 'spline'
        		                },        		
                            rangeSelector: {
                                buttons : [{
                                        type : 'day',
                                        count : 1,
                                        text : '1d'
                                    }, {
                                        type : 'week',
                                        count : 1,
                                        text : '1w'
                                    }, {
                                        type : 'month',
                                        count : 1,
                                        text : '1m'
                                    }, {
                                        type : 'all',
                                        count : 1,
                                        text : 'All'
                                    }],
                                    selected : 4
                            },
                            title: {
                                text: 'Test history'
                            },            
                            yAxis: {
                                title: {
                                    text: 'Test duration (seconds)'
                                }
                            },
                            legend: {
                                enabled: true,
                                align: 'bottom',
       		                 },
                           plotOptions: {
           		                 areaspline: {
              	                  fillOpacity: 0.5
            	                 }
        		                },
                            series: 
                            [{
            		            marker: {
                		                enabled: true,
                                        radius:  10,
                                        lineColor: '#CCEBE8',
                                        lineWidth: 3
           		                  },
                                point:{
                                    events:{
                                        click: function(){ var url = this.url; window.open(url,'_blank');}
                                    },
                                },
                                name: 'Test durtion',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 05), y: 0.4030403, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-32-05-379.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 10), y: 0.4020402, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-10-634.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 54), y: 0.4090409, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-54-833.html'},{ x: Date.UTC(2016, 03, 03, 12, 36, 06), y: 0.4020402, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-36-06-207.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 07), y: 0.4030403, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-07-376.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 45), y: 0.4010401, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-45-415.html'},{ x: Date.UTC(2016, 03, 03, 12, 41, 14), y: 0.4020402, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-41-14-218.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            {
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Test event 1',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 05), y: 0.2020202, text: 'Event duration: 00:00:00:202'},{ x: Date.UTC(2016, 03, 03, 12, 33, 10), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 33, 54), y: 0.2070207, text: 'Event duration: 00:00:00:207'},{ x: Date.UTC(2016, 03, 03, 12, 36, 06), y: 0.2020202, text: 'Event duration: 00:00:00:202'},{ x: Date.UTC(2016, 03, 03, 12, 40, 07), y: 0.2030203, text: 'Event duration: 00:00:00:203'},{ x: Date.UTC(2016, 03, 03, 12, 40, 45), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 41, 14), y: 0.2020202, text: 'Event duration: 00:00:00:202'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });