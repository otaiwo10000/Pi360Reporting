

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController002",
            ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
                ReportController002]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController002($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report2-view';
        vm.viewName = 'Report2';

        vm.menuObj = {};
        vm.yearperiodObj = {};
        vm.reportId = $stateParams.ParameterKey;


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

        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.achievementsmodel = "All";
        vm.rolesmodel = "All";

        vm.periodmodel = "0";
        vm.yearmodel = "0";

        vm.yearmodel2 = "0";
        vm.periodmodel2 = "0";

        vm.achievementsmodel2 = "";
        vm.pathmodel2 = "";
        //vm.periodmodel2 = "";
        vm.rolesmodel2 = "";
        //vm.yearmodel2 = "";

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';

        //vm.reportId = $scope.reports[0].id;  //initializing so as to have the value on pageload
        //$scope.reportId = $stateParams.ParameterKey;  //initializing so as to have the value on pageload
        //        vm.reportId = $stateParams.ParameterKey;

        //$scope.openReport = function (reportId) {
        //    //$scope.reportId = reportId;
        //    vm.reportId = reportId;
        //}

        ////$scope.getBalanceSheet = function () {
        //vm.reportSet002 = function () {
        //    vm.reportId = vm.reportId;
        //    vm.achievementsmodel2 = "1";
        //    vm.pathmodel2 = vm.menuObj.ReportPAth;
        //    vm.periodmodel2 = vm.periodmodel;
        //    vm.rolesmodel2 = "1";
        //    vm.yearmodel2 = vm.yearmodel;            
        //}

        //var defaultreportSet002 = function () {
        //    vm.reportId = vm.reportId;
        //    menuobjFunc();
        //    latestyearAndperiodFunc();
        //    vm.achievementsmodel2 = "1";
        //    vm.rolesmodel2 = "1";

        //}

        //-------- the below function is used for the "achievement" control parameter --------------------
        vm.scorecardmetricList = [];
        vm.yearstring = '2019';

        var ScoreCardMetricFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/scorecardmetric/' + vm.yearstring, null,
                function (result) {
                    vm.scorecardmetricList = result.data;
                },
                function (result) {

                }, null);
        };

        var ScoreCardMetricFunc2 = function (valu) {

            vm.viewModelHelper.apiGet('api/reportprocedure/scorecardmetric/' + valu, null,
                function (result) {
                    vm.scorecardmetricList = result.data;
                },
                function (result) {

                }, null);
        };

        //-------- the below function is used for the "role" control parameter --------------------
        vm.scorecardroleList = [];

        var ScoreCardRolesFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/scorecardroles', null,
                function (result) {
                    vm.scorecardroleList = result.data;
                },
                function (result) {

                }, null);
        };


        $scope.getSelectItems = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                n = n + 1;
                if (n < data.length) { outpNew += value.name + ","; }
                else { outpNew += value.name; }
            });
            vm.periodmodel = outpNew;
        };

        var latestyearAndperiodFunc = function () {
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    ////initializing
                    vm.yearmodel = vm.yearperiodObj.Year.toString();
                    vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel2 = vm.yearperiodObj.Year.toString();
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

        var defaultreportSet002 = function () {

            vm.currentStatus = '';
            latestyearAndperiodFunc();
            ScoreCardMetricFunc();
            ScoreCardRolesFunc();
            displaysingleORmultiSelectDDFunc();
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
                    //if (vm.currentStatus === "OFF" && vm.loggedindefcode !== "SA") {
                    //    alert("Report for the periods ( " + vm.reportStatusObj.Period + " ) are being processed");
                    //}
                    if (vm.currentStatus === "OFF" && vm.loggedindefcode !== "SA") {
                        //alert("Report for the periods ( " + vm.reportStatusObj.Period + " ) are being processed");
                        periodFunc(vm.lockPeriods);
                        alert("Report for the periods ( " + vm.p.join('/') + " ) are being processed");
                    }
                    else {
                        vm.reportId = vm.reportId;
                        //menuobjFunc();
                        vm.pathmodel2 = 'null';
                        //latestyearAndperiodFunc();
                        vm.achievementsmodel2 = "All";
                        vm.rolesmodel2 = "All";
                    }

                },
                function (result) {

                }, null);
        };


        vm.periodmodel3 = 0;
        vm.reportSet002 = function () {

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
                        vm.achievementsmodel2 = encodeURIComponent(vm.achievementsmodel);
                        //vm.pathmodel2 = vm.menuObj.ReportPAth;
                        vm.pathmodel2 = 'null';
                        vm.periodmodel2 = vm.periodmodel;
                        vm.rolesmodel2 = vm.rolesmodel;
                        vm.yearmodel2 = vm.yearmodel;
                        vm.periodmodel3 = vm.periodmodel3 + 1;
                    }
                },
                function (result) {

                }, null);
        };


        vm.displaysingleSelectDD = false;
        vm.displaymultiSelectDD = false;

        var displaysingleORmultiSelectDDFunc = function () {
            if (vm.reportId === 'Trend_ScoreCard_Consolidated') { vm.displaysingleSelectDD = true; }
            else if (vm.reportId === 'Trend_ScoreCard_ConsolidatedYTD') { vm.displaymultiSelectDD = true; }
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
            document.getElementById("report002").hidden = vm.tToggle;
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
        defaultreportSet002();


    }
}());
