
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111114-16-03-03-12-55-56-132').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 25), y: 0.3020302, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-49-25-959.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 33), y: 0.3020302, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-50-33-039.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 38), y: 0.3010301, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-51-38-365.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 18), y: 0.3020302, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-54-18-457.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 07), y: 0.310031, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-55-07-628.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 36), y: 0.3010301, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-55-36-042.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 56), y: 0.3080308, marker:{ fillColor: '#D6FAF7'}, url: 'test-16-03-03-12-55-56-132.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 25), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 33), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 38), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 18), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 07), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 36), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 56), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });