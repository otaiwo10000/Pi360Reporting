
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("Dashboard2Controller",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout', '$modal', 
            Dashboard2Controller]);

    function Dashboard2Controller($scope, $state, viewModelHelper, validator, Excel, $timeout, $modal) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'dashboard2-view';
        vm.viewName = 'Dashboard';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.barchart = [];
        vm.piechart = [];


        vm.cesbarchart = [
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


        var ceo2colbarChartFunc3D = function () {
            //$('#container').highcharts({
            Highcharts.chart('ceo_2colbarchart', {
                chart: {
                    type: 'column',
                    options3d: {
                        enabled: true,
                        alpha: 15,
                        beta: 15,
                        viewDistance: 25,
                        depth: 40
                    }
                },

                title: {
                    text: 'Total ..?, grouped by ? if grouped'
                },

                xAxis: {
                    categories: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '16px'
                        }
                    }
                },

                yAxis: {
                    allowDecimals: false,
                    min: 0,
                    title: {
                        text: 'Number of ?',
                        skew3d: true
                    }
                },

                tooltip: {
                    headerFormat: '<b>{point.key}</b><br>',
                    pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} / {point.stackTotal}'
                },

                plotOptions: {
                    column: {
                        stacking: 'normal',
                        depth: 40
                    }
                },

                series: [{
                    name: 'Team A',
                    data: [5, 3, 4, 7, 2],
                    stack: 0
                }, {
                    name: 'Team B',
                    data: [3, 4, 4, 2, 5],
                    stack: 1
                }, {
                    name: 'Team C',
                    data: [2, 5, 6, 2, 1],
                    stack: 2
                }, {
                    name: 'Team D',
                    data: [3, 0, 4, 4, 3],
                    stack: 3
                }]
            });
        };


        var ceo1colbarChartFunc3D = function () {
            Highcharts.chart('ceo_1colbarchart', {
        //Highcharts.chart('container', {
            chart: {
                type: 'column',
                options3d: {
                    enabled: true,
                    alpha: 10,
                    beta: 25,
                    depth: 70
                }
            },
            title: {
                text: 'Title: 3D chart with null values'
            },
            subtitle: {
                //text: 'Notice the difference between a 0 value and a null point'
                text: 'Subtitle here if needed.'
            },
            plotOptions: {
                column: {
                    depth: 25
                }
            },
            xAxis: {
                categories: Highcharts.getOptions().lang.shortMonths,
                labels: {
                    skew3d: true,
                    style: {
                        fontSize: '16px'
                    }
                }
            },
            yAxis: {
                title: {
                    text: null
                }
            },
            series: [{
                name: 'Sales',
                data: [2, 3, null, 4, 0, 5, 1, 4, 6, 3]
            }]
        });
    };


        var ceopieChartFunc3D = function () {
            Highcharts.chart('ceo_piechart', {
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    }
                },
                title: {
                    text: 'Browser market shares at a specific website, 2014'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Browser share',
                    data: [
                        ['Firefox', 45.0],
                        ['IE', 26.8],
                        {
                            name: 'Chrome',
                            y: 12.8,
                            sliced: true,
                            selected: true
                        },
                        ['Safari', 8.5],
                        ['Opera', 6.2],
                        ['Others', 0.7]
                    ]
                }]
            });
        };



        ceo2colbarChartFunc3D();
        ceopieChartFunc3D();
        ceo1colbarChartFunc3D();
    }
}());
