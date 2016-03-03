
                    $(function () {
                        $('#statistics-pie').highcharts({       		
           	                chart: {
            		                type: 'pie'
        		                },
                            title: {
                                text: 'Test results'
                            },
                            series: 
                            [{
                                name: 'Results',
                                data: [{name: 'Passed', y: 15, color: '#8bc34a'},{name: 'Failed', y: 5, color: '#ef5350'},{name: 'Broken', y: 1, color: '#ffc107'},{name: 'Ignored', y: 2, color: '#81d4fa'},{name: 'Inconclusive', y: 1, color: '#D6FAF7'}]
                            }]
                        });
                });