

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController104",
            ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'Excel', '$timeout', '$modal',
                ReportController104]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController104($scope, $state, viewModelHelper, $stateParams, validator, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;


        vm.view = 'report104-view';
        vm.viewName = 'Report104';

        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.reportId = $stateParams.ParameterKey;

        vm.currencyList = [
            { value: "All", name: "All Currencies" },
            { value: "Afghani", name: "Afghani" },
            { value: "Algerian Dinar", name: "Algerian Dinar" },
            { value: "Argentine Peso", name: "Argentine Peso" }
        ];

        vm.startdatemodel = new Date();
        vm.enddatemodel = new Date();

        vm.startdatemodel2 = vm.startdatemodel;
        vm.enddatemodel2 = vm.enddatemodel;

        vm.currencymodel = "All";
        vm.currencymodel2 = vm.currencymodel;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.act = true;

        var latestyearAndperiodFunc = function () {
            //vm.periodmodel = 0;
            //vm.yearmodel = 0;
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

        $scope.getSelectItems = function (data) {
            var outpNew = "";
            angular.forEach(data, function (value, key) {
                outpNew += value.fiName + ",";
            });
            vm.currencymodel = outpNew;
        };



        vm.currentStatus = '';
        //vm.currentPeriod = 0;
        vm.setPeriods = null;
        vm.lockPeriods = [];
        vm.currentYear = 0;
        vm.loggedindefcode = '';

        var defaultreportSet104 = function () {

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
                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
                        //vm.channelmodel2 = vm.channelmodel;
                        vm.currencymodel2 = vm.currencymodel;
                    }
                },
                function (result) {

                }, null);
        };

        vm.periodmodel3 = 0;
        vm.reportSet104 = function () {

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
                        vm.startdatemodel2 = vm.startdatemodel;
                        vm.enddatemodel2 = vm.enddatemodel;
                        //vm.channelmodel2 = vm.channelmodel;
                        vm.currencymodel2 = vm.currencymodel;
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
            document.getElementById("report104").hidden = vm.tToggle;
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


        defaultreportSet104();

        // vm.currentStatusFunc();



        //$scope.para = { "pat": "John", "type": "Doe" , "cur": "ddd"};
        ////$http.post('<%= ResolveUrl("~/default.aspx/postjson") %>', $scope.user)
        //vm.viewModelHelper.apiPost('<%= ResolveUrl("~/Viewer.aspx/Page_Load") %>', $scope.para, null);
    }
}());
