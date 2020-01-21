

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LandingPageController",
            ['$scope', '$state', 'viewModelHelper', '$modal', 'validator', '$rootScope', 'Excel', '$timeout',
                LandingPageController]);

    function LandingPageController($scope, $state, viewModelHelper, $modal, $modalInstance, validator, $rootScope, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'landingpage-view';
        vm.viewName = 'Landing Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        $scope.yaxistextcolor = 'white';
        $scope.yaxisvaluecolor = 'white';
        $scope.xaxistextcolor = 'white';
        $scope.xaxisvaluecolor = 'white';
        $scope.titlecolor = 'green';
        $scope.font1 = 'serif';
        $scope.fontweight = 'bold';

        $scope.stylesglobal = { fontFamily: 'serif', color: 'black', fontWeight: 'bold', fontSize: '100%' };  // for title
        $scope.xaxisstyleglobal = { fontSize: '120%', color: 'black', font: 'serif' };
        $scope.yaxisstyleglobal = { fontSize: '80%', color: 'black', font: 'serif' };
        $scope.legendstyleglobal = { fontSize: '60%', color: 'black', font: 'serif' };
        $scope.piechartstyleglobal = { fontFamily: 'sans-serif', fontSize: '70%', color: 'black' }
        $scope.yaxistextmarginglobal = 30;
        //$scope.yaxistitleformatglobal = { text: 'Amount', style: { color: 'white', font: 'serif' }, margin: 30 };
        //$scope.yaxislabelformatglobal = { style: { color: 'white', font: 'serif' } };

        //$scope.chartbackgroundcolorglobal = 'rgba(255, 255, 255, 0.0)';
        $scope.chartbackgroundcolorglobal = 'white';


        //------ mix variables starts --------------------------

        vm.DList = [];

        vm.D1ListMainCaption = [];
        vm.D1Listsubobj = [];
        vm.D2ListMainCaption = [];
        vm.D2Listsubobj = [];
        vm.D3ListMainCaption = [];
        vm.D3Listsubobj = [];

        vm.D1ASubcaptions = [];
        vm.D1AAmounts = [];
        vm.D2ASubcaptions = [];
        vm.D2AAmounts = [];
        vm.D3ASubcaptions = [];
        vm.D3AAmounts = [];

        vm.piechartSubCaptionAndAmount = [];
        vm.piechartSubCaptionAndAmount2 = [];
        vm.piechartSeriesdata = [];

        //------ mix variables ends --------------------------


        //------ mix variables starts --------------------------

        vm.TList = [];

        vm.T1ListMainCaption = [];
        vm.T1Listsubobj = [];
        vm.T2ListMainCaption = [];
        vm.T2Listsubobj = [];
        vm.T3ListMainCaption = [];
        vm.T3Listsubobj = [];

        vm.T1APeriods = [];
        vm.T1AAmounts = [];
        vm.T2APeriods = [];
        vm.T2AAmounts = [];
        vm.T3APeriods = [];
        vm.T3AAmounts = [];

        //------ mix variables ends --------------------------


        /////////////////////// Mix Function /////////////////////////////////////////////////

        var dFunc = function () {
            vm.viewModelHelper.apiGet('api/landingpage/mix', null,
                function (res) {
                    //vm.outputT1 = res.data;
                    vm.DList = res.data;


                    angular.forEach(vm.DList, function (a, b) {
                        angular.forEach(a.D1List, function (a2, b2) {
                            vm.D1ListMainCaption.push(a2.MainCaption);
                            vm.D1Listsubobj.push(a2.subobj);
                            //vm.D1Listsubobj.push({ subobj: a2.subobj });

                            angular.forEach(a2.subobj, function (c, d) {
                                vm.D1ASubcaptions.push(c.SubCaption);
                                vm.D1AAmounts.push(c.Amount);
                            });
                        });
                    });

                    D1Func();

                    //angular.forEach(vm.DList, function (a, b) {
                    //    angular.forEach(a.D2List, function (a2, b2) {
                    //        vm.D2ListMainCaption.push(a2.MainCaption);
                    //        vm.D2Listsubobj.push(a2.subobj);
                    //        //vm.D1Listsubobj.push({ subobj: a2.subobj });

                    //        angular.forEach(a2.subobj, function (c, d) {
                    //            vm.D2ASubcaptions.push(c.SubCaption);
                    //            vm.D2AAmounts.push(c.Amount);
                    //        });
                    //    });
                    //});

                    //=====  the particular part below is for pie chart =====================
                    angular.forEach(vm.DList, function (a, b) {
                        angular.forEach(a.D2List, function (a2, b2) {
                            vm.D2ListMainCaption.push(a2.MainCaption);
                            vm.D2Listsubobj.push(a2.subobj);

                            angular.forEach(a2.subobj, function (c, d) {
                                vm.piechartSubCaptionAndAmount.push([c.SubCaption, c.Amount]);

                                //vm.D2ASubcaptions.push(c.SubCaption);
                                //vm.D2AAmounts.push(c.Amount);
                            });
                        });
                    });
                    pieChartMix1Func();   // for pie chart


                    angular.forEach(vm.DList, function (a, b) {
                        angular.forEach(a.D3List, function (a2, b2) {
                            vm.D3ListMainCaption.push(a2.MainCaption);
                            vm.D3Listsubobj.push(a2.subobj);
                            //vm.D1Listsubobj.push({ subobj: a2.subobj });

                            angular.forEach(a2.subobj, function (c, d) {
                                vm.D3ASubcaptions.push(c.SubCaption);
                                vm.D3AAmounts.push(c.Amount);
                            });
                        });
                    });
                    D3Func();


                    //D2Func();
                   
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };


        /////////////////////////////// D1 Starts ///////////////////////////////////////////////////////////////////////////

        var D1Func = function () {
            //dFunc();

            Highcharts.chart('d1', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: $scope.stylesglobal
                },

                title: {
                    text: 'Total Deposit Mix',
                    //text: vm.D1ListMainCaption,
                    style: $scope.stylesglobal,
                },

                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                plotOptions: {
                    column: {
                        depth: 25
		
                    }
					
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    categories: vm.D1ASubcaptions,
                    labels: {
                        skew3d: true,
                        style: $scope.xaxisstyleglobal
                    }
                },
                yAxis: {
                    title: { text: 'Amount', style: $scope.yaxisstyleglobal, margin: 30 },
                    labels: { style: $scope.yaxisstyleglobal },

                    //title: { text: 'Amount', style: { color: 'white', font: 'serif' }, margin: 30 },
                    //labels: { style: { color: 'white'} },
                },

                legend: {
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    name: ' ',
                    //type: 'bar',    //line, column, bar, area
                    //visible: false,
                    //series maker symbol
                    maker: { enabled: true, symbol: null },
                    data: vm.D1AAmounts
                }]

                //exporting: {
                //    buttons: {
                //        contextButton: {
                //            menuItems: Highcharts.defaultOptions.exporting.buttons.contextButton.menuItems.slice(0, 0)
                //        }
                //    }
                //}

                //exporting: {
                //    buttons: {
                //        exportButton: {
                //            enabled: false
                //        },
                //        printButton: {
                //            enabled: false
                //        }
                //    }
                //}

            });
        };

        /////////////////////////////// D1 Ends ///////////////////////////////////////////////////////////////////////////


        /////////////////////////////// D3 Starts ///////////////////////////////////////////////////////////////////////////

        var D3Func = function () {
            //dFunc();

            Highcharts.chart('d3', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: $scope.stylesglobal
                },

                title: {
                    text: 'Total Revenue Mix',
                    //text: vm.D1ListMainCaption,
                    style: $scope.stylesglobal,
                },

                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    categories: vm.D3ASubcaptions,
                    labels: {
                        skew3d: true,
                        style: $scope.xaxisstyleglobal
                    }
                },
                yAxis: {
                    title: { text: 'Amount', style: $scope.yaxisstyleglobal, margin: 30 },
                    labels: { style: $scope.yaxisstyleglobal },

                    //title: { text: 'Amount', style: { color: 'white', font: 'serif' }, margin: 30 },
                    //labels: { style: { color: 'white'} },
                },

                legend: {
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    name: ' ',
                    //type: 'bar',    //line, column, bar, area
                    //visible: false,
                    //series maker symbol
                    maker: { enabled: true, symbol: null },
                    data: vm.D3AAmounts
                }]

            });
        };



        //var D3Func = function () {
        //    //dFunc();

        //    Highcharts.chart('d3', {
        //        chart: {
        //            type: 'column',
        //            //backgroundColor: 'dodgerblue',
        //            //backgroundColor: 'rgba(255, 255, 255, 0.0)',
        //            backgroundColor: 'navy',
        //            options3d: {
        //                enabled: true,
        //                alpha: 10,
        //                beta: 25,
        //                depth: 70
        //            },
        //            style: {
        //                fontFamily: 'serif',
        //                color: 'white',
        //                fontWeight: 'bold',
        //            }
        //        },

        //        title: {
        //            text: 'Total Revenue Mix',
        //            //text: vm.D3ListMainCaption,
        //            style: { fontSize: '120%', color: 'white' }
        //        },
        //        subtitle: {
        //            //text: 'Notice the difference between a 0 value and a null point'
        //        },
        //        credits: {
        //            enabled: false,  // to remove highcharts.com
        //            //text: 'test.com',
        //            //href: 'http//www.t.com'
        //            position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
        //        },
        //        plotOptions: {
        //            column: {
        //                depth: 25
        //            }
        //        },
        //        xAxis: {
        //            //categories: Highcharts.getOptions().lang.shortMonths,
        //            //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
        //            categories: vm.D3ASubcaptions,
        //            labels: {
        //                skew3d: true,
        //                style: {
        //                    fontSize: '120%', color: 'white'
        //                }                       
        //            }
        //        },
        //        yAxis: {
        //            title: {
        //                text: 'Amount',
        //                margin: 30
        //            }
        //        },

        //        legend: {
        //            enabled: false,
        //            //align: 'right',
        //            //verticalAlign: 'middle'
        //        },

        //        //series: [{
        //        //    //name: 'Sales',
        //        //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
        //        //}]

        //        series: [{
        //            name: ' ',
        //            //type: 'line',
        //            data: vm.D3AAmounts
        //        }]

        //    });
        //};

        /////////////////////////////// D3 Ends ///////////////////////////////////////////////////////////////////////////



        /////////////////////////////// T1 MainCaption Trend Function starts ///////////////////////////////////////////////////////////////////////////

        vm.MTList_1 = [];
        vm.MT1List = [];
        vm.MT1ListMainCaption = [];
        vm.MT1MainCaption = "";
        vm.MT1ListPeriods = [];
        vm.MT1ListAmounts = [];
        vm.MT1MainCaption_var = '';

        var mt1Func = function () {
            vm.viewModelHelper.apiGet('api/landingpage/maincaptiontrend_t1', null,
                function (res) {

                    vm.MTList_1 = res.data;


                    angular.forEach(vm.MTList_1, function (a, b) {
                        //vm.D1ListMainCaption.push(a.MainCaption);
                        vm.MT1MainCaption_var = a.MainCaption;
                    });

                    angular.forEach(vm.MTList_1, function (a, b) {
                        vm.MT1ListPeriods.push(a.Period);
                        vm.MT1ListAmounts.push(a.Amount);
                        //vm.D1Listsubobj.push({ subobj: a2.subobj });
                    });
                    periodFunc(vm.MT1ListPeriods);

                    MT1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };


        var MT1ChartFunc = function () {

            Highcharts.chart('mt1', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: $scope.stylesglobal
                },

                title: {
                    text: 'Total Deposits Trend',
                    style: $scope.stylesglobal
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                plotOptions: {
                    column: {
                        depth: 25
                    },
                    //series: {
                    //    marker: { enabled: false }
                    // }
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    //categories: vm.MT1ListPeriods,
                    categories: vm.p,
                    labels: {
                        skew3d: true,
                        style: $scope.xaxisstyleglobal
                    }
                },
                yAxis: {
                    title: { text: 'Amount', style: $scope.yaxisstyleglobal, margin: 30 },
                    labels: { style: $scope.yaxisstyleglobal },
                },

                legend: {
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    name: ' ',
                    //type: 'bar',    //line, column, bar, area
                    //visible: false,
                    //series maker symbol
                    data: vm.MT1ListAmounts
                }]

            });
        };

        /////////////////////////////// T1 MainCaption Trend Function ends ///////////////////////////////////////////////////////////////////////////


        /////////////////////////////// T2 SubCaption Trend Function starts ///////////////////////////////////////////////////////////////////////////

        vm.MTList_t2 = [];
        vm.MT2List = [];
        vm.MT2ListMainCaption = [];
        vm.MT2MainCaption = "";
        vm.MT2ListPeriods = [];
        vm.MT2ListAmounts = [];
        vm.abpList = [];
        vm.abpList2 = [];
        vm.MT1MainCaption_var2 = "";

        var mt2Func = function () {
            vm.viewModelHelper.apiGet('api/landingpage/subcaptiontrend_t2', null,
                function (res) {

                    vm.MTList_t2 = res.data;


                    angular.forEach(vm.MTList_t2, function (value, key) {
                        vm.nANDperiod = value.abpList;

                        angular.forEach(vm.nANDperiod, function (a, b) {
                            if (vm.MT2ListPeriods.indexOf(a.Period) === -1) {  //get unique /distict values of a.period
                                vm.MT2ListPeriods.push(a.Period);
                            }
                        });
                    });
                    periodFunc(vm.MT2ListPeriods);

                    vm.ceo2_2 = [];
                    vm.ceo2_2b = [];
                    vm.Amounts = [];
                    angular.forEach(vm.MTList_t2, function (value, key) {
                        angular.forEach(value.abpList, function (a, b) { vm.Amounts.push(a.Amount); });

                        vm.ceo2_2 = { name: value.SubCaption, data: vm.Amounts, stack: value.stack };
                        // vm.ceo2_2.push({name: value.name, data: value.data, stack: value.stack});
                        //vm.ceo2_2.prototype.push.apply(name, data, stack);
                        vm.ceo2_2b.push(vm.ceo2_2);
                        vm.Amounts = [];
                    });


                    MT2ChartFunc();

                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };


        var MT2ChartFunc = function () {

            Highcharts.chart('mt2', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: $scope.stylesglobal
                },

                title: {
                    text: 'Total Accounts Trend',
                    style: $scope.stylesglobal
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                plotOptions: {
                    column: {
                        depth: 25
                    },
                    //series: {
                    //    marker: { enabled: false }
                    // }
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    //categories: vm.MT2ListPeriods,
                    //categories: vm.MT2ListPeriods,
                    categories: vm.p,
                    labels: {
                        skew3d: true,
                        style: $scope.xaxisstyleglobal
                    }
                },
                yAxis: {
                    title: { text: 'Amount', style: $scope.yaxisstyleglobal, margin: 30 },
                    labels: { style: $scope.yaxisstyleglobal },
                },

                legend: {
                    enabled: true,
                    align: 'right',
                    //verticalAlign: 'middle'
                    itemStyle: $scope.legendstyleglobal
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                //series: [{
                //    name: ' ',
                //    //type: 'bar',    //line, column, bar, area
                //    //visible: false,
                //    //series maker symbol
                //    data: vm.MT2ListAmounts
                //}]

                series: vm.ceo2_2b

            });
        };

        /////////////////////////////// T2 SubCaption Trend Function ends ///////////////////////////////////////////////////////////////////////////



        /////////////////////////////// T3 MainCaption Trend Function starts ///////////////////////////////////////////////////////////////////////////

        vm.MTList_t3 = [];
        vm.MT3List = [];
        vm.MT3ListMainCaption = [];
        vm.MT3MainCaption = "";
        vm.MT3ListPeriods = [];
        vm.MT3ListAmounts = [];
        vm.MT1MainCaption_var3 = "";


        var mt3Func = function () {
            vm.viewModelHelper.apiGet('api/landingpage/maincaptiontrend_t3', null,
                function (res) {

                    vm.MTList_t3 = res.data;


                    angular.forEach(vm.MTList_3, function (a, b) {
                        vm.MT1MainCaption_var3 = a.MainCaption;
                    });

                    //vm.MT3ListMainCaption = vm.MTList_t3.MainCaption;

                    angular.forEach(vm.MTList_t3, function (a, b) {
                        vm.MT3ListPeriods.push(a.Period);
                        vm.MT3ListAmounts.push(a.Amount);
                        //vm.D1Listsubobj.push({ subobj: a2.subobj });                        
                    });
                    periodFunc(vm.MT3ListPeriods);


                    MT3ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };


        var MT3ChartFunc = function () {

            Highcharts.chart('mt3', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: $scope.stylesglobal
                },
                title: {
                    text: 'Total Revenue Trend',
                    style: $scope.stylesglobal
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                plotOptions: {
                    column: {
                        depth: 25
                    },
                    //series: {
                    //    marker: { enabled: false }
                    // }
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    //categories: vm.MT3ListPeriods,
                    categories: vm.p,
                    labels: {
                        skew3d: true,
                        style: $scope.xaxisstyleglobal
                    }
                },
                yAxis: {
                    title: { text: 'Amount', style: $scope.yaxisstyleglobal, margin: 30 },
                    labels: { style: $scope.yaxisstyleglobal },
                },

                legend: {
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    name: ' ',
                    //type: 'bar',    //line, column, bar, area
                    //visible: false,
                    //series maker symbol
                    data: vm.MT3ListAmounts
                }]

            });
        };

        /////////////////////////////// T3 MainCaption Trend Function ends ///////////////////////////////////////////////////////////////////////////



        /////////////////////////////// Pie chart for Mix starts ///////////////////////////////////////////////////////////////////////////

        var pieChartMix1Func = function () {

            Highcharts.chart('pie_mix1', {
                chart: {
                    type: 'pie',
                    //backgroundColor: 'dodgerblue',
                    //backgroundColor: 'rgba(255, 255, 255, 0.0)',
                    backgroundColor: $scope.chartbackgroundcolorglobal,
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25
                        //depth: 70
                    },
                    style: $scope.stylesglobal
                },  //==== chart end =====

                colors: ['#90EE90', '#FF0000', '#FFBF00'],

                title: {
                    text: 'Total Accounts',
                    style: $scope.stylesglobal
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15 }  //aligning the .com text eg 'highcharts.com'  to the left
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                //plotOptions: {
                //    column: {
                //        depth: 25
                //    },
                //    //series: {
                //    //    marker: { enabled: false }
                //    // }
                //},
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            format: '{point.name}',
                            //color: 'white',
                            //style: { fontFamily: '\'Lato\', sans-serif', lineHeight: '18px', fontSize: '17px' }
                            //style: { fontFamily: 'sans-serif', fontSize: '70%', color: 'white' }
                            style: $scope.piechartstyleglobal
                        }
                    }
                },

                legend: {
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    type: 'pie',
                    //name: vm.D2ListMainCaption,
                    data: vm.piechartSubCaptionAndAmount
                }]

            });
        };


        /////////////////////////////// Pie chart for Mix ends ///////////////////////////////////////////////////////////////////////////   


        vm.p = [];
        // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {

                switch (value) {
                    case 1:
                        vm.p.push("Jan");
                        break;
                    case 2:
                        vm.p.push("Feb");
                        break;
                    case 3:
                        vm.p.push("Mar");
                        break;
                    case 4:
                        vm.p.push("Apr");
                        break;
                    case 5:
                        vm.p.push("May");
                        break;
                    case 6:
                        vm.p.push("Jun");
                        break;
                    case 7:
                        vm.p.push("Jul");
                        break;
                    case 8:
                        vm.p.push("Aug");
                        break;
                    case 9:
                        vm.p.push("Sep");
                        break;
                    case 10:
                        vm.p.push("Oct");
                        break;
                    case 11:
                        vm.p.push("Nov");
                        break;
                    case 12:
                        vm.p.push("Dec");
                }

                //if (value == 1) { vm.p.push("Jan"); }
                //else if (value == 2) { vm.p.push("Feb"); }
                //else if (value == 3) { vm.p.push("Mar"); }
                //else if (value == 4) { vm.p.push("Apr") }
                //else if (value == 5) { vm.p.push("May") }
                //else if (value == 6) { vm.p.push("Jun") }
                //else if (value == 7) { vm.p.push("Jul"); }
                //else if (value == 8) { vm.p.push("Aug"); }
                //else if (value == 9) { vm.p.push("Sep") }
                //else if (value == 10) { vm.p.push("Oct") }
                //else if (value == 11) { vm.p.push("Nov") }
                //else if (value == 12) { vm.p.push("Dec") }
            });
        };


        //var createGroupedArray = function (arr, chunkSize) {
        //    var groups = [], i;
        //    for (i = 0; i < arr.length; i += chunkSize) {
        //        groups.push(arr.slice(i, i + chunkSize));
        //    // vm.ceo2_2.push({name: value.name, data: value.data, stack: value.stack});
        //    }
        //    return groups;
        //}

        //vm.redColor = "panel panel-red";
        //vm.greenColor = "panel panel-green";
        //vm.yellowColor = "panel panel-yellow";
        //vm.subCaptionList = [];
        ////vm.subCaptionList2 = [];

        //var functionThatReturnsStyle = function () {
        //    vm.subCaptionList = [];
        //    var vari = '';

        //    vm.viewModelHelper.apiGet('api/landingpage/cards', null,
        //       function (res) {
        //           vm.varr = res.data;

        //           angular.forEach(vm.varr, function (value, key) {
        //               //if (value.amount < value.budget)  return redColor;
        //               if (value.CurrentMonth < value.Budget) {
        //                   vari = vm.redColor;
        //                   vm.subCaptionList.push({ SubCaption: value.SubCaption, CurrentMonth: value.CurrentMonth, Budget: value.Budget, colorVariable: vari });
        //               }

        //               else if (value.CurrentMonth > value.Budget) {
        //                   vari = vm.greenColor;
        //                   vm.subCaptionList.push({ SubCaption: value.SubCaption, CurrentMonth: value.CurrentMonth, Budget: value.Budget, colorVariable: vari });
        //               }

        //               else if (value.CurrentMonth === value.Budget) {
        //                   vari = vm.yellowColor;
        //                   vm.subCaptionList.push({ SubCaption: value.SubCaption, CurrentMonth: value.CurrentMonth, Budget: value.Budget, colorVariable: vari });
        //               }                      

        //               //return redColor;
        //               //else if (condition2)
        //               //    return brownColor;
        //               //else if (condition2)
        //               //    return blueColor;
        //               //else if (condition2)
        //               //    return greenColor;
        //           });  //end of foreach

        //           vm.subCaptionList.colorVariable;

        //           //vm.subCaptionList = createGroupedArray(vm.subCaptionList, 4);


        //       },
        //       function (result) {
        //           // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //       }, null);



        //    } //end of function


        //var colorAssigmentFunc = function(amount, budget) {

        //}


        ////................... for cards ..................................................................................................................................

        vm.checkdata = false;
        vm.CheckMC1 = false; vm.CheckMC2 = false; vm.CheckMC3 = false; vm.CheckMC4 = false; vm.CheckMC5 = false; vm.CheckMC6 = false;
        vm.maki = false;


        var cardFunc = function () {
            vm.DataCardList = [];
            //vm.DataCardsLiist2 = [];
            vm.MaincaptionCardList = [];
            vm.AmountCardList = [];
            vm.PeriodCardList = [];
            vm.checkdata = false;
            vm.valu = 'CARD1';

            vm.CardMainCaption1 = ''; vm.CardAmount1 = ''; vm.CardMainCaption2 = ''; vm.CardAmount2 = ''; vm.CardMainCaption3 = ''; vm.CardAmount3 = '';
            vm.CardMainCaption4 = ''; vm.CardAmount4 = ''; vm.CardMainCaption5 = ''; vm.CardAmount5 = ''; vm.CardMainCaption6 = ''; vm.CardAmount6 = '';
            vm.CardMainCaption7 = ''; vm.CardAmount7 = '';

            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
            //vm.viewModelHelper.apiGet('api/dashboard2/landingpagecards/' + '/' + vm.valu, null,
            vm.viewModelHelper.apiGet('api/landingpage/cardsA/' + vm.valu, null,
                function (res) {
                    vm.DataCardList = res.data;

                    angular.forEach(vm.DataCardList, function (value, key) {

                        switch (value.MainCaption) {
                            case 'NRFF':
                                vm.CardAmount1 = fnum(value.Amount * 1000); vm.CardMainCaption1 = 'NRFF';
                                if (value.MainCaption !== '') { vm.CheckMC1 = true; }
                                break;

                            case 'Total Risk Assets':
                                vm.CardAmount2 = fnum(value.Amount * 1000); vm.CardMainCaption2 = 'Total Risk Assets';
                                if (value.MainCaption !== '') { vm.CheckMC2 = true; }
                                break;

                            case 'TOTAL COMM AND FEES':
                                vm.CardAmount3 = fnum(value.Amount * 1000); vm.CardMainCaption3 = 'Total Comm and Fees';
                                if (value.MainCaption !== '') { vm.CheckMC3 = true; }
                                break;

                            case 'DIRECT OPERATING EXPENSE':
                                vm.CardAmount4 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption4 = 'Direct Operating Expense';
                                if (value.MainCaption !== '') { vm.CheckMC4 = true; }
                                break;

                            case 'Total Deposits':
                                vm.CardAmount5 = fnum(value.Amount * 1000); vm.CardMainCaption5 = 'Total Deposits';
                                if (value.MainCaption !== '') { vm.CheckMC5 = true; }
                                break;

                            case 'ACTIVE ACCOUNTS':
                                vm.CardAmount6 = fnum(value.Amount * 1000); vm.CardMainCaption6 = 'Active Accounts';
                                if (value.MainCaption !== '') { vm.CheckMC6 = true; }
                        }

                        //switch (value.MainCaption) {
                        //    case 'Total Deposits':
                        //        vm.CardAmount1 = fnum(value.Amount * 1000); vm.CardMainCaption1 = 'Total Deposits';
                        //        //if (value.MainCaption !== '') { vm.CheckMC1 = true; }
                        //        break;

                        //    case 'Loans':
                        //        vm.CardAmount2 = fnum(value.Amount * 1000); vm.CardMainCaption2 = 'Loans';
                        //        //if (value.MainCaption !== '') { vm.CheckMC2 = true; }
                        //        break;

                        //    case 'TOTAL COMM AND FEES':
                        //        vm.CardAmount3 = fnum(value.Amount * 1000); vm.CardMainCaption3 = 'Total Comm and Fees';
                        //        //if (value.MainCaption !== '') { vm.CheckMC3 = true; }
                        //        break;

                        //    case 'DIRECT OPERATING EXPENSE':
                        //        vm.CardAmount4 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption4 = 'Direct Operating Expense';
                        //        //if (value.MainCaption !== '') { vm.CheckMC4 = true; }
                        //        break;

                        //    case 'ACTIVE ACCOUNTS':
                        //        vm.CardAmount5 = fnum(value.Amount * 1000); vm.CardMainCaption5 = 'Active Accounts';
                        //        //if (value.MainCaption !== '') { vm.CheckMC5 = true; }
                        //        break;

                        //    case 'INACTIVE ACCOUNTS':
                        //        vm.CardAmount6 = fnum(value.Amount * 1000); vm.CardMainCaption6 = 'Inactive Accounts';
                        //        //if (value.MainCaption !== '') { vm.CheckMC5 = true; }
                        //        break;

                        //    case 'TOTAL DORMANT ACCOUNTS':
                        //        vm.CardAmount7 = fnum(value.Amount * 1000); vm.CardMainCaption7 = 'Dormant Accounts';
                        //    //if (value.MainCaption !== '') { vm.CheckMC6 = true; }
                        //}

                    });
                    //if (vm.DataCardList !== []) { vm.checkdata = true; }
                    //if (vm.DataCardList.length !== 0) { vm.maki === true; }
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);

        }; //end of function


        ////........................................................................................................

        //=========== number formatting starts ===========================

        var fnum = function (x) {
            if (isNaN(x)) return x;

            if (x < 9999) {
                return x;
            }

            if (x < 1000000) {
                //return Math.round(x / 1000) + "K";
                return (x / 1000).toFixed(2) + "K";
            }
            if (x < 10000000) {
                return (x / 1000000).toFixed(2) + "M";
            }

            if (x < 1000000000) {
                return (x / 1000000).toFixed(2) + "M";
            }

            if (x < 1000000000000) {
                return (x / 1000000000).toFixed(2) + "B";
            }

            return "1T+";
        };
        //========== number formatting ends ============================





        cardFunc();
        //functionThatReturnsStyle();
        dFunc();
        mt1Func();
        mt2Func();
        mt3Func();

    }
}());
