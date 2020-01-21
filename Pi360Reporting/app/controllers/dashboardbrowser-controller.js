
//correct order of dependencies: ['$rootScope', '$scope', '$http', '$location', '$localStorage', 'loginService'

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardBrowserController",
        //['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout', '$stateParams',
            DashboardBrowserController]);
    

    function DashboardBrowserController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {

    
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'Browser-view';
        vm.viewName = 'Browser Analytics';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];



        var BrowserChartFunc = function () {

            Highcharts.chart('brs', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Browser market shares. January, 2018'
                },
                subtitle: {
                    text: 'Click the columns to view versions. Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Total percent market share'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.1f}%'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                series: [
                    {
                        name: "Browsers",
                        colorByPoint: true,
                        data: [
                            {
                                name: "Chrome",
                                y: 62.74,
                                drilldown: "Chrome"
                            },
                            {
                                name: "Firefox",
                                y: 10.57,
                                drilldown: "Firefox"
                            }
                        ]
                    }
                ],
                drilldown: {
                    series: [
                        {
                            name: "Chrome",
                            id: "Chrome",
                            data: [
                                [
                                    "v65.0",
                                    0.1
                                ],
                                [
                                    "v64.0",
                                    1.3
                                ],
                                [
                                    "v63.0",
                                    53.02
                                ]
                            ]
                        },
                        {
                            name: "Firefox",
                            id: "Firefox",
                            data: [
                                [
                                    "v58.0",
                                    1.02
                                ],
                                [
                                    "v57.0",
                                    7.36
                                ]
                            ]
                        }
                    ]
                }
            });
        };   //  end of function



        BrowserChartFunc();

    }
}());
