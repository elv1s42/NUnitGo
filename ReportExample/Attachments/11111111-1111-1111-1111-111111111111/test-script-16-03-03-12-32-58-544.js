
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111111-16-03-03-12-32-58-544').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 54), y: 0.610061, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-31-54-713.html'},{ x: Date.UTC(2016, 03, 03, 12, 32, 58), y: 0.6110611, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-32-58-544.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 54), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 32, 58), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });