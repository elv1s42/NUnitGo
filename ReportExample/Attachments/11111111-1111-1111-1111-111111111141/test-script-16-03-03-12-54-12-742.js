
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111141-16-03-03-12-54-12-742').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 20), y: 0.120012, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-49-20-931.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 28), y: 0.1220122, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-50-28-000.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 23), y: 0.1210121, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-51-23-805.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 12), y: 0.1090109, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-54-12-742.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 20), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 28), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 23), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 12), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });