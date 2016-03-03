
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111110-16-03-03-12-41-05-412').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 57), y: 1.7691769, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-31-57-721.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 01), y: 1.490149, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-01-639.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 46), y: 1.1511151, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-33-46-364.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 57), y: 2.030203, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-35-57-775.html'},{ x: Date.UTC(2016, 03, 03, 12, 39, 58), y: 0.8980898, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-39-58-682.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 36), y: 1.7351735, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-40-36-714.html'},{ x: Date.UTC(2016, 03, 03, 12, 41, 05), y: 0.9910991, marker:{ fillColor: '#8bc34a'}, url: 'Test-16-03-03-12-41-05-412.html'},],
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
                            {
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Checking some stuff 1',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 56), y: 0.2450245, text: 'Event duration: 00:00:00:245'},{ x: Date.UTC(2016, 03, 03, 12, 33, 00), y: 0.2910291, text: 'Event duration: 00:00:00:291'},{ x: Date.UTC(2016, 03, 03, 12, 33, 45), y: 0.3840384, text: 'Event duration: 00:00:00:384'},{ x: Date.UTC(2016, 03, 03, 12, 35, 56), y: 0.4290429, text: 'Event duration: 00:00:00:429'},{ x: Date.UTC(2016, 03, 03, 12, 39, 57), y: 0.0320032, text: 'Event duration: 00:00:00:032'},{ x: Date.UTC(2016, 03, 03, 12, 40, 35), y: 0.2940294, text: 'Event duration: 00:00:00:294'},{ x: Date.UTC(2016, 03, 03, 12, 41, 04), y: 0.1090109, text: 'Event duration: 00:00:00:109'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Checking some stuff 2',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 57), y: 1.1141114, text: 'Event duration: 00:00:01:114'},{ x: Date.UTC(2016, 03, 03, 12, 33, 01), y: 1.1221122, text: 'Event duration: 00:00:01:122'},{ x: Date.UTC(2016, 03, 03, 12, 33, 45), y: 0.2060206, text: 'Event duration: 00:00:00:206'},{ x: Date.UTC(2016, 03, 03, 12, 35, 57), y: 0.9650965, text: 'Event duration: 00:00:00:965'},{ x: Date.UTC(2016, 03, 03, 12, 39, 58), y: 0.540054, text: 'Event duration: 00:00:00:540'},{ x: Date.UTC(2016, 03, 03, 12, 40, 36), y: 1.0721072, text: 'Event duration: 00:00:01:072'},{ x: Date.UTC(2016, 03, 03, 12, 41, 04), y: 0.4350435, text: 'Event duration: 00:00:00:435'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 
{
                                marker: {
                		                enabled: true,
                                        radius:  4,
                                        lineColor: '#CCEBE8'
           		                },
                                name: 'Checking some stuff 3',
                                type: 'spline',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 57), y: 0.9140914, text: 'Event duration: 00:00:00:914'},{ x: Date.UTC(2016, 03, 03, 12, 33, 01), y: 0.9220922, text: 'Event duration: 00:00:00:922'},{ x: Date.UTC(2016, 03, 03, 12, 33, 45), y: 0.0060006, text: 'Event duration: 00:00:00:006'},{ x: Date.UTC(2016, 03, 03, 12, 35, 57), y: 0.7650765, text: 'Event duration: 00:00:00:765'},{ x: Date.UTC(2016, 03, 03, 12, 39, 58), y: 0.340034, text: 'Event duration: 00:00:00:340'},{ x: Date.UTC(2016, 03, 03, 12, 40, 36), y: 0.8720872, text: 'Event duration: 00:00:00:872'},{ x: Date.UTC(2016, 03, 03, 12, 41, 04), y: 0.2350235, text: 'Event duration: 00:00:00:235'},],
                                fillColor : '#000000',
                                color : '#000000'
                            }, 

                            ]
                        });
                });