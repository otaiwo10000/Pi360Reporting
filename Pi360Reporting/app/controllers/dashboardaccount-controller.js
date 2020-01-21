
//correct order of dependencies: ['$rootScope', '$scope', '$http', '$location', '$localStorage', 'loginService'

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardAccountController",
        //['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout', '$stateParams',
            DashboardAccountController]);
    

    function DashboardAccountController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {

    
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'Account-view';
        vm.viewName = 'Account Analytics';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];



        ////................... for cards ..................................................................................................................................

        vm.DataCardList = [];
        //vm.DataCardsLiist2 = [];
        vm.MaincaptionCardList = [];
        vm.AmountCardList = [];
        //vm.PeriodCardList = [];
        vm.valu = 'CARD4';

        var cardFunc = function () {
            vm.DataCardList = [];
            vm.MaincaptionCardList = [];
            vm.AmountCardList = [];

            vm.CardMainCaption1 = ''; vm.CardAmount1 = ''; vm.CardMainCaption2 = ''; vm.CardAmount2 = ''; vm.CardMainCaption3 = ''; vm.CardAmount3 = '';
            vm.CardMainCaption4 = ''; vm.CardAmount4 = ''; vm.CardMainCaption5 = ''; vm.CardAmount5 = '';

            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
            vm.viewModelHelper.apiGet('api/dashboard2/landingpagecards/' + '/' + vm.valu, null,
               function (res) {
                   vm.DataCardList = res.data;

                   angular.forEach(vm.DataCardList, function (value, key) {

                       switch (value.MainCaption) {
                           case 'ACTIVE ACCOUNTS':
                               vm.CardAmount1 = fnum(Math.abs(value.Amount)); vm.CardMainCaption1 = 'No of Active Accounts';
                               break;

                           case 'TOTAL DORMANT ACCOUNTS':
                               vm.CardAmount2 = fnum(Math.abs(value.Amount)); vm.CardMainCaption2 = 'No of Dormant Accounts';
                               break;

                           case 'COUNT OF REACTIVATED ACCOUNTS':
                               vm.CardAmount3 = fnum(Math.abs(value.Amount)); vm.CardMainCaption3 = 'No of Reactivated Accounts';
                               break;                        

                           case 'TOTAL ACCOUNTS':
                               vm.CardAmount4 = fnum(Math.abs(value.Amount)); vm.CardMainCaption4 = 'No of Total Accounts';
                               break;

                           case 'New Account':
                               vm.CardAmount5 = fnum(Math.abs(value.Amount)); vm.CardMainCaption5 = 'No of New Accounts';
                       }
                   });
               },
               function (result) {
                   // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
               }, null);

        } //end of function


        /////////////////// Mix  starts ///////////////////////////////////////////

        vm.mixparam1 = "CARD5";
        vm.MixDataList1 = [];
        vm.MixMaincaptions1 = [];
        vm.MixAmounts1 = [];

        var mixFunc1 = function () {

            vm.MixDataList1 = [];
            vm.MixMaincaptions1 = [];
            vm.MixAmounts1 = [];
            vm.viewModelHelper.apiGet('api/dashboard2/landingpagecards/' + vm.mixparam1, null,
                function (res) {
                    vm.MixDataList1 = res.data;

                    angular.forEach(vm.MixDataList1, function (a, b) {
                        vm.MixMaincaptions1.push(a.MainCaption);
                        vm.MixAmounts1.push(a.Amount);
                    });
                    periodFunc(vm.MixAmounts1);

                    D1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var D1ChartFunc = function () {

            Highcharts.chart('acctmix1', {
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
                    text: 'Account Balances',
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
                    categories: vm.MixMaincaptions1,
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
                        text: 'Amount'
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



        ////////////////// Mix ends //////////////////////////////////////////////




        /////////////////////// Trend Function //////////////////////////////////////////////////////////////////////////////

        vm.acctTrendDataList1 = [];
        vm.acctTrendAmounts1 = [];
        vm.acctTrendBudgets1 = [];
        vm.acctTrendPeriods1 = [];

        vm.acctTrendAmountsModulusTrend1 = [];
        vm.acctTrendBudgetsModulusTrend1 = [];
        vm.accttrendparam1 = 'CARD1';
        vm.accttrendORmonthlyparam1 = 'trend';
        vm.ceo2_2 = [];
        vm.ceo2_2b = [];

        var accttFunc1 = function () {

            vm.acctTrendDataList1 = [];
            vm.acctTrendAmounts1 = [];
            vm.acctTrendBudgets1 = [];
            vm.acctTrendPeriods1 = [];
            vm.acctTrendAmountsModulusTrend1 = [];
            vm.acctTrendBudgetsModulusTrend1 = [];
            vm.ceo2_2 = [];
            vm.ceo2_2b = [];

            vm.viewModelHelper.apiGet('api/dashboard2/subcaptions/' + vm.accttrendparam1 + '/' + vm.accttrendORmonthlyparam1, null,
                function (res) {
                    vm.acctTrendDataList1 = res.data;

                    //vm.acctTrendPeriods1b = [];
                    //angular.forEach(vm.acctTrendDataList1, function (a, b) {
                    //    vm.acctTrendPeriods1b.push(a.Period);                   

                    //    //vm.ceo2_2 = { name: value.SubCaption, data: vm.Amounts, stack: value.stack };
                    //    //vm.ceo2_2 = { name: a.SubCaption, data: a.Amount };

                    //    //vm.ceo2_2 = { name: a.SubCaption, data: a.Amount};
                    //    //vm.ceo2_2b.push(vm.ceo2_2);

                    //    vm.ceo2_2.push({ name: a.SubCaption, data: a.Amount });
                    //});

                    //angular.forEach(vm.acctTrendPeriods1b, function (a, b) {
                    //    if (vm.acctTrendPeriods1.indexOf(a.Period) == -1) {  //get unique /distict values of a.period
                    //        vm.acctTrendPeriods1.push(a.Period);
                    //    }
                    //});


                    angular.forEach(vm.acctTrendDataList1, function (value, key) {
                        //vm.nANDperiod = value.abpList;

                        angular.forEach(value.abpList, function (a, b) {
                            if (vm.acctTrendPeriods1.indexOf(a.Period) == -1) {  //get unique /distict values of a.period
                                vm.acctTrendPeriods1.push(a.Period);
                            }
                        });
                    });


                    vm.ceo2_2 = [];
                    vm.ceo2_2b = [];
                    vm.Amounts = [];
                    angular.forEach(vm.acctTrendDataList1, function (value, key) {
                        angular.forEach(value.abpList, function (a, b) { vm.Amounts.push(a.Amount); });

                        vm.ceo2_2 = { name: value.SubCaption, data: vm.Amounts, stack: value.stack };
                        // vm.ceo2_2.push({name: value.name, data: value.data, stack: value.stack});
                        //vm.ceo2_2.prototype.push.apply(name, data, stack);
                        vm.ceo2_2b.push(vm.ceo2_2);
                        vm.Amounts = [];
                    });


                    periodFunc(vm.acctTrendPeriods1);

					
                    acctT1ChartFunc();
					checkLoadingFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var acctT1ChartFunc = function () {

            Highcharts.chart('accttrend1', {
                chart: {
                    // margin: [0, 0, 0, 0],
                    // //spacingTop: 0,
                    // spacingBottom: 2,
                    // spacingLeft: 0,
                    // spacingRight: 0,

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
                    text: 'Account Analytics Trend',
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
                        size: '100%',
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
                        text: 'Amount'
                    },
                },

                legend: {
                    enabled: true,
                    align: 'right',
                    //verticalAlign: 'middle'
                    itemStyle: { fontSize: '80%', color: 'black', font: 'serif' },
                    //itemStyle: $scope.legendstyleglobal
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                //series: [{ name: 'Actual', data: vm.acctTrendAmounts1 }, { name: 'Budget', data: vm.TrendBudgets1 }]
                //series: [{ name: 'Actual', data: vm.TrendAmountsModulusTrend1 }, { name: 'Budget', data: vm.TrendBudgetsModulusTrend1 }]
                series: vm.ceo2_2b
            });
        };


        //.........................................................................................................................................

		 vm.spinningLoading = true;
        vm.contentsLoading = false;

        var checkLoadingFunc = function () {
            if ((vm.MixMaincaptions1.length !== 0 || vm.MixMaincaptions1 != []) && 
                (vm.ceo2_2b.length !== 0 || vm.ceo2_2b != []))
            {
                vm.spinningLoading = false;
                vm.contentsLoading = true;
            }
        }
		
		
        vm.p = [];
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
        }
//========== number formatting ends ============================
		
		
		
		$rootScope.$on("dashboardaccountGlobalFunc_ParentMethod", function () {
            $scope.dashboardaccountGlobalFunc();
        });

        $scope.dashboardaccountGlobalFunc = function () {
            cardFunc();
            mixFunc1();
            accttFunc1();
        };



        cardFunc();
        mixFunc1();
        accttFunc1();

    }
}());
