
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardController",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout', '$modal', 
            DashboardController]);

    function DashboardController($scope, $state, viewModelHelper, validator, Excel, $timeout, $modal) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
       
        vm.view = 'dashboard-view';
        vm.viewName = 'Dashboard';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.barchart = [];
        vm.piechart = [];


        vm.barchart = [
            { y: '2014', a: 50, b: 90 },
            { y: '2015', a: 65, b: 75 },
            { y: '2016', a: 50, b: 50 },
            { y: '2017', a: 75, b: 60 },
            { y: '2018', a: 80, b: 65 },
            { y: '2019', a: 90, b: 70 },
            { y: '2020', a: 100, b: 75 },
            { y: '2021', a: 115, b: 75 },
            { y: '2022', a: 120, b: 85 },
            { y: '2023', a: 145, b: 85 },
            { y: '2024', a: 160, b: 95 }
        ];

        vm.dashboard = [
            { y: '2014', a: 50, b: 90 },
            { y: '2015', a: 65, b: 75 },
            { y: '2016', a: 50, b: 50 },
            { y: '2017', a: 75, b: 60 },
            { y: '2018', a: 80, b: 65 },
            { y: '2019', a: 90, b: 70 },
            { y: '2020', a: 100, b: 75 },
            { y: '2021', a: 115, b: 75 },
            { y: '2022', a: 120, b: 85 },
            { y: '2023', a: 145, b: 85 },
            { y: '2024', a: 160, b: 95 }
        ];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {

                //$http.get("data.json")   //file name
                //    .success(function (response) {
                //        $scope.tasks = response.tasks;
                //    });  

                $scope.tasks = vm.tasks;

                open();
            }
        }

      

        var InitialView = function () {
            InitialGrid();
        }


        //var pieChartFunc3D = function () {
        //    Highcharts.chart('pieChart3D', {
        //        chart: {
        //            type: 'pie',
        //            options3d: {
        //                enabled: true,
        //                alpha: 45,
        //                beta: 0,
        //            }
        //        },
        //        plotOptions: {
        //            pie: {
        //                depth: 25
        //            }
        //        },
        //        series: [{
        //            data: [2, 4, 6, 1, 3]
        //        }]
        //    });
        //};


        var donutChartFunc3D = function () {
            Highcharts.chart('donut3Dchart', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45
                    }
                },
                title: {
                    text: 'Contents of Highsoft\'s weekly fruit delivery'
                },
                subtitle: {
                    text: '3D donut in Highcharts'
                },
                plotOptions: {
                    pie: {
                        innerSize: 100,
                        depth: 45
                    }
                },
                series: [{
                    name: 'Delivered amount',
                    data: [
                        ['Bananas', 8],
                        ['Kiwi', 3],
                        ['Mixed nuts', 1],
                        ['Oranges', 6],
                        ['Apples', 8],
                        ['Pears', 4],
                        ['Clementines', 4],
                        ['Reddish (bag)', 1],
                        ['Grapes (bunch)', 1]
                    ]
                }]
            });
        };

        var pieChartFunc3D = function () {
            //$('#container').highcharts({
            Highcharts.chart('pieChart3D', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0,
                    }
                },
                plotOptions: {
                    pie: {
                        depth: 25
                    }
                },
                series: [{
                    data: [2, 4, 6, 1, 3]
                }]
            });
        };


        var barChartFunc3D = function () {
            // $(function () {
            // var myChart = Highcharts.chart('container_barchart', {
            Highcharts.chart('3Dbarchart', {
                chart: {
                    type: 'column',
                    margin: 75,
                    options3d: {    //CONFIGURING THE 3D OPTIONS FOR A CHART
                        enabled: true,
                        alpha: 15,
                        beta: 15,
                        depth: 50
                        //viewDistance: ´numeric value´,
                    }
                },
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                ////supply the data driven for the bar chart
                //series: [{
                //    data: [29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
                //}]


                series: [{
                    data: [
                        { name: 'Team A', y: 29.9 },
                        { name: 'Team B', y: 71.5, },
                        { name: 'Team C', y: 106.4 },
                        { name: 'Team D', y: 129.2 },
                        { name: 'Team E', y: 144.0},
                        { name: 'Team F', y: 176.0 },
                        { name: 'Team G', y: 144.0 },
                        { name: 'Team H', y: 135.6 },
                        { name: 'Team I', y: 148.5 },
                        { name: 'Team J', y: 216.4 },
                        { name: 'Team K', y: 54.4 }
                    ]
                }]

                //series: [{
                //    data: [
                //        { name: 'Team A', color: 'lightblue', value: 'v1', y: 0 },
                //        { name: 'Team B', color: 'lightblue', value: 'v2', y: 4 },
                //        { name: 'Team C', color: 'lightblue', value: 'v3', y: 8 }
                //    ]
                //}]

                //series: [{
                //    data: [
                //        { name: 'point 1', color: 'blue', y: 0 },
                //        { name: 'point 2', color: 'red', y: 4 },
                //        { name: 'point 3', color: 'green', y: 8 }
                //    ]
                //}]


                //series: [{
                    //data: [
                    //    [0, 1],
                    //    [1, 2],
                    //    [2, 8]
                    //]
              
                    

                //    data: [{
                //        x: 'TeamA',
                //        y: 9
                //        //name: "Point2",
                //        //color: "#00FF00"
                //    }, {
                //        x: 'TeamB',
                //        y: 6
                //        //name: "Point1",
                //        //color: "#FF00FF"
                //    },
                //    {
                //        x: 'TeamC',
                //        y: 6
                //        //name: "Point1",
                //        //color: "#FF00FF"
                //    },
                //    {
                //        x: 'TeamD',
                //        y: 2
                //        //name: "Point1",
                //        //color: "#FF00FF"
                //    }]
                //}]


                //series: [{
                //    data: [
                //        { x: 'First', y: 29.9 },
                //        { x: 'Second', y: 71.5 },
                //        { x: 'Third', y: 106.4 }
                //    ]
                //}]

            });
            // });


        };


        var barChartFunc3DMultipleColumns = function () {  //3Dbarchartmultiplecolumns
            //$('#container').highcharts({
            Highcharts.chart('3Dbarchartmultiplecolumns', {

                zAxis: {
                    min: 0,
                    max: 3,
                    categories: [1, 2, 3],
                    labels: {
                        y: 5,
                        rotation: 18
                    }
                },

                chart: {
                    type: 'column',
                    margin: 75,
                    options3d: {
                        enabled: true,
                        alpha: 15,
                        beta: 15,
                        depth: 110,
                       // viewDistance: 5,
                        frame: {
                            bottom: {
                                size: 1
                                //color: 'rgba(0,0,0.05)'
                                //color: 'blue'
                            }
                        }
                    }
                },
                plotOptions: {  //plotOptions.column.groupZPadding: Spacing between columns on the z-axis.
                    column: {
                        depth: 200,
                        stacking: true,
                        grouping: false,
                        groupZPadding: 10
                    }
                },
                series: [{
                    data: [1, 2, 4, 3, 2, 4],   //for blue
                    stack: 0
                }, {
                    data: [5, 6, 3, 4, 1, 2],    //for black
                    stack: 1
                }, {
                    data: [7, 9, 8, 7, 5, 8],    //for green
                    stack: 2
                }]
            });
        };



        //var barChartFunc = function () {
        //   // $(function () {
        //       // var myChart = Highcharts.chart('container_barchart', {
        //        Highcharts.chart('container_barchart', {
        //            chart: {
        //                type: 'column'
        //            },
        //            title: {
        //                text: 'Weekly Target Metricsss'

        //            },
        //            xAxis: {
        //                categories: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
        //                //extends the x-axis beyond the default
        //                tickwidth: 0,
        //                max: 5,
        //                labels: {
        //                    formatter: function () {
        //                        if (!isFinite(String(this.value))) {
        //                            return this.value;
        //                        }
        //                    }
        //                }

        //            },
        //            yAxis: {
        //                min: 0,
        //                title: {
        //                    text: '% Daily Target'
        //                },
        //                label: {
        //                    enabled: false
        //                },
        //                gridLineWidth: 20
        //            },
        //            //supply the data driven for the bar chart
        //            series: [{
        //                name: 'Team A: LNC-TEM111',
        //                data: [1, 90, 4, 4, 80]
        //            }, {
        //                name: 'Team B: LNC-TEM222',
        //                data: [5, 7, 3, 4, 80]
        //            }, {
        //                name: 'Team C: LNC-TEM333',
        //                data: [15, 7, 20, 5, 30]
        //            }, {
        //                name: 'Team D: LNC-TEM444',
        //                data: [8, 17, 30, 10, 70]
        //            }, {
        //                name: 'Team E: LNC-TEM555',
        //                data: [35, 37, 3, 40, 4]
        //            }]
        //        });
        //   // });
        //};



        //var geoHighChartFunc = function () {

        //    Highcharts.chart('container-piechart', {
        //        chart: {
        //            type: 'pie'
        //        },
        //        title: {
        //            text: 'Browser market share, June, 2018'
        //        },
        //        //subtitle: {
        //        //    text: 'Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
        //        //},
        //        yAxis: {
        //            title: {
        //                text: 'Total percent market share'
        //            }
        //        },
        //        plotOptions: {
        //            pie: {
        //                shadow: false,
        //                center: ['50%', '50%']
        //            }
        //        },
        //        tooltip: {
        //            valueSuffix: '%'
        //        },
        //        series: [{
        //            name: 'Browsers',
        //            data: browserData,
        //            size: '60%',
        //            dataLabels: {
        //                formatter: function () {
        //                    return this.y > 5 ? this.point.name : null;
        //                },
        //                color: '#ffffff',
        //                distance: -30
        //            }
        //        }, {
        //            name: 'Versions',
        //            data: versionsData,
        //            size: '80%',
        //            innerSize: '60%',
        //            dataLabels: {
        //                formatter: function () {
        //                    // display only if larger than 1
        //                    return this.y > 1 ? '<b>' + this.point.name + ':</b> ' +
        //                        this.y + '%' : null;
        //                }
        //            },
        //            id: 'versions'
        //        }],
        //        responsive: {
        //            rules: [{
        //                condition: {
        //                    maxWidth: 400
        //                },
        //                chartOptions: {
        //                    series: [{
        //                        id: 'versions',
        //                        dataLabels: {
        //                            enabled: false
        //                        }
        //                    }]  //end series data
        //                }   //end series
        //            }]  //end the rules
        //        }
        //    }   //end second parameter of the chart
        //    );  //end chart funct

        //}


      
        //    var colors = Highcharts.getOptions().colors,
        //        categories = [
        //            "Chrome",
        //            "Firefox",
        //            "Internet Explorer",
        //            "Safari",
        //            "Edge",
        //            "Opera",
        //            "Other"
        //        ],
        //        data = [
        //            {
        //                "y": 62.74,
        //                "color": colors[2],
        //                "drilldown": {
        //                    "name": "Chrome",
        //                    "categories": [
        //                        "Chrome v65.0",
        //                        "Chrome v64.0",
        //                        "Chrome v63.0",
        //                        "Chrome v62.0",
        //                        "Chrome v61.0",
        //                        "Chrome v60.0",
        //                        "Chrome v59.0",
        //                        "Chrome v58.0",
        //                        "Chrome v57.0",
        //                        "Chrome v56.0",
        //                        "Chrome v55.0",
        //                        "Chrome v54.0",
        //                        "Chrome v51.0",
        //                        "Chrome v49.0",
        //                        "Chrome v48.0",
        //                        "Chrome v47.0",
        //                        "Chrome v43.0",
        //                        "Chrome v29.0"
        //                    ],
        //                    "data": [
        //                        0.1,
        //                        1.3,
        //                        53.02,
        //                        1.4,
        //                        0.88,
        //                        0.56,
        //                        0.45,
        //                        0.49,
        //                        0.32,
        //                        0.29,
        //                        0.79,
        //                        0.18,
        //                        0.13,
        //                        2.16,
        //                        0.13,
        //                        0.11,
        //                        0.17,
        //                        0.26
        //                    ]
        //                }
        //            },  //end of first data content in the array
        //            {
        //                "y": 10.57,
        //                "color": colors[1],
        //                "drilldown": {
        //                    "name": "Firefox",
        //                    "categories": [
        //                        "Firefox v58.0",
        //                        "Firefox v57.0",
        //                        "Firefox v56.0",
        //                        "Firefox v55.0",
        //                        "Firefox v54.0",
        //                        "Firefox v52.0",
        //                        "Firefox v51.0",
        //                        "Firefox v50.0",
        //                        "Firefox v48.0",
        //                        "Firefox v47.0"
        //                    ],
        //                    "data": [
        //                        1.02,
        //                        7.36,
        //                        0.35,
        //                        0.11,
        //                        0.1,
        //                        0.95,
        //                        0.15,
        //                        0.1,
        //                        0.31,
        //                        0.12
        //                    ]
        //                }
        //            },  //end of second data content in the array
        //            {
        //                "y": 7.23,
        //                "color": colors[0],
        //                "drilldown": {
        //                    "name": "Internet Explorer",
        //                    "categories": [
        //                        "Internet Explorer v11.0",
        //                        "Internet Explorer v10.0",
        //                        "Internet Explorer v9.0",
        //                        "Internet Explorer v8.0"
        //                    ],
        //                    "data": [
        //                        6.2,
        //                        0.29,
        //                        0.27,
        //                        0.47
        //                    ]
        //                }
        //            },      //end of third data content in the array

        //            {
        //                "y": 5.58,
        //                "color": colors[3],
        //                "drilldown": {
        //                    "name": "Safari",
        //                    "categories": [
        //                        "Safari v11.0",
        //                        "Safari v10.1",
        //                        "Safari v10.0",
        //                        "Safari v9.1",
        //                        "Safari v9.0",
        //                        "Safari v5.1"
        //                    ],
        //                    "data": [
        //                        3.39,
        //                        0.96,
        //                        0.36,
        //                        0.54,
        //                        0.13,
        //                        0.2
        //                    ]
        //                }
        //            },  //end of fourth data content in the array
        //            {
        //                "y": 4.02,
        //                "color": colors[5],
        //                "drilldown": {
        //                    "name": "Edge",
        //                    "categories": [
        //                        "Edge v16",
        //                        "Edge v15",
        //                        "Edge v14",
        //                        "Edge v13"
        //                    ],
        //                    "data": [
        //                        2.6,
        //                        0.92,
        //                        0.4,
        //                        0.1
        //                    ]
        //                }
        //            },  //end of fifth data content in the array
        //            {
        //                "y": 1.92,
        //                "color": colors[4],
        //                "drilldown": {
        //                    "name": "Opera",
        //                    "categories": [
        //                        "Opera v50.0",
        //                        "Opera v49.0",
        //                        "Opera v12.1"
        //                    ],
        //                    "data": [
        //                        0.96,
        //                        0.82,
        //                        0.14
        //                    ]
        //                }
        //            },  //end of sixth data content in the array
        //            {
        //                "y": 7.62,
        //                "color": colors[6],
        //                "drilldown": {
        //                    "name": 'Other',
        //                    "categories": [
        //                        'Other'
        //                    ],
        //                    "data": [
        //                        7.62
        //                    ]
        //                }
        //            }
        //        ],  //end of seventh data content in the array
        //        browserData = [],
        //        versionsData = [],
        //        i,
        //        j,
        //        dataLen = data.length,
        //        drillDataLen,
        //        brightness;


        //    // Build the data arrays
        //    for (i = 0; i < dataLen; i += 1) {

        //        // add browser data
        //        browserData.push({
        //            name: categories[i],
        //            y: data[i].y,
        //            color: data[i].color
        //        });

        //        // add version data
        //        drillDataLen = data[i].drilldown.data.length;
        //        for (j = 0; j < drillDataLen; j += 1) {
        //            brightness = 0.2 - (j / drillDataLen) / 5;
        //            versionsData.push({
        //                name: data[i].drilldown.categories[j],
        //                y: data[i].drilldown.data[j],
        //                color: Highcharts.Color(data[i].color).brighten(brightness).get()
        //            });
        //        }
        //    }   //end outer for loop

      

       


        //var morrisAreaFunc = function () {
        //    // vm.drawMorrisCharts2 = new Morris.Area({
        //    //var points = [];
        //    //points = results2;
        //    Morris.Area({
        //        element: 'flot-visitor',
        //        resize: true,
        //        data: vm.dashboard,
        //        xkey: 'y',
        //        ykeys: ['a', 'b'],
        //        labels: ['Item 1', 'Item 2'],
        //        lineColors: ['#a0d0e0', '#3c8dbc'],
        //        hideHover: 'auto'
        //    });
        //};

       

        //initialize();
       // barChartFunc();
       // geoHighChartFunc();
        //morrisAreaFunc();
        barChartFunc3D();
        barChartFunc3DMultipleColumns();
        pieChartFunc3D();
        donutChartFunc3D();
    }
}());
