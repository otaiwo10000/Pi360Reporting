

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController030",
            ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
                ReportController030]);


    function ReportController030($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.reportId = $stateParams.ParameterKey;


        vm.menuObj = {};
        vm.yearperiodObj = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.yearList = [];

       
        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.yearmodel = 0;
        vm.periodmodel = 0;
        vm.startdatemodel = "";
        vm.enddatemodel = "";
        vm.metricmodel = "";
        //vm.metrictypemodel = "";
        //vm.reporttypemodel = "";
        vm.pathmodel = "test";
        vm.analysistypemodel = "";
        //vm.analysiscategorymodel = "";
        vm.sortordermodel = "";

        vm.yearmodel2 = 0;
        vm.periodmodel2 = 0;
        vm.startdatemodel2 = vm.startdatemodel;
        vm.enddatemodel2 = vm.enddatemodel;
        vm.metricmodel2 = vm.metricmodel;
        //vm.metrictypemodel2 = vm.metrictypemodel;
        //vm.reporttypemodel2 = vm.reporttypemodel;
        vm.pathmodel2 = vm.pathmodel;
        vm.analysistypemodel2 = vm.analysistypemodel;
        //vm.analysiscategorymodel2 = vm.analysiscategorymodel;
        vm.sortordermodel2 = vm.sortordermodel;

        //var metrictypeList = [{ value: "greater than", name: "Greater Than" }, { value: "less than", name: "Less Than" }];
        //var reporttypeList = [{ value: "account", name: "account" }, { value: "customer", name: "customer" }];
        vm.analysistypeList = [{ value: "All", name: "All" }, { value: "Deposit", name: "Deposit" }, { value: "Loan", name: "Loan" }];
        vm.sortorderList = [{ value: "Top", name: "Top" }, { value: "Bottom", name: "Bottom" }];
        //var analysiscategory = [];


        var latestyearAndperiodFunc = function () {
            vm.yearmodel = 0;
            vm.periodmodel = 0;
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    ////initializing
                    vm.yearmodel = vm.yearperiodObj.Year.toString();
                    vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel2 = vm.yearmodel;
                    vm.periodmodel2 = vm.periodmodel;

                    //vm.yearmodel2 = vm.yearperiodObj.Year;
                    //vm.periodmodel2 = vm.yearperiodObj.Period.toString();
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

        vm.dateList = [];
        vm.yrDefault = '2019';
        //vm.yrDefault = vm.yearmodel;

        var dateListFunc = function (yr) {
            yr = vm.yrDefault;
            vm.dateList = [];
            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/rundate2/' + yr, null,

                function (result) {
                    vm.dateList = result.data;
                    ////////vm.rundatemodel = vm.rundateList.RunDate;
                },
                function (result) {

                }, null);
        };

        vm.dateListFunc2 = function (yr) {
            vm.dateList = [];
            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/rundate2/' + yr, null,

                function (result) {
                    vm.dateList = result.data;
                    ////////vm.rundatemodel = vm.rundateList.RunDate;
                },
                function (result) {

                }, null);
        };


        vm.currentStatus = '';
        //vm.currentPeriod = 0;
        vm.setPeriods = null;
        vm.lockPeriods = [];
        vm.currentYear = 0;
        vm.loggedindefcode = '';

        var defaultreportSet030 = function () {

            vm.currentStatus = '';
            latestyearAndperiodFunc();
            dateListFunc();

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
                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
                        vm.metricmodel2 = vm.metricmodel;
                        //vm.metrictypemodel2 = vm.metrictypemodel;
                        //vm.reporttypemodel2 = vm.reporttypemodel;
                        vm.pathmodel2 = vm.pathmodel;
                        vm.analysistypemodel2 = vm.analysistypemodel;
                        //vm.analysiscategorymodel2 = vm.analysiscategorymodel;
                        vm.sortordermodel2 = vm.sortordermodel;
                    }
                },
                function (result) {

                }, null);
        };


        vm.periodmodel3 = 0;
        vm.reportSet030 = function () {

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
                        vm.yearmodel2 = vm.yearmodel;
                        vm.periodmodel2 = vm.periodmodel;
                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
                        vm.metricmodel2 = vm.metricmodel;
                        //vm.metrictypemodel2 = vm.metrictypemodel;
                        //vm.reporttypemodel2 = vm.reporttypemodel;
                        vm.pathmodel2 = vm.pathmodel;
                        vm.analysistypemodel2 = vm.analysistypemodel;
                        //vm.analysiscategorymodel2 = vm.analysiscategorymodel;
                        vm.sortordermodel2 = vm.sortordermodel;

                        vm.periodmodel3 = vm.periodmodel3 + 1;
                    }

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
            document.getElementById("report027").hidden = vm.tToggle;
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
        //defaultreportSet030();

    }
}());
