

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController006",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'Excel', '$timeout', '$modal',
            ReportController006]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController006($scope, $state, viewModelHelper, $stateParams, validator, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report006-view';
        vm.viewName = 'Report006';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.year = [
                 { value: "2018", name: "2018" },
                 { value: "2017", name: "2017" },
                 { value: "2016", name: "2016" },
                 { value: "2015", name: "2015" },
                 { value: "2014", name: "2014" },
                 { value: "2013", name: "2013" },
                 { value: "2012", name: "2012" },
                 { value: "2011", name: "2011" },
                 { value: "2010", name: "2010" },
                 { value: "2009", name: "2009" }
        ];

        vm.period = [
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

        vm.currency = [{ id: "1", name: "Local" }, { id: "2", name: "Foreign" }];

        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.currencymodel2 = "";
        vm.directionmodel2 = "";
        vm.levelselectmodel2 = "";
        vm.officemodel2 = "";
        vm.pathmodel2 = "";
        vm.periodmodel2 = "";
        vm.staffmodel2 = "";
        vm.topmodel2 = "";
        vm.typemodel2 = "";
        vm.unitmodel2 = "";
        vm.yearmodel2 = "";
        

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';

        //vm.reportId = $scope.reports[0].id;  //initializing so as to have the value on pageload
        //$scope.reportId = $stateParams.ParameterKey;  //initializing so as to have the value on pageload
        vm.reportId = $stateParams.ParameterKey;

        vm.reportSet006 = function () {
            vm.reportId = vm.reportId;
            vm.currencymodel2 = vm.currencymodel;
            vm.directionmodel2 = vm.directionmodel;
            vm.levelselectmodel2 = vm.levelselectmodel;
            vm.officemodel2 = vm.officemodel;
            vm.pathmodel2 = "Deposit Mobilization";
            vm.periodmodel2 = vm.periodmodel;
            vm.staffmodel2 = vm.staffmodel;
            vm.topmodel2 = vm.topmodel;
            vm.typemodel2 = "Deposit";
            vm.unitmodel2 = vm.unitmodel;
            vm.yearmodel2 = vm.yearmodel;
        }

    }
}());
