
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111147-16-03-03-12-41-00-434').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 53), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-31-53-150.html'},{ x: Date.UTC(2016, 03, 03, 12, 32, 56), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-32-56-215.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 41), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-41-377.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 51), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-35-51-735.html'},{ x: Date.UTC(2016, 03, 03, 12, 39, 53), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-39-53-992.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 30), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-30-926.html'},{ x: Date.UTC(2016, 03, 03, 12, 41, 00), y: 0.10001, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-41-00-434.html'},],
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