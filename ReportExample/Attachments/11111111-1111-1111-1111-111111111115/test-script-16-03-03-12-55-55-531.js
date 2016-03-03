
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111115-16-03-03-12-55-55-531').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 25), y: 0.3010301, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-49-25-473.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 32), y: 0.3030303, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-50-32-488.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 37), y: 0.3020302, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-51-37-819.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 17), y: 0.3040304, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-54-17-844.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 07), y: 0.3030303, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-55-07-005.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 35), y: 0.3030303, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-55-35-465.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 55), y: 0.3030303, marker:{ fillColor: '#81d4fa'}, url: 'test-16-03-03-12-55-55-531.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 25), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 32), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 37), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 17), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 07), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 35), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 55), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });