
//correct order of dependencies: ['$rootScope', '$scope', '$http', '$location', '$localStorage', 'loginService'

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("DashboardCustomerCRMController",
        //['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout', '$stateParams',
            DashboardCustomerCRMController]);
    

    //function DashboardDepositController($scope, $state, $rootScope, viewModelHelper, $modal, $modalInstance, validator, Excel, $timeout) {
    // function DashboardDepositController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {
    function DashboardCustomerCRMController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {

    
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        var rootUrl = '';

        vm.view = 'customercrm-view';
        vm.viewName = 'Customer CRM Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //----------------------------------------------------------

        vm.yaxistitlecolor = 'white';


        var currentMISCodeReport = ""; // i will create an api mth that return the loggedOn MISCode and currentlly selected MISCode. Then, call it on page load.


 //.....................................................................................................................................................

        vm.mixparam3 = "Total Deposit";
        vm.MixDataList3 = [];
        vm.MixSubcaptions3 = [];
        vm.MixAmounts3 = [];

        var dFunc3 = function () {

            vm.MixDataList3 = [];
            vm.MixSubcaptions3 = [];
            vm.MixAmounts3 = [];
            vm.viewModelHelper.apiGet('api/customercrm/ccrm/' + vm.mixparam3, null,
                function (res) {
                    vm.MixDataList3 = res.data;

                    angular.forEach(vm.MixDataList3, function (a, b) {
                        vm.MixSubcaptions3.push(a.AccountNo);
                        vm.MixAmounts3.push(a.Amount);

                    });
                    periodFunc(vm.MixAmounts3);

                    D3ChartFunc();
                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };


        var D3ChartFunc = function () {

            Highcharts.chart('ccrm1', {
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
                        fontWeight: 'bold'
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
                    }
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
                            fontSize: '120%'
                            //fontFamily: 'serif',
                            //fontWeight: 'bold'
                        }
                    }
                    //style: { fontSize: '1%', fontFamily: 'serif'}
                },
                yAxis: {
                    title: {
                        text: 'Amount', margin: 30
                    }
                },

                legend: {
                    enabled: false
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


        
        vm.p = [];
       // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {
                if (value === 1) { vm.p.push("Jan"); }
                else if (value === 2) { vm.p.push("Feb"); }
                else if (value === 3) { vm.p.push("Mar"); }
                else if (value === 4) { vm.p.push("Apr"); }
                else if (value === 5) { vm.p.push("May"); }
                else if (value === 6) { vm.p.push("Jun"); }
                else if (value === 7) { vm.p.push("Jul"); }
                else if (value === 8) { vm.p.push("Aug"); }
                else if (value === 9) { vm.p.push("Sep"); }
                else if (value === 10) { vm.p.push("Oct"); }
                else if (value === 11) { vm.p.push("Nov"); }
                else if (value === 12) { vm.p.push("Dec"); }
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


        $rootScope.$on("dashboardcustomercrmGlobalFunc_ParentMethod", function () {
            ////$scope.dashboarddepositGlobalFunc();
            //depositCardFunc();
            //dFunc1();
            //dFunc2();
            //dFunc3();
            //tFunc1();
            //tFunc2();
        });


        //depositCardFunc();
        //dFunc1();
        //dFunc2();
        dFunc3();
        //tFunc1();
        //tFunc2();
        
    }
}());
