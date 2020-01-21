
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardRatiosController",
        ['$scope', '$state', 'viewModelHelper', '$rootScope', '$modal', 'validator', 'Excel', '$timeout',
            DashboardRatiosController]);

    function DashboardRatiosController($scope, $state, viewModelHelper, $rootScope, $modal, $modalInstance, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'Ratios-view';
        vm.viewName = 'Ratios Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //------ mix variables starts --------------------------
        vm.DataLiist = [];
        vm.DataLiist2 = [];
        vm.MaincaptionList = [];
        vm.AmountList = [];
        vm.AmountList = [];
        vm.PeriodList = [];
        vm.DList = [];
        vm.checkLoading = true;

        //........................................ ratios starts ...........................................................................................

        var ratiosFunc = function () {
            vm.DataLiist = [];
            vm.DataLiist2 = [];
            vm.MaincaptionList = [];
            vm.AmountList = [];
            vm.AmountList = [];
            vm.PeriodList = [];

            vm.DList = [];

            vm.MainCaption1 = ''; vm.Amount1 = ''; vm.MainCaption2 = ''; vm.Amount2 = ''; vm.MainCaption3 = ''; vm.Amount3 = '';
            vm.MainCaption4 = ''; vm.Amount4 = ''; vm.MainCaption5 = ''; vm.Amount5 = ''; vm.MainCaption6 = ''; vm.Amount6 = '';
            vm.MainCaption7 = ''; vm.Amount7 = ''; vm.MainCaption8 = ''; vm.Amount8 = ''; vm.MainCaption9 = ''; vm.Amount9 = '';

            //if (vm.init === false) {
            vm.viewModelHelper.apiGet('api/dashboard2/dashboard2ratio', null,
               function (res) {
                   vm.DList = res.data;

                   //vm.DList = [] ? vm.checkLoading = false : vm.checkLoading = true;
                   if (vm.DList.length !== 0 || vm.DList != []) { vm.checkLoading = false; }// else { vm.checkLoading = 'false'; }

                   angular.forEach(vm.DList, function (value, key) {

                       switch (value.MainCaption) {
                           case 'Deposits to Risk Assets ':
                               vm.Amount1 = value.Amount; vm.MainCaption1 = 'Deposits to Risk Assets';
                               break;

                           case 'Loan/Deposit Ratio':
                               vm.Amount2 = value.Amount; vm.MainCaption2 = 'Loan/Deposit Ratio';
                               break;

                           case 'Cost to Income':
                               vm.Amount3 = value.Amount; vm.MainCaption3 = 'Cost to Income';
                               break;

                           case 'MOBILE BANKING:TOTAL ACCOUNTS':
                               vm.Amount4 = value.Amount; vm.MainCaption4 = 'Mobile Banking to Total Accounts';
                               break;

                           case 'Return On Assets':
                               vm.Amount5 = value.Amount; vm.MainCaption5 = 'Return On Assets';
                               break;

                           case 'Return On Earnings':
                               vm.Amount6 = value.Amount; vm.MainCaption6 = 'Return On Earnings';
                               break;

                           case 'Accounts Dormancy Ratio':
                               vm.Amount7 = value.Amount; vm.MainCaption7 = 'Accounts Dormancy Ratio';
                               break;

                           case 'COST OF FUNDS':
                               vm.Amount8 = value.Amount; vm.MainCaption8 = 'Cost of Funds';
                               break;

                           case 'Funding Ratio':
                               vm.Amount9 = value.Amount; vm.MainCaption9 = 'Funding Ratio';
                       }

                   });
                   R1Func(); R2Func(); R3Func(); R4Func(); R5Func(); R6Func(); R7Func(); R8Func(); R9Func();
               },
               function (result) {
                   // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
               }, null);
            // }

        } //end of function

        //....................................... ratios1 chart functin starts ..........................................................

        var R1Func = function () {

            Highcharts.chart('ratios1', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption1
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    credits: {
                        enabled: false,  // to remove highcharts.com
                        //text: 'test.com',
                        //href: 'http//www.t.com'
                        //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                    },

                    //plotBands: [
                    //    {
                    //        from: 0,
                    //        to: 100,
                    //        color: '#55BF3B' // green
                    //    },
                    //    {
                    //        from: 120,
                    //        to: 160,
                    //        color: '#DDDF0D' // yellow
                    //    },
                    //    {
                    //        from: 160,
                    //        to: 200,
                    //        color: '#DF5353' // red
                    //    }
                    //]

                    //plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount1],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });//,
            //// Add some life
            //function (chart) {
            //    if (!chart.renderer.forExport) {
            //        setInterval(function () {
            //            var point = chart.series[0].points[0],
            //                newVal,
            //                inc = Math.round((Math.random() - 0.5) * 20);

            //            newVal = point.y + inc;
            //            if (newVal < 0 || newVal > 200) {
            //                newVal = point.y - inc;
            //            }

            //            point.update(newVal);

            //        }, 3000);
            //    }
            //});

        };

        //............................................. ratios1 ends.......................................................................................

        //............................................. ratios2  starts .........................................................

        var R2Func = function () {

            Highcharts.chart('ratios2', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption2
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount2],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratio2 ends ........................................................

        //............................................. ratios3  starts .........................................................

        var R3Func = function () {

            Highcharts.chart('ratios3', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption3
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount3],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios3 ends ........................................................

        //............................................. ratios4  starts .........................................................

        var R4Func = function () {

            Highcharts.chart('ratios4', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption4
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount4],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios4 ends ........................................................

        //............................................. ratios4  starts .........................................................

        var R5Func = function () {

            Highcharts.chart('ratios5', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption5
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount5],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios5 ends ........................................................

        //............................................. ratios6  starts .........................................................

        var R6Func = function () {

            Highcharts.chart('ratios6', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption6
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount6],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios6 ends ........................................................

        //............................................. ratios7  starts .........................................................

        var R7Func = function () {

            Highcharts.chart('ratios7', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption7
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount7],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios7 ends ........................................................

        //............................................. ratios8  starts .........................................................

        var R8Func = function () {

            Highcharts.chart('ratios8', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption8
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount8],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios8 ends ........................................................

        //............................................. ratios9  starts .........................................................

        var R9Func = function () {

            Highcharts.chart('ratios9', {
                chart: {
                    type: 'gauge',
                    plotBackgroundColor: null,
                    plotBackgroundImage: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },

                title: {
                    text: vm.MainCaption9
                },

                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    //position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
                },

                pane: {
                    startAngle: -150,
                    endAngle: 150,
                    background: [{
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#FFF'],
                                [1, '#333']
                            ]
                        },
                        borderWidth: 0,
                        outerRadius: '109%'
                    }, {
                        backgroundColor: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0, '#333'],
                                [1, '#FFF']
                            ]
                        },
                        borderWidth: 1,
                        outerRadius: '107%'
                    }, {
                        // default background
                    }, {
                        backgroundColor: '#DDD',
                        borderWidth: 0,
                        outerRadius: '105%',
                        innerRadius: '103%'
                    }]
                },

                //// the value axis
                yAxis: {
                    min: 0,
                    //max: 200,
                    max: 100,

                    //minorTickInterval: 'auto',
                    //minorTickWidth: 1,
                    //minorTickLength: 10,
                    //minorTickPosition: 'inside',
                    //minorTickColor: '#666',

                    tickPixelInterval: 30,
                    tickWidth: 2,
                    tickPosition: 'inside',
                    tickLength: 10,
                    tickColor: '#666',
                    labels: {
                        step: 2,
                        rotation: 'auto'
                    },
                    title: {
                        text: 'ratio'
                    },

                    plotBands: [{ from: 0, to: 100, color: '#55BF3B' }] // green
                },

                series: [{
                    name: 'Speed',
                    //data: [80],
                    data: [vm.Amount9],
                    tooltip: {
                        valueSuffix: ' ratio'
                    }
                }]

            });
        };

        //............................................. ratios4 ends ........................................................



        vm.open = function () {
            //$rootScope.globalopen = function () {

            var modalInstance = $modal.open({
                //templateUrl: 'modalctrl-view.html',
                //templateUrl: '/app/assets/views/modalctrl-view.html',
                backdrop: true,
                templateUrl: rootUrl + 'app/views/modalctrl-view.html',
                controller: 'ModalCtrl as vm',
                windowClass: 'app-modal-window1'
            })
        };



        vm.p = [];
        // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {
                if (value == 1) { vm.p.push("Jan"); }
                else if (value == 2) { vm.p.push("Feb"); }
                else if (value == 3) { vm.p.push("Mar"); }
                else if (value == 4) { vm.p.push("Apr") }
                else if (value == 5) { vm.p.push("May") }
                else if (value == 6) { vm.p.push("Jun") }
                else if (value == 7) { vm.p.push("Jul"); }
                else if (value == 8) { vm.p.push("Aug"); }
                else if (value == 9) { vm.p.push("Sep") }
                else if (value == 10) { vm.p.push("Oct") }
                else if (value == 11) { vm.p.push("Nov") }
                else if (value == 12) { vm.p.push("Dec") }
            });
        }

        $rootScope.$on("dashboardratiosGlobalFunc_ParentMethod", function () {
            ratiosFunc();
        })




        ratiosFunc();

    }
}());
