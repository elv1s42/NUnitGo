
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111112-16-03-03-12-40-34-724').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 55), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-31-55-804.html'},{ x: Date.UTC(2016, 03, 03, 12, 32, 59), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-32-59-869.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 44), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-44-896.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 55), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-35-55-456.html'},{ x: Date.UTC(2016, 03, 03, 12, 39, 57), y: 0.20002, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-39-57-486.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 34), y: 0.2010201, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-34-724.html'},],
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