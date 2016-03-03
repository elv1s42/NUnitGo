
                    $(function () {
                        $('#history-11111111-1111-1111-1111-111111111116-16-03-03-12-40-00-415').highcharts('StockChart', {       		
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
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 59), y: 0.3010301, marker:{ fillColor: '#81d4fa'}, url: 'Test-16-03-03-12-31-59-054.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 03), y: 0.3040304, marker:{ fillColor: '#81d4fa'}, url: 'Test-16-03-03-12-33-03-322.html'},{ x: Date.UTC(2016, 03, 03, 12, 33, 48), y: 0.3020302, marker:{ fillColor: '#81d4fa'}, url: 'Test-16-03-03-12-33-48-025.html'},{ x: Date.UTC(2016, 03, 03, 12, 35, 59), y: 0.3020302, marker:{ fillColor: '#81d4fa'}, url: 'Test-16-03-03-12-35-59-410.html'},{ x: Date.UTC(2016, 03, 03, 12, 40, 00), y: 0.3110311, marker:{ fillColor: '#81d4fa'}, url: 'Test-16-03-03-12-40-00-415.html'},],
                                id: 'dataseries',
                                tooltip: {
                                    valueDecimals: 4
                                },
                                fillColor : '#CCEBE8',
                                color : '#CCEBE8'
                            }, {
                                name: 'Screenshots',
                                type: 'flags',
                                data: [{ x: Date.UTC(2016, 03, 03, 12, 31, 59), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 03), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 33, 48), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 35, 59), title: 'img', text: 'Screenshot'},{ x: Date.UTC(2016, 03, 03, 12, 40, 00), title: 'img', text: 'Screenshot'},],
                                shape: 'flag',
                                width: 16,
                                fillColor : '#D6FAF7',
                                color : '#CCEBE8'
                            },
                            
                            ]
                        });
                });