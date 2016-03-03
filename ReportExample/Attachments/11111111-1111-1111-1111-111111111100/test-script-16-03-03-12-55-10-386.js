
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111100-16-03-03-12-55-10-386').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 28), y: 0.7010701, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-49-28-563.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 35), y: 0.7020702, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-50-35-683.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 41), y: 0.7010701, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-51-41-021.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 21), y: 0.7060706, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-54-21-199.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 10), y: 0.7040704, marker:{ fillColor: '#ef5350'}, url: 'test-16-03-03-12-55-10-386.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 28), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 35), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 41), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 21), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 10), title: 'img', text: 'Screenshot'},],
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 28), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 50, 35), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 51, 40), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 54, 20), y: 0.50005, text: 'Event duration: 00:00:00:500'},{ x: Date.UTC(2016, 03, 03, 12, 55, 10), y: 0.5030503, text: 'Event duration: 00:00:00:503'},],
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 28), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 50, 35), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 51, 41), y: 0.2010201, text: 'Event duration: 00:00:00:201'},{ x: Date.UTC(2016, 03, 03, 12, 54, 21), y: 0.2050205, text: 'Event duration: 00:00:00:205'},{ x: Date.UTC(2016, 03, 03, 12, 55, 10), y: 0.2010201, text: 'Event duration: 00:00:00:201'},],
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 28), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 50, 35), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 51, 41), y: 0.0010001, text: 'Event duration: 00:00:00:001'},{ x: Date.UTC(2016, 03, 03, 12, 54, 21), y: 0.0020002, text: 'Event duration: 00:00:00:002'},{ x: Date.UTC(2016, 03, 03, 12, 55, 10), y: 0.0010001, text: 'Event duration: 00:00:00:001'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });