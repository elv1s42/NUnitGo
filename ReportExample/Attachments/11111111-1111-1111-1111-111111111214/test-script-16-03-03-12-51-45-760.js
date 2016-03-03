
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111214-16-03-03-12-51-45-760').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 32), y: 0.4110411, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-49-32-778.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 40), y: 0.420042, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-50-40-008.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 45), y: 0.4320432, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-51-45-760.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 32), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 40), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 45), title: 'img', text: 'Screenshot'},],
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 32), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 50, 39), y: 0.20002, text: 'Event duration: 00:00:00:200'},{ x: Date.UTC(2016, 03, 03, 12, 51, 45), y: 0.220022, text: 'Event duration: 00:00:00:220'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });