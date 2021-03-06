﻿

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController009",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
            ReportController009]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController009($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report009-view';
        vm.viewName = 'Report009';

        vm.menuObj = {};
        vm.yearperiodObj = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.yearList = [];
       
        vm.periodList = [
             { id: "1", name: "January" },
             { id: "2", name: "February" },
             { id: "3", name: "March" },
             { id: "4", name: "April" },
             { id: "5", name: "May" },
             { id: "6", name: "June" },
             { id: "7", name: "July" },
             { id: "8", name: "August" },
             { id: "9", name: "September" },
             { id: "10", name: "October" },
             { id: "11", name: "November" },
             { id: "12", name: "December" }
        ];

        vm.directionList = [
                 { value: "top", name: "Top" },
                 { value: "bottom", name: "Bottom" }
        ];

        vm.stateList = [
                { value: "monthly", name: "Monthly" },
                { value: "ytd", name: "Year to Date" }
        ];

        vm.categoryList = [
               { value: "Account Profitability Report", name: "Account Profitability Report" },
               { value: "Customer Profitability Report", name: "Customer Profitability Report" },            
        ];

        vm.currencyList = [{ id: "Naira", name: "Naira" }, { id: "Dollar", name: "Dollar" }, { id: "Pound", name: "Pound" }];


        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.productTypeList = [];
        vm.accountnumbermodel2 = "0000000000";
        vm.categorymodel = "Account Profitability Report"; // or liability
        vm.directionmodel2 = "top";
        vm.directionmodel = "top";
        vm.pathmodel2 = null;
        vm.periodmodel2 = null;
        vm.producttypemodel = 'null';
        vm.producttypemodel2 = "null";
        vm.rankingmodel = "AvgDebitBal";
        vm.rankingmodel2 = "AvgDebitBal";
        vm.statemodel2 = "monthly";
        vm.statemodel = "monthly";
        vm.topmodel2 = 10;
        vm.typemodel2 = $stateParams.ParameterKey;
        vm.yearmodel2 = 0;
        vm.rankingmodel2 = "AvgDebitBal";
        vm.topmodel = 10;
        vm.typemodel2 = null;
        vm.currencymodel = "Naira";
        vm.currencymodel2 = "Naira";

        vm.reportId = $stateParams.ParameterKey;

        
        var latestyearAndperiodFunc = function () {
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    ////initializing
                    vm.yearmodel = vm.yearperiodObj.Year.toString();
                    vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel2 = vm.yearperiodObj.Year;
                    vm.periodmodel2 = vm.yearperiodObj.Period.toString();
                },
                function (result) {

                }, null);
        };

        var menuobjFunc = function () {
            vm.viewModelHelper.apiGet('api/menu/getmenuobject/' + vm.reportId, null,
                function (result) {
                    vm.menuObj = result.data;
                    vm.pathmodel2 = vm.menuObj.ReportPAth;
                },
                function (result) {

                }, null);
        };

        var yearFunc = function () {
            yearsService.yearsFunc()
                .then(function (data) {
                    vm.yearList = data;
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };


        vm.currentStatus = '';
        //vm.currentPeriod = 0;
        vm.setPeriods = null;
        vm.lockPeriods = [];
        vm.currentYear = 0;
        vm.loggedindefcode = '';

        var defaultreportSet009 = function () {

            vm.currentStatus = '';
            latestyearAndperiodFunc();
            vm.viewModelHelper.apiGet('api/reportprocedure/mprreportstatus', null,
                function (result) {
                    vm.reportStatusObj = result.data;

                    //vm.currentPeriod = vm.reportStatusObj.Period;
                    vm.lockPeriods = vm.reportStatusObj.Period.split(',');
                    vm.currentYear = vm.reportStatusObj.Year;
                    vm.currentStatus = vm.reportStatusObj.ReportStatus;
                    vm.loggedindefcode = vm.reportStatusObj.loggedindefcode;


                    //if (vm.currentStatus === "OFF" && vm.loggedindefcode !== "SA") {
                    //    alert("Report for the current period is being processed");
                    //}
                    if (vm.currentStatus === "OFF" && vm.loggedindefcode !== "SA") {
                        //alert("Report for the periods ( " + vm.reportStatusObj.Period + " ) are being processed");
                        periodFunc(vm.lockPeriods);
                        alert("Report for the periods ( " + vm.p.join('/') + " ) are being processed");
                    }
                    else {
                        vm.reportId = vm.reportId;
                        vm.accountnumbermodel2 = vm.accountnumbermodel;
                        vm.categorymodel2 = "Account Profitability Report"; // or liability
                        vm.directionmodel2 = vm.directionmodel;
                        // vm.pathmodel2 = vm.menuObj.ReportPAth;
                        vm.pathmodel2 = 'null';
                        vm.periodmodel2 = vm.periodmodel;
                        vm.rankingmodel2 = vm.rankingmodel;
                        vm.statemodel = "monthly";
                        vm.statemodel2 = vm.statemodel;
                        vm.topmodel2 = vm.topmodel;
                        vm.typemodel2 = $stateParams.ParameterKey;
                        vm.yearmodel2 = vm.yearmodel;
                        vm.currencymodel2 = vm.currencymodel;
                    }

                },
                function (result) {

                }, null);
        };

        vm.periodmodel3 = 0;
        vm.reportSet009 = function () {

            //vm.currentStatus = '';
            //vm.currentPeriod = 0;
            //vm.currentYear = 0;

            vm.viewModelHelper.apiGet('api/reportprocedure/mprreportstatus', null,
                function (result) {
                    vm.reportStatusObj = result.data;

                    //vm.currentPeriod = vm.reportStatusObj.Period;
                    vm.lockPeriods = vm.reportStatusObj.Period.split(',');
                    vm.currentYear = vm.reportStatusObj.Year;
                    vm.currentStatus = vm.reportStatusObj.ReportStatus;
                    vm.loggedindefcode = vm.reportStatusObj.loggedindefcode;


                    //if (vm.currentStatus === "OFF" && vm.currentPeriod.toString() === vm.periodmodel.toString() && vm.currentYear.toString() === vm.yearmodel.toString() && vm.loggedindefcode !== 'SA') {
                    //    alert("Report for the current period is being processed");
                    //}
                    if (vm.currentStatus === "OFF" && vm.lockPeriods.indexOf(vm.periodmodel.toString()) !== -1 && vm.currentYear.toString() === vm.yearmodel.toString() && vm.loggedindefcode !== 'SA') {
                        //alert("Report for the periods ( " + vm.reportStatusObj.Period + " ) are being processed");
                        periodFunc(vm.lockPeriods);
                        alert("Report for the periods ( " + vm.p.join('/') + " ) are being processed");
                    }
                    else {
                        vm.reportId = vm.reportId;
                        vm.pathmodel2 = "null";
                        APRcaptionreportFunc();
                        RankingFunc();
                        vm.directionmodel2 = vm.directionmodel;
                        vm.accountnumbermodel2 = vm.accountnumbermodel;
                        //vm.producttypemodel =
                        vm.producttypemodel2 = vm.producttypemodel;
                        //vm.categorymodel2 = "Account Profitability Report"; // or liability
                        vm.categorymodel2 = vm.categorymodel;
                        vm.rankingmodel = "AvgDebitBal";
                        vm.rankingmodel2 = "AvgDebitBal";
                        vm.typemodel2 = null;
                        vm.statemodel2 = vm.statemodel;
                        vm.topmodel = 10;
                        vm.topmodel2 = 10;
                        vm.currencymodel2 = vm.currencymodel;
						vm.pathmodel2 = 'null';
                        vm.periodmodel2 = vm.periodmodel; 
                        vm.yearmodel2 = vm.yearmodel;
                        //vm.topmodel2 = vm.topmodel;
                        vm.periodmodel3 = vm.periodmodel3 + 1; 
                    }

                },
                function (result) {

                }, null);

        };



        var APRcaptionreportFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/APRdistinctcaptionreport', null,
                function (result) {
                    vm.productTypeList = result.data;
                },
                function (result) {

                }, null);
        };

        vm.rankingList = [];
        vm.Profitability = 'Profitability';

        var RankingFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/ranking/' + '/' + vm.Profitability, null,
                function (result) {
                    vm.rankingList = result.data;
                },
                function (result) {

                }, null);
        };

        var toggleTextFunc = function () {
            if (vm.tToggle === true) {
                vm.toggleText = "show prompt";
            }
            else if (vm.tToggle === false) {
                vm.toggleText = "collapse prompt";
            }
        };

        vm.toggleReportPage = function () {
            vm.tToggle = !vm.tToggle;
            document.getElementById("report009").hidden = vm.tToggle;
            toggleTextFunc();
        };

        vm.p = [];
        // angular.forEach(vm.MTList_t2, function (value, key) {
        var periodFunc = function (peri) {
            vm.p = [];
            angular.forEach(peri, function (value, key) {

                switch (value) {
                    case '1':
                        vm.p.push("Jan");
                        break;
                    case '2':
                        vm.p.push("Feb");
                        break;
                    case '3':
                        vm.p.push("Mar");
                        break;
                    case '4':
                        vm.p.push("Apr");
                        break;
                    case '5':
                        vm.p.push("May");
                        break;
                    case '6':
                        vm.p.push("Jun");
                        break;
                    case '7':
                        vm.p.push("Jul");
                        break;
                    case '8':
                        vm.p.push("Aug");
                        break;
                    case '9':
                        vm.p.push("Sep");
                        break;
                    case '10':
                        vm.p.push("Oct");
                        break;
                    case '11':
                        vm.p.push("Nov");
                        break;
                    case '12':
                        vm.p.push("Dec");
                }
            });
        };



        yearFunc();
        defaultreportSet009();

    }
}());
