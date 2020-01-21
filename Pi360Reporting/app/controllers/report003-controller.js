

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController003",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
                ReportController003]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController003($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report003-view';
        vm.viewName = 'Report003';
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

        var dateFunc = function (d) {
            var theDate = null;
            var dYear = new Date(d).getFullYear();
            var dMonth = new Date(d).getMonth() + 1;
            var dDate = new Date(d).getDate();
            if (dMonth < 10) { dMonth = "0" + dMonth; }
            if (dDate < 10) { dDate = "0" + dDate; }
            theDate = dYear + '-' + dMonth + '-' + dDate;

            return theDate;
        };

        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.yearmodel2 = null;
        vm.periodmodel2 = null;
        vm.rundatemodel2_B = null;
        //vm.rundatemodel2 = null;
        //vm.rundatemodel = null;   ///// not used in the new update
        vm.rundatemodel = new Date();   ////// used in the new update
        //vm.rundatemodel = dateFunc(vm.rundatemodel);
        vm.rundatemodel2 = dateFunc(vm.rundatemodel);


        $scope.getSelectItems = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                //outpNew += value.RunDate + ",";
                n = n + 1;
                if (n < data.length) { outpNew += value.RunDate + ","; }
                else { outpNew += value.RunDate; }   
            });
            vm.rundatemodel2_B = outpNew;
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

        var latestyearAndperiodFunc = function () {
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    ////initializing
                    vm.yearmodel = vm.yearperiodObj.Year.toString();
                    vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel2 = vm.yearperiodObj.Year;
                    vm.periodmodel2 = vm.yearperiodObj.Period.toString();

                    //vm.rundatemodel2 = "2017-6-10";

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

        vm.rundateList = [];
        vm.yrDefault = '2019';
        //vm.yrDefault = vm.yearmodel;

        var rundateFunc = function (yr) {
            yr = vm.yrDefault;
            vm.rundateList = [];
            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/rundate/' + yr, null,

                function (result) {
                    vm.rundateList = result.data;
                    ////////vm.rundatemodel = vm.rundateList.RunDate;
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
                    ////////vm.rundatemodel = vm.rundateList.RunDate;
                },
                function (result) {

                }, null);
        };



        vm.currentStatus = null;
        //vm.currentPeriod = 0;
        vm.setPeriods = null;
        vm.lockPeriods = [];
        vm.currentYear = 0;
        vm.loggedindefcode = null;

        var defaultreportSet003 = function () {

            displaycalenderORmultiSelectDDFunc();
            vm.currentStatus = null;
            latestyearAndperiodFunc();
            rundateFunc();  ///////////////// not used in d new update 
            

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
                        //menuobjFunc();
                        vm.pathmodel2 = 'null';
                        //latestyearAndperiodFunc();
                        //vm.rundatemodel2 = vm.rundatemodel;   ////////// used in d new update  
                        vm.rundatemodel2 = dateFunc(vm.rundatemodel2);
                    }
                },
                function (result) {

                }, null);
        };

        vm.periodmodel3 = 0;
        vm.reportSet003 = function () {

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
                        vm.pathmodel2 = 'null';
                        //////////vm.rundatemodel2 = vm.rundatemodel2_B;  ////////// not used in d new update 
                        ////////////vm.rundatemodel2 = vm.rundatemodel;    //////// used in d new update                   
                        vm.periodmodel3 = vm.periodmodel3 + 1;

                        if (vm.reportId === "Daily") {
                            vm.rundatemodel2 = dateFunc(vm.rundatemodel);    //////// used in d new update 
                        }
                        else {
                            vm.rundatemodel2 = vm.rundatemodel2_B;  ////////// not used in d new update 
                        }
                    }
                },
                function (result) {

                }, null);

        };

        //var dateFunc = function (d) {
        //    var myDate = new Date(d);
        //    myDate = myDate.toString();
        //    return myDate.substr(0, 10);
        //};

        //var dateFunc = function (d) {
        //    var theDate = null;
        //    var dYear = new Date(d).getFullYear();
        //    var dMonth = new Date(d).getMonth() + 1;
        //    var dDate = new Date(d).getDate();
        //    if (dMonth < 10) { dMonth = "0" + dMonth; }
        //    if (dDate < 10) { dDate = "0" + dDate; }
        //    theDate = dYear + '-' + dMonth + '-' + dDate;

        //    return theDate;
        //};

        vm.displaycalender = false;
        vm.displaymultiSelectDD = false;

        var displaycalenderORmultiSelectDDFunc = function () {
            if (vm.reportId === 'Daily') { vm.displaycalender = true; }
            else { vm.displaymultiSelectDD = true; }
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
            document.getElementById("report003").hidden = vm.tToggle;
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
        defaultreportSet003();

    }
}());
