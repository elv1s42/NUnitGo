
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111111-16-03-03-12-51-35-379').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 22), y: 0.6030603, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-49-22-750.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 30), y: 0.6040604, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-50-30-221.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 35), y: 0.6060606, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-51-35-379.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 22), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 30), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 35), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });