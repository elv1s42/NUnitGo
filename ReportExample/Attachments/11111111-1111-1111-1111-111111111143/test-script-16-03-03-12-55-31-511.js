
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111143-16-03-03-12-55-31-511').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 21), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-49-21-546.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 28), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-50-28-699.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 33), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-51-33-915.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 13), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-54-13-458.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 02), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-02-879.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 31), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-31-511.html'},],
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