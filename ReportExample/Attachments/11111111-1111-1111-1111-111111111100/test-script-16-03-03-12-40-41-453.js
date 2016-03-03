
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111100-16-03-03-12-40-41-453').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 02), y: 0.7040704, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-32-02-150.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 06), y: 0.7010701, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-33-06-718.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 50), y: 0.7020702, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-33-50-783.html'},{ x: Date.UTC(2016, 03, 03, 12, 36, 02), y: 0.7010701, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-36-02-161.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 03), y: 0.7020702, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-40-03-450.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 41), y: 0.7020702, marker:{ fillColor: '#ef5350'}, url: 'Test-16-03-03-12-40-41-453.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 02), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 06), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 50), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 36, 02), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 40, 03), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 40, 41), title: 'img', text: 'Screenshot'},],
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
                                name: 'Checking something',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 01), y: 0.5010501, text: 'Event duration: 00:00:00:501'},{ x: Date.UTC(2016, 03, 03, 12, 33, 06), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 33, 50), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 36, 01), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 40, 03), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 40, 41), y: 0.50005, text: 'Event duration: 00:00:00:500'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Some operation time',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 02), y: 0.2020202, text: 'Event duration: 00:00:00:202'},{ x: Date.UTC(2016, 03, 03, 12, 33, 06), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 33, 50), y: 0.2020202, text: 'Event duration: 00:00:00:202'},{ x: Date.UTC(2016, 03, 03, 12, 36, 02), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 40, 03), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 40, 41), y: 0.2010201, text: 'Event duration: 00:00:00:201'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Suboperation time',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 32, 02), y: 0.0020002, text: 'Event duration: 00:00:00:002'},{ x: Date.UTC(2016, 03, 03, 12, 33, 06), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 33, 50), y: 0.0020002, text: 'Event duration: 00:00:00:002'},{ x: Date.UTC(2016, 03, 03, 12, 36, 02), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 40, 03), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 40, 41), y: 0.0010001, text: 'Event duration: 00:00:00:001'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });