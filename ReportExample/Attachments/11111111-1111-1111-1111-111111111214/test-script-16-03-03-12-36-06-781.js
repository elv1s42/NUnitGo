
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111214-16-03-03-12-36-06-781').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 05), y: 0.4220422, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-32-05-835.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 11), y: 0.4180418, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-33-11-204.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 55), y: 0.4090409, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-33-55-396.html'},{ x: Date.UTC(2016, 03, 03, 12, 36, 06), y: 0.4160416, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-36-06-781.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 05), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 11), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 55), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 36, 06), title: 'img', text: 'Screenshot'},],
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 05), y: 0.20002, text: 'Event duration: 00:00:00:200'},{ x: Date.UTC(2016, 03, 03, 12, 33, 11), y: 0.20002, text: 'Event duration: 00:00:00:200'},{ x: Date.UTC(2016, 03, 03, 12, 33, 55), y: 0.20002, text: 'Event duration: 00:00:00:200'},{ x: Date.UTC(2016, 03, 03, 12, 36, 06), y: 0.20002, text: 'Event duration: 00:00:00:200'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });