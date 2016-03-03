
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111122-16-03-03-12-55-59-861').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 29), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-49-29-481.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 36), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-50-36-630.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 42), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-51-42-013.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 22), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-54-22-148.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 11), y: 0.2020202, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-11-356.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 39), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-39-917.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 59), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-59-861.html'},],
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
                            
                            ]
                        });
                });