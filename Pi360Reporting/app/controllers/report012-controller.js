

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController012",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
            ReportController012]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController012($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

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

        vm.reportlevelList = [
            { value: "directorate", name: "Directorate" },
            { value: "region", name: "Region" },
            { value: "division", name: "Division" },
            { value: "branch", name: "Branch" },
            { value: "accountOfficer", name: "AccountOfficer" }
        ];

        vm.stateList = [
                { value: "monthly", name: "Monthly" },
                { value: "ytd", name: "Year to Date" }
        ];

        vm.reportTypeList = [{ value: "BS", name: "Balancesheet" }, { value: "PL", name: "Income" }, { value: "SC", name: "Other MPR Captions" }];
        
		vm.reporttypemodel = 'BS';
        vm.reporttypemodel2 = 'BS';

        vm.newArray = [];

        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.maincaptionList = [];
        //vm.captionList = [];


        vm.view = 'report012-view';
        vm.viewName = 'Report012';
        vm.menuObj = {};
        vm.yearperiodObj = {};

        vm.reportId = $stateParams.ParameterKey;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';        

        vm.periodmodel2 = null;
        vm.yearmodel2 = 0;
        vm.maincaptionmodel = "AGRIC LOANS";
       
        vm.reportlevelmodel = "directorate";
       

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

        var defaultreportSet012 = function () {

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
                        //menuobjFunc();
                        vm.pathmodel2 = "null";
                        maincaptiontypeFunc();
                        //latestyearAndperiodFunc();
                        vm.reporttypemodel2 = 'BS';
                        vm.maincaptionmodel2 = "AGRIC LOANS";
                        vm.reportlevelmodel2 = "directorate";
                    }

                },
                function (result) {

                }, null);
        };

        vm.periodmodel3 = 0;
        vm.reportSet012 = function () {

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
                        menuobjFunc();
                        vm.reportId = vm.reportId;
                        // vm.pathmodel2 = vm.menuObj.ReportPAth;
                        vm.pathmodel2 = "null";
                        vm.periodmodel2 = vm.periodmodel;
                        vm.reportlevelmodel2 = vm.reportlevelmodel;
                        vm.reporttypemodel2 = vm.reporttypemodel;
                        vm.maincaptionmodel2 = vm.maincaptionmodel;
                        vm.yearmodel2 = vm.yearmodel;
                        vm.periodmodel3 = vm.periodmodel3 + 1;
                    }

                },
                function (result) {

                }, null);

        };


        $scope.getSelectItems = function (data) {
            var outpNew = "";
            angular.forEach(data, function (value, key) {
                outpNew += value.fiName + ",";
            });
            vm.maincaptionmodel = outpNew;
        };

        var maincaptiontypeFunc = function () {

            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/maincaptionreport/' + vm.reporttypemodel, null,

                function (result) {
                    vm.maincaptionList = result.data;
                },
                function (result) {

                }, null);
        };

      
        vm.maincaptiontypeFunc2 = function (capt) {
            //vm.captiontypemodel = capt;
            vm.maincaptionList = [];

            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
            vm.viewModelHelper.apiGet('api/reportprocedure/maincaptionreport/' + capt, null,

                function (result) {
                    vm.maincaptionList = result.data;
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
            document.getElementById("report012").hidden = vm.tToggle;
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
        defaultreportSet012();

        //$scope.para = { "pat": "John", "type": "Doe" , "cur": "ddd"};
        ////$http.post('<%= ResolveUrl("~/default.aspx/postjson") %>', $scope.user)
        //vm.viewModelHelper.apiPost('<%= ResolveUrl("~/Viewer.aspx/Page_Load") %>', $scope.para, null);
    }
}());
