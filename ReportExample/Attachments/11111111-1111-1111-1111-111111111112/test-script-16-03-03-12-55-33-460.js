
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111112-16-03-03-12-55-33-460').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 23), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-49-23-132.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 30), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-50-30-665.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 35), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-51-35-846.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 15), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-54-15-422.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 04), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-04-900.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 33), y: 0.2030203, marker:{ fillColor: '#8bc34a'}, url: 'test-16-03-03-12-55-33-460.html'},],
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