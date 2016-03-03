
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111110-16-03-03-12-50-32-045').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 25), y: 1.8711871, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-49-25-087.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 32), y: 1.2341234, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-50-32-045.html'},],
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
                                name: 'Checking some stuff 1',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 23), y: 0.3640364, text: 'Event duration: 00:00:00:364'},{ x: Date.UTC(2016, 03, 03, 12, 50, 30), y: 0.0320032, text: 'Event duration: 00:00:00:032'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Checking some stuff 2',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 24), y: 0.9590959, text: 'Event duration: 00:00:00:959'},{ x: Date.UTC(2016, 03, 03, 12, 50, 31), y: 0.590059, text: 'Event duration: 00:00:00:590'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Checking some stuff 3',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 24), y: 0.7590759, text: 'Event duration: 00:00:00:759'},{ x: Date.UTC(2016, 03, 03, 12, 50, 31), y: 0.390039, text: 'Event duration: 00:00:00:390'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });