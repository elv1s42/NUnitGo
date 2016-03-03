
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111144-16-03-03-12-35-52-917').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 53), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-31-53-844.html'},{ x: Date.UTC(2016, 03, 03, 12, 32, 57), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-32-57-361.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 42), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-42-478.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 52), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-35-52-917.html'},],
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