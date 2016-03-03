
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111146-16-03-03-12-40-59-913').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 52), y: 0.1150115, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-31-52-822.html'},{ x: Date.UTC(2016, 03, 03, 12, 32, 55), y: 0.1140114, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-32-55-750.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 40), y: 0.1370137, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-40-907.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 51), y: 0.1320132, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-35-51-254.html'},{ x: Date.UTC(2016, 03, 03, 12, 39, 53), y: 0.1260126, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-39-53-505.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 30), y: 0.1210121, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-30-386.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 59), y: 0.1170117, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-59-913.html'},],
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