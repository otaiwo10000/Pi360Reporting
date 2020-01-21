

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardExpenseController",
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout',
            DashboardExpenseController]);

    function DashboardExpenseController($scope, $state, viewModelHelper, validator, $rootScope, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'Expense-view';
        vm.viewName = 'Expense Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];       



        ////................... for cards ..................................................................................................................................

        vm.DataCardList = [];
        //vm.DataCardsLiist2 = [];
        vm.MaincaptionCardList = [];
        vm.AmountCardList = [];
        //vm.PeriodCardList = [];
        vm.valu = 'CARD3';

        var cardFunc = function () {
            vm.DataCardList = [];
            vm.MaincaptionCardList = [];
            vm.AmountCardList = [];

            vm.CardMainCaption1 = ''; vm.CardAmount1 = ''; vm.CardMainCaption2 = ''; vm.CardAmount2 = ''; vm.CardMainCaption3 = ''; vm.CardAmount3 = '';
            vm.CardMainCaption4 = ''; vm.CardAmount4 = '';

            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
            vm.viewModelHelper.apiGet('api/dashboard2/landingpagecards/' + vm.valu, null,
               function (res) {
                   vm.DataCardList = res.data;

                   angular.forEach(vm.DataCardList, function (value, key) {

                       switch (value.MainCaption) {
                           case 'TOTAL EXPENSE': 
                               vm.CardAmount1 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption1 = 'Total Expense';
                               break;

                           case 'DIRECT STAFF EXPENSES':
                               vm.CardAmount2 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption2 = 'Direct Staff Expenses';
                               break;

                           case 'DIRECT OPERATING EXPENSE':
                               vm.CardAmount3 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption3 = 'Direct Operating Expense';
                               break;

                           case 'TOTAL OPERATING EXPENSE':
                               vm.CardAmount4 = fnum(Math.abs(value.Amount * 1000)); vm.CardMainCaption4 = 'Total Operating Expense';
                       }
                   });
               },
               function (result) {
                   // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
               }, null);

        } //end of function






        /////////////////////////////// cards ends //////////////////////////////////////////////////////////////////



//=========================================== Expense Mix  starts ===================================================================

        vm.MixSubcaptions = [];
        vm.MixAmounts = [];

        //vm.mixparam1 = "Total Deposits";
        vm.mixparam1 = "CARD2";
        vm.MixDataList1 = [];
        //vm.MixSubcaptions2 = [];
        //vm.MixAmounts2 = [];

        vm.piechartSubCaptionAndAmount1 = [];
        vm.piechartSubCaptionAndAmount1Modulus = [];
        //vm.SubCaptionPie1Modulus = 

        var dFunc1 = function () {

            vm.MixSubcaptions = [];
            vm.MixAmounts = [];
            vm.MixDataList1 = [];
            
            vm.piechartSubCaptionAndAmount1 = [];
            vm.piechartSubCaptionAndAmount1Modulus = [];

            vm.viewModelHelper.apiGet('api/dashboard2/subcaptions/' + vm.mixparam1, null,

                function (res) {
                    vm.MixDataList1 = res.data;

                    angular.forEach(vm.MixDataList1, function (c, d) {
                        vm.piechartSubCaptionAndAmount1.push([c.SubCaption, Math.abs(c.Amount)]);
                    });

                    //angular.forEach(vm.piechartSubCaptionAndAmount1, function (c, d) {
                    //    vm.piechartSubCaptionAndAmount1Modulus.push([c.SubCaption, Math.abs(c.Amount)]);
                    //});

                    D1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }
        
        var D1ChartFunc = function () {

            Highcharts.chart('expensemix1', {
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
                    text: 'Expense Mix', 
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
                    data: vm.piechartSubCaptionAndAmount1
                    //data: vm.piechartSubCaptionAndAmount1Modulus                    
                }]

            });
        };

 //======================================== Expense Mix ends ================================================================
       
    
//======================================== Expense Trend starts ============================================================

        vm.expTrendDataList1 = [];
        vm.expTrendAmounts1 = [];
        vm.expTrendBudgets1 = [];
        vm.expTrendPeriods1 = [];

        vm.expTrendAmountsModulusTrend1 = [];
        vm.expTrendBudgetsModulusTrend1 = [];
        vm.exptrendparam1 = 'CARD2';
        vm.exptrendORmonthlyparam1 = 'trend';
        vm.ceo2_2 = [];
        vm.ceo2_2b = [];

        var exptFunc1 = function () {

            vm.expTrendDataList1 = [];
            vm.expTrendAmounts1 = [];
            vm.expTrendBudgets1 = [];
            vm.expTrendPeriods1 = [];
            vm.expTrendAmountsModulusTrend1 = [];
            vm.expTrendBudgetsModulusTrend1 = [];
            vm.ceo2_2 = [];
            vm.ceo2_2b = [];

            vm.viewModelHelper.apiGet('api/dashboard2/subcaptions/' + vm.exptrendparam1 + '/' + vm.exptrendORmonthlyparam1, null,
                function (res) {
                    vm.expTrendDataList1 = res.data;

                    angular.forEach(vm.expTrendDataList1, function (value, key) {
                        //vm.nANDperiod = value.abpList;

                        angular.forEach(value.abpList, function (a, b) {
                            if (vm.expTrendPeriods1.indexOf(a.Period) === -1) {  //get unique /distict values of a.period
                                vm.expTrendPeriods1.push(a.Period);
                            }
                        });
                    });



                    vm.ceo2_2 = [];
                    vm.ceo2_2b = [];
                    vm.Amounts = [];
                    angular.forEach(vm.expTrendDataList1, function (value, key) {
                        angular.forEach(value.abpList, function (a, b) { vm.Amounts.push(Math.abs(a.Amount)); });

                        vm.ceo2_2 = { name: value.SubCaption, data: vm.Amounts, stack: value.stack };
                        // vm.ceo2_2.push({name: value.name, data: value.data, stack: value.stack});
                        //vm.ceo2_2.prototype.push.apply(name, data, stack);
                        vm.ceo2_2b.push(vm.ceo2_2);
                        vm.Amounts = [];
                    });


                    periodFunc(vm.expTrendPeriods1);

					checkLoadingFunc();
                    expT1ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var expT1ChartFunc = function () {

            Highcharts.chart('expensetrend1', {
                chart: {
                    type: 'line',
                    //backgroundColor: 'dodgerblue',

                     //margin: [0, 0, 0, 0],
                     //spacingTop: 10,
                     //spacingBottom: 10,
                     //spacingLeft: 1000,
                     //spacingRight: 1000,

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
                    text: 'Expense Trend',
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
                    enabled: false,
                    //align: 'right',
                    //verticalAlign: 'middle'
                },

                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                //series: [{ name: 'Actual', data: vm.acctTrendAmounts1 }, { name: 'Budget', data: vm.TrendBudgets1 }]
                series: vm.ceo2_2b
            });
        };
       

//======================================== Expense Trend ends ==============================================================


        //====================================== Expense Tren 2 starts ===============================================

        vm.TrendDataList2 = [];
        vm.TrendAmounts2 = [];
        vm.TrendBudgets2 = [];
        vm.TrendPeriods2 = [];

        vm.TrendAmountsModulusTrend2 = [];
        vm.TrendBudgetsModulusTrend2 = [];
        vm.trendparam2 = 'TOTAL EXPENSE';

        var tFunc2 = function () {

            vm.TrendDataList2 = [];
            vm.TrendAmounts2 = [];
            vm.TrendBudgets2 = [];
            vm.TrendPeriods2 = [];
            vm.TrendAmountsModulusTrend2 = [];
            vm.TrendBudgetsModulusTrend2 = [];

            vm.viewModelHelper.apiGet('api/dashboard/dashboardtrend2/' + vm.trendparam2, null,
                function (res) {
                    vm.TrendDataList2 = res.data;

                    angular.forEach(vm.TrendDataList2, function (a, b) {
                        vm.TrendBudgets2.push(Math.abs(a.Budget));
                        vm.TrendAmounts2.push(Math.abs(a.Amount));
                        vm.TrendPeriods2.push(a.Period);
                    });

                    periodFunc(vm.TrendPeriods2);

                    T2ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        var T2ChartFunc = function () {

            Highcharts.chart('expensetrend2', {
                chart: {
                    // //renderTo: 'expensetrend2',
                    // margin: [0,0, 0, 0],
                    // //spacingTop: 0,
                    // spacingBottom: 0,
                    // spacingLeft: 0,
                    // spacingRight: 0,

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
                    text: 'Total Expense/Budget Trend',
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

                series: [{ name: 'Actual', data: vm.TrendAmounts2 }, { name: 'Budget', data: vm.TrendBudgets2 }]
            });
        };


        //====================================== Expense Trend 2 ends ============================================

		vm.spinningLoading = true;
        vm.contentsLoading = false;

        var checkLoadingFunc = function () {
            if ((vm.piechartSubCaptionAndAmount1.length !== 0 || vm.piechartSubCaptionAndAmount1 !== []) &&
                (vm.ceo2_2b.length !== 0 || vm.ceo2_2b !== []) &&
                (vm.TrendAmounts2.length !== 0 || vm.TrendAmounts2 !== [])) {
                vm.spinningLoading = false;
                vm.contentsLoading = true;
            }
        };


        vm.p = [];
       // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {
                if (value === 1) { vm.p.push("Jan"); }
                else if (value === 2) { vm.p.push("Feb"); }
                else if (value === 3) { vm.p.push("Mar"); }
                else if (value === 4) { vm.p.push("Apr") }
                else if (value === 5) { vm.p.push("May") }
                else if (value === 6) { vm.p.push("Jun") }
                else if (value === 7) { vm.p.push("Jul"); }
                else if (value === 8) { vm.p.push("Aug"); }
                else if (value === 9) { vm.p.push("Sep") }
                else if (value === 10) { vm.p.push("Oct") }
                else if (value === 11) { vm.p.push("Nov") }
                else if (value === 12) { vm.p.push("Dec") }
            });
        };

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
      
		
		
		
		
		
        $rootScope.$on("dashboardexpenseGlobalFunc_ParentMethod", function () {
            $scope.dashboardexpenseGlobalFunc();
        });

        $scope.dashboardexpenseGlobalFunc = function () {
            cardFunc();
            dFunc1();
            exptFunc1();
            tFunc2();
        }


		
        cardFunc();
        dFunc1();
        exptFunc1();
        tFunc2()
        
    }
}());
