
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111113-16-03-03-12-55-09-405').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 27), y: 0.9580958, marker:{ fillColor: '#ffc107'}, url: 'test-16-03-03-12-49-27-625.html'},{ x: Date.UTC(2016, 03, 03, 12, 50, 34), y: 0.9050905, marker:{ fillColor: '#ffc107'}, url: 'test-16-03-03-12-50-34-757.html'},{ x: Date.UTC(2016, 03, 03, 12, 51, 40), y: 0.9110911, marker:{ fillColor: '#ffc107'}, url: 'test-16-03-03-12-51-40-061.html'},{ x: Date.UTC(2016, 03, 03, 12, 54, 20), y: 0.9250925, marker:{ fillColor: '#ffc107'}, url: 'test-16-03-03-12-54-20-234.html'},{ x: Date.UTC(2016, 03, 03, 12, 55, 09), y: 0.9840984, marker:{ fillColor: '#ffc107'}, url: 'test-16-03-03-12-55-09-405.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 49, 27), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 49, 26), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 49, 27), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 34), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 33), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 50, 34), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 40), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 39), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 51, 39), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 20), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 19), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 54, 19), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 09), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 08), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 55, 08), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });