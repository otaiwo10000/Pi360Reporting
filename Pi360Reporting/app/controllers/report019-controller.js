

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController019",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
            ReportController019]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController019($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report019-view';
        vm.viewName = 'Report019';

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


        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.productTypeList = [];


        vm.directionmodel = "top";
        vm.directionmodel2 = vm.directionmodel;
        vm.pathmodel2 = null;
        vm.periodmodel2 = null;
        vm.producttypemodel = "--All Product--";
        vm.rankingmodel = "CreditCount";
        vm.rankingmodel2 = vm.rankingmodel;
        vm.statemodel = "monthly";
        vm.statemodel2 = vm.statemodel;
        vm.topmodel = 10;
        vm.topmodel2 = 10;
        vm.typemodel2 = null;
        vm.yearmodel2 = 0;

        vm.startdatemodel = '2019-01-31';
        vm.startdatemodel2 = vm.startdatemodel;

        vm.enddatemodel = '2019-01-31';
        vm.enddatemodel2 = vm.enddatemodel;

        vm.menuObj = {};
        vm.yearperiodObj = {};
        vm.reportId = $stateParams.ParameterKey;


        var latestyearAndperiodFunc = function () {
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    
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

        var APRcaptionreportFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/APRdistinctcaptionreport', null,
                function (result) {
                    vm.productTypeList = result.data;
                },
                function (result) {

                }, null);
        };

        vm.rankingList = [];
        vm.Profitability = 'Turnover';

        var RankingFunc = function () {
            vm.viewModelHelper.apiGet('api/reportprocedure/ranking/' + vm.Profitability, null,
                function (result) {
                    vm.rankingList = result.data;
                },
                function (result) {

                }, null);
        };


        vm.rundateList = [];
        //vm.yrDefault = '2018';
        vm.yrDefault = '2019';
        

        var rundateFunc = function (yr) {
            vm.rundateList = [];
            yr = vm.yrDefault;
            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/rundate/' + yr, null,

                function (result) {
                    vm.rundateList = result.data;
                },
                function (result) {

                }, null);
        };

        vm.rundateFunc2 = function (yr) {
            vm.rundateList = [];
            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/rundate/' + yr, null,

                function (result) {
                    vm.rundateList = result.data;
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

        var defaultreportSet019 = function () {

            vm.currentStatus = '';
            latestyearAndperiodFunc();
            rundateFunc();
            RankingFunc();
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
                        vm.pathmodel2 = null;
                        //latestyearAndperiodFunc();
                        APRcaptionreportFunc();

                        vm.directionmodel = "top";
                        vm.directionmodel2 = "top";
                        vm.producttypemodel = "--All Product--";
                        vm.producttypemodel2 = "--All Product--";
                        vm.rankingmodel = vm.rankingmodel;
                        vm.rankingmodel2 = vm.rankingmodel;
                        vm.statemodel = "monthly";
                        vm.statemodel2 = "monthly";
                        vm.topmodel = 10;
                        vm.topmodel2 = 10;
                        vm.typemodel2 = "null";

                        //vm.rundatemodel2 = vm.rundatemodel;

                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
                        rundateFunc();
                    }

                },
                function (result) {

                }, null);
        };

        vm.periodmodel3 = 0;
        vm.reportSet019 = function () {

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
                        APRcaptionreportFunc();
                        vm.directionmodel2 = vm.directionmodel;
                        vm.pathmodel2 = null;
                        vm.periodmodel2 = vm.periodmodel;
                        vm.producttypemodel2 = vm.producttypemodel;
                        vm.rankingmodel2 = vm.rankingmodel;
                        vm.statemodel2 = vm.statemodel;
                        vm.topmodel2 = vm.topmodel;
                        vm.typemodel2 = $stateParams.ParameterKey;
                        vm.yearmodel2 = vm.yearmodel;

                        //vm.rundatemodel2 = vm.rundatemodel;

                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
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
            document.getElementById("report019").hidden = vm.tToggle;
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
        defaultreportSet019();


    }
}());
