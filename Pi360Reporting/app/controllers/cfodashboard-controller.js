

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CFODashboardController",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout', '$modal', 
            CFODashboardController]);

    function CFODashboardController($scope, $state, viewModelHelper, validator, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'cfodashboard-view';
        vm.viewName = 'Dashboard';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.barchart = [];
        vm.piechart = [];


        var rootUrl = '';

        vm.ceo2table1 = [
            { y: '2014', a: 50, b: 90 },
            { y: '2015', a: 65, b: 75 },
            { y: '2016', a: 50, b: 50 },
            //{ y: '2017', a: 75, b: 60 },
            //{ y: '2018', a: 80, b: 65 },
            //{ y: '2019', a: 90, b: 70 },
            //{ y: '2020', a: 100, b: 75 },
           // { y: '2021', a: 115, b: 75 },
            { y: '2022', a: 120, b: 85 },
            { y: '2023', a: 145, b: 85 },
            { y: '2024', a: 160, b: 95 }
        ];

        //vm.ceo2table2 = [
        //    { y: '2014', a: 50, b: 90},
        //    { y: '2015', a: 65, b: 75},
        //    { y: '2016', a: 50, b: 50},
        //    //{ y: '2017', a: 75, b: 60},
        //    //{ y: '2018', a: 80, b: 65},
        //    //{ y: '2019', a: 90, b: 70},
        //    { y: '2020', a: 100, b: 75},
        //    { y: '2021', a: 115, b: 75},
        //    { y: '2022', a: 120, b: 85},
        //    { y: '2023', a: 145, b: 85},
        //    { y: '2024', a: 160, b: 95}
        //];

        vm.ceo2table2 = [
            { y: '2014', a: 50, b: 90, c: 10 },
            { y: '2015', a: 65, b: 75, c: 90 },
            { y: '2016', a: 50, b: 50, c: 50 },
            //{ y: '2017', a: 75, b: 60, c: 40},
            //{ y: '2018', a: 80, b: 65, c: 20},
            //{ y: '2019', a: 90, b: 70, c: 50},
            //{ y: '2020', a: 100, b: 75, c: 40 },
           // { y: '2021', a: 115, b: 75, c: 60 },
            { y: '2022', a: 120, b: 85, c: 70 },
            { y: '2023', a: 145, b: 85, c: 20 },
            { y: '2024', a: 160, b: 95, c: 90 }
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

                //vm.open = open();
            }
        }



        var InitialView = function () {
            InitialGrid();
        }


       


        var ceo2colbarChartFunc3D = function () {
            Highcharts.chart('ceo2_1colbarchart', {
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
                //text: 'Title: 3D chart with null values'
                text: 'CASA'
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


        var ceo2pieChartFunc3D = function () {
            Highcharts.chart('ceo2_piechart', {
                chart: {
                    //renderTo: 'container',
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45,
                        beta: 0
                    }
                },
                title: {
                   // text: 'Browser market shares at a specific website, 2014'
                    text: 'Ratio Analysis'
                },
                tooltip: {
                    //format: '<b>{point.name}</b>: {point.y:.1f} Rs.',
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            //formatter: function() {
                            //    return Math.round(this.percentage*100)/100 + ' %';},
                            //format: '{point.name}'
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                            
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


        var ceo22colbarChartFunc3D = function () {
            //$('#container').highcharts({
            Highcharts.chart('ceo2_2colbarchart', {
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
                    //text: 'Total ..?, grouped by ? if grouped'
                    text: 'CASA'
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


        var InitialGrid1 = function () {
            setTimeout(function () {

                // data export
                if ($('#ceo2_1colbarchart_table').length > 0) {
                    var exportTable = $('#ceo2_1colbarchart_table').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        "scrollY": 22,
                        "scrollX": 50,
                        sDom: "T<'clearfix'>" +
                        "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                        "t" +
                        "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }


        vm.open = function () {
        //var open = function () {
            var modalInstance = $modal.open({
                //templateUrl: 'modalctrl-view.html',
                //templateUrl: '/app/assets/views/modalctrl-view.html',
                backdrop: true,
                templateUrl: rootUrl + 'app/views/modalctrl-view.html',
                controller: 'ModalCtrl as vm',
                windowClass: 'app-modal-window1'
            })
        };


        ceo2colbarChartFunc3D();
        InitialGrid1();
        ceo2pieChartFunc3D();
        ceo22colbarChartFunc3D();
        initialize();
    }
}());
