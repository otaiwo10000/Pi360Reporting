
//correct order of dependencies: ['$rootScope', '$scope', '$http', '$location', '$localStorage', 'loginService'

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardDepositController",
        //['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout', '$stateParams',
            DashboardDepositController]);
    

    //function DashboardDepositController($scope, $state, $rootScope, viewModelHelper, $modal, $modalInstance, validator, Excel, $timeout) {
    // function DashboardDepositController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {
    function DashboardDepositController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {

    
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'Deposit-view';
        vm.viewName = 'Deposit Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //----------------------------------------------------------

        vm.yaxistitlecolor = 'white';



        var currentMISCodeReport = "" // i will create an api mth that return the loggedOn MISCode and currentlly selected MISCode. Then, call it on page load.
       

/////////////////////// Mix Function /////////////////////////////////////////////////

        vm.MixSubcaptions = [];
        vm.MixAmounts = [];

        vm.mixparam1 = "Fixed Deposit";
        vm.MixDataList1 = [];
        vm.MixSubcaptions1 = [];
        vm.MixAmounts1 = [];
        vm.MixSubcaptions1b = [];

        var dFunc1 = function () {
            vm.MixDataList1 = [];
            vm.MixSubcaptions1 = [];
            vm.MixAmounts1 = [];
            vm.MixSubcaptions1b = [];

            vm.viewModelHelper.apiGet('api/dashboard/depositbycurrency/' + vm.mixparam3, null,
                function (res) {
                    vm.MixDataList1 = res.data;

                    //angular.forEach(vm.MixDataList1, function (a, b) {

                    //    //if (a.SubCaption == 'banana' || fruit == 'lemon') {
                    //    //    handleYellowFruit();
                    //    //}

                    //    vm.MixSubcaptions1.push(a.SubCaption);
                    //    //vm.MixAmounts3.push(a.Amount);
                    //});
                    ////periodFunc(vm.MixAmounts3);

                    angular.forEach(vm.MixDataList1, function (a, b) {
                        //vm.MixSubcaptions1.push({lcy: a.lcy, fcy:a.fcy});
                        //vm.MixSubcaptions1.push({ lcy: a.lcy, fcy: a.fcy });
                        vm.MixSubcaptions1.push({ lcy: a.lcy, fcy: a.fcy });
                        vm.MixAmounts1.push(a.Amount);

                    });

                    //D1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var D1ChartFunc = function () {

            Highcharts.chart('depositmix1', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: {
                        fontFamily: 'serif',
                        color: 'black',
                        fontWeight: 'bold',
                    }



                },  //==== chart end =====

                title: {
                    text: 'Deposit Breakdown by Currency',
                    //text: vm.D1ListMainCaption,
                    style: { fontSize: '120%'}
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
                    categories: vm.MixSubcaptions1,
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '120%',
                            //fontFamily: 'serif',
                            //fontWeight: 'bold'
                        }
                    }
                    //style: { fontSize: '1%', fontFamily: 'serif'}
                },
                yAxis: {
                    title: {
                        text: 'Amount', margin: 30
                    },
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
                    data: vm.MixAmounts1
                }]

            });
        };


//...................................................................................................................................

        vm.mixparam2 = "Total Deposits";
        vm.MixDataList2 = [];
        vm.MixSubcaptions2 = [];
        vm.MixAmounts2 = [];
        vm.piechartSubCaptionAndAmount2 = [];

        var dFunc2 = function () {

            vm.MixDataList2 = [];
            vm.MixSubcaptions2 = [];
            vm.MixAmounts2 = [];
            vm.piechartSubCaptionAndAmount2 = [];
            //vm.viewModelHelper.apiGet('api/menu/getmenuobject/' + vm.reportId, null,
            vm.viewModelHelper.apiGet('api/dashboard/dashboardmix/' + vm.mixparam2, null,
                function (res) {
                    vm.MixDataList2 = res.data;

                    //angular.forEach(vm.MixDataList2, function (a, b) {
                    //            vm.MixSubcaptions2.push(a.SubCaption);
                    //            vm.MixAmounts2.push(a.Amount);
                    //});

                    angular.forEach(vm.MixDataList2, function (c, d) {
                        vm.piechartSubCaptionAndAmount2.push([c.SubCaption, c.Amount]);
                    });

                    D2ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }

        
        var D2ChartFunc = function () {

            Highcharts.chart('depositmix2', {
                chart: {
                    type: 'pie',
                    //backgroundColor: 'dodgerblue',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        //depth: 70
                    },
                    style: {
                        fontFamily: 'serif',
                        color: 'black',
                        fontWeight: 'bold',
                    }                    

                },  //==== chart end =====

                title: {
                    text: 'Deposit Mix', 
                    //text: vm.D1ListMainCaption,
                    style: { fontSize: '120%'}
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                credits: {
                    enabled: false,  // to remove highcharts.com
                    //text: 'test.com',
                    //href: 'http//www.t.com'
                    position: { align: 'left', x: 15}  //aligning the .com text eg 'highcharts.com'  to the left
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
                            color: 'black',
                            //style: { fontFamily: '\'Lato\', sans-serif', lineHeight: '18px', fontSize: '17px' }
                            style: { fontFamily: 'sans-serif', fontSize: '70%' }
                        }
                    }
                },
                //xAxis: {
                //    //categories: Highcharts.getOptions().lang.shortMonths,
                //    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                //    categories: vm.MixSubcaptions2,
                //    labels: {
                //        skew3d: true,
                //        style: {
                //            fontSize: '120%', 
                //            //fontFamily: 'serif',
                //            //fontWeight: 'bold'
                //        }
                //    }
                //    //style: { fontSize: '1%', fontFamily: 'serif'}
                //},
                //yAxis: {
                //    title: {
                //        text: 'Amount'
                //    },
                //},

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
                    //type: 'pie',
                    //name: vm.D2ListMainCaption,
                    data: vm.piechartSubCaptionAndAmount2
                }]

            });
        };

 //.....................................................................................................................................................

        vm.mixparam3 = "Fixed Deposit";
        vm.MixDataList3 = [];
        vm.MixSubcaptions3 = [];
        vm.MixAmounts3 = [];

        var dFunc3 = function () {

            vm.MixDataList3 = [];
            vm.MixSubcaptions3 = [];
            vm.MixAmounts3 = [];
            //vm.viewModelHelper.apiGet('api/menu/getmenuobject/' + vm.reportId, null,
            vm.viewModelHelper.apiGet('api/dashboard/dashboardmix/' + vm.mixparam3, null,
                function (res) {
                    vm.MixDataList3 = res.data;

                    angular.forEach(vm.MixDataList3, function (a, b) {
                        vm.MixSubcaptions3.push(a.SubCaption);
                        vm.MixAmounts3.push(a.Amount);

                    });
                    periodFunc(vm.MixAmounts3);

                    D3ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var D3ChartFunc = function () {

            Highcharts.chart('depositmix3', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: {
                        fontFamily: 'serif',
                        color: 'black',
                        fontWeight: 'bold',
                    }



                },  //==== chart end =====

                title: {
                    text: 'Fixed Deposit Breakdown',
                    //text: vm.D1ListMainCaption,
                    style: { fontSize: '120%' }
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
                    categories: vm.MixSubcaptions3,
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '120%',
                            //fontFamily: 'serif',
                            //fontWeight: 'bold'
                        }
                    }
                    //style: { fontSize: '1%', fontFamily: 'serif'}
                },
                yAxis: {
                    title: {
                        text: 'Amount', margin: 30
                    },
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
                    data: vm.MixAmounts3
                }]

            });
        };

/////////////////////////////// D1 Ends ///////////////////////////////////////////////////////////////////////////

/////////////////////// Trend Function //////////////////////////////////////////////////////////////////////////////

        vm.MTList_t2 = [];
        vm.MT2List = [];
        vm.MT2ListMainCaption = [];
        vm.MT2MainCaption = "";
        vm.MT2ListPeriods = [];
        vm.MT2ListAmounts = [];
        vm.abpList = [];
        vm.abpList2 = [];
        vm.MT1MainCaption_var2 = "";
        vm.trendparam2 = "Low Cost Deposits";

        var tFunc2 = function () {

            vm.MTList_t2 = [];
            vm.MT2List = [];
            vm.MT2ListMainCaption = [];
            vm.MT2MainCaption = "";
            vm.MT2ListPeriods = [];
            vm.MT2ListAmounts = [];
            vm.abpList = [];
            vm.abpList2 = [];
            vm.MT1MainCaption_var2 = "";

            vm.viewModelHelper.apiGet('api/dashboard/dashboardtrend/' + vm.trendparam2, null,
                function (res) {

                    vm.MTList_t2 = res.data;

                    angular.forEach(vm.MTList_t2, function (value, key) {
                        vm.nANDperiod = value.abpList;

                        angular.forEach(vm.nANDperiod, function (a, b) {
                            if (vm.MT2ListPeriods.indexOf(a.Period) == -1) {  //get unique /distict values of a.period
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


                    T2ChartFunc();

                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var T2ChartFunc = function () {

            Highcharts.chart('deposittrend2', {
                chart: {
                    type: 'line',
                    //backgroundColor: 'dodgerblue',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: {
                        fontFamily: 'serif',
                        color: 'black',
                        fontWeight: 'bold',
                    }
                },

                title: {
                    text: 'Low Cost Trend',
                    style: { fontSize: '120%' }
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
                        style: {
                            fontSize: '120%'
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: 'Amount', margin: 30
                    }
                },

                legend: {
                    enabled: true,
                    align: 'right',
                    //verticalAlign: 'middle'
                    itemStyle: { fontSize: '100%', color: 'black', font: 'serif' },
                    //itemStyle: $scope.legendstyleglobal
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

//.........................................................................................................................................


        vm.TrendDataList1 = [];
        vm.TrendAmounts1 = [];
        vm.TrendBudgets1 = [];
        vm.TrendPeriods1 = [];
        //vm.TrendPeriods1 = [];
        vm.trendparam1 = "Total Deposits";

        var tFunc1 = function () {

            vm.TrendDataList1 = [];
            vm.TrendAmounts1 = [];
            vm.TrendBudgets1 = [];
            vm.TrendPeriods1 = [];
            vm.viewModelHelper.apiGet('api/dashboard/dashboardtrend2/' + vm.trendparam1, null,
                function (res) {
                    vm.TrendDataList1 = res.data;

                    angular.forEach(vm.TrendDataList1, function (a, b) {
                        vm.TrendBudgets1.push(a.Budget);
                        vm.TrendAmounts1.push(a.Amount);
                        vm.TrendPeriods1.push(a.Period);
                    });

                    periodFunc(vm.TrendPeriods1);
                   
                    T1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var T1ChartFunc = function () {

            Highcharts.chart('deposittrend1', {
                chart: {
                    type: 'line',
                    //backgroundColor: 'dodgerblue',
                    options3d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    },
                    style: {
                        fontFamily: 'serif',
                        color: 'black',
                        fontWeight: 'bold',
                    }



                },  //==== chart end =====

                title: {
                    text: 'Total Deposits Trend',
                    //text: vm.D1ListMainCaption,
                    style: { fontSize: '120%' }
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
                    categories: vm.p,
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '120%',
                            //fontFamily: 'serif',
                            //fontWeight: 'bold'
                        }
                    }
                    //style: { fontSize: '1%', fontFamily: 'serif'}
                },
                yAxis: {
                    title: {
                        text: 'Amount', margin: 30
                    },
                },

                legend: {
                    enabled: true,
                    align: 'right',
                    //verticalAlign: 'middle'
                    itemStyle: { fontSize: '100%', color: 'black', font: 'serif' },
                    //itemStyle: $scope.legendstyleglobal
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{ name: 'Actual', data: vm.TrendAmounts1 }, { name: 'Budget', data: vm.TrendBudgets1 }]
            });
        };


/////////////////////////////// Trend ends ///////////////////////////////////////////////////////////////////////////


        ////////////////////////////// Cards starts /////////////////////////////////////////////////////////////////

        //vm.cardList = [];

        //var cardFunc = function () {

        //    vm.cardList = [];

        //    vm.viewModelHelper.apiGet('api/dashboard/dashboardcardmix', null,
        //       function (res) {
        //           vm.cardList = res.data;
        //       },
        //       function (result) {
        //           // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //       }, null);

        //} //end of function

        
        ////................... for cards ..................................................................................................................................

        var depositCardFunc = function () {
            vm.DataCardList = [];
            //vm.DataCardsLiist2 = [];
            vm.MaincaptionCardList = [];
            vm.AmountCardList = [];
            //vm.PeriodCardList = [];
            vm.valu = 'CARD2';

            vm.CardMainCaption1 = ''; vm.CardAmount1 = ''; vm.CardMainCaption2 = ''; vm.CardAmount2 = ''; vm.CardMainCaption3 = ''; vm.CardAmount3 = '';
            vm.CardMainCaption4 = ''; vm.CardAmount4 = ''; 

            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
            vm.viewModelHelper.apiGet('api/dashboard2/landingpagecards/' + '/' + vm.valu, null,
               function (res) {
                   vm.DataCardList = res.data;

                   angular.forEach(vm.DataCardList, function (value, key) {

                       switch (value.MainCaption) {
                           case 'Total Deposits':
                               vm.CardAmount1 = fnum(value.Amount); vm.CardMainCaption1 = 'Total Deposits';
                               break;

                           case 'Low Cost Deposits':
                               vm.CardAmount2 = fnum(value.Amount); vm.CardMainCaption2 = 'Low Cost Deposits';
                               break;

                           case 'Demand Deposit':
                               vm.CardAmount3 = fnum(value.Amount); vm.CardMainCaption3 = 'Demand Deposit';
                               break;

                           case 'FIXED DEPOSIT':
                               vm.CardAmount4 = fnum(value.Amount); vm.CardMainCaption4 = 'Fixed Deposit';
                       }
                   });
               },
               function (result) {
                   // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
               }, null);

        } //end of function






        /////////////////////////////// cards ends //////////////////////////////////////////////////////////////////


        //vm.open = function () {
        ////$rootScope.globalopen = function () {

        //    var modalInstance = $modal.open({
        //        //templateUrl: 'modalctrl-view.html',
        //        //templateUrl: '/app/assets/views/modalctrl-view.html',
        //        backdrop: true,
        //        templateUrl: rootUrl + 'app/views/modalctrl-view.html',
        //        controller: 'ModalCtrl as vm',
        //        windowClass: 'app-modal-window1'
        //    })
        //};
       
       

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
        }
      

        //$scope.dashboarddepositGlobalFunc = function () {
        ////$rootScope.dashboarddepositGlobalFunc = function () {
        //    cardFunc();
        //    dFunc1();
        //    dFunc2();
        //    dFunc3();
        //    tFunc1();
        //    tFunc2();
        //};


        $rootScope.$on("dashboarddepositGlobalFunc_ParentMethod", function () {
            //$scope.dashboarddepositGlobalFunc();
            depositCardFunc();
            dFunc1();
            dFunc2();
            dFunc3();
            tFunc1();
            tFunc2();
        });


        //$scope.sessindataObj = {};
        //$scope.test100 = 'test1';
        //$scope.miscodeGlobalData = 'null';
        //$scope.customSesssionVariableFunc = function () {
          
        //    vm.viewModelHelper.apiGet('api/sessiondata/sessionvariables', null,
        //                   function (res) {
        //                       $scope.sessindataObj = res.data;

        //                       $scope.test100 = 'test2';
        //                       //angular.forEach(vm.sessindataObj, function (a, b) {                           
        //                       //    $scope.miscodeGlobalData.push(a.MISCode);
        //                       //});
        //                   },
        //                   function (result) {
        //                       //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //                   }, null);    
        //}


        //$scope.dashboarddepositGlobalFunc = function () {
        //    depositCardFunc();
        //    dFunc1();
        //    dFunc2();
        //    dFunc3();
        //    tFunc1();
        //    tFunc2();
        //}


            //$rootScope.dashboarddepositFunc();
			

        depositCardFunc();
        dFunc1();
        dFunc2();
        dFunc3();
        tFunc1();
        tFunc2();
        
    }
}());
