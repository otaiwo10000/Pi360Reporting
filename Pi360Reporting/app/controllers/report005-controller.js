

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController005",
        ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'Excel', '$timeout', '$modal',
            ReportController005]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController005($scope, $state, viewModelHelper, $stateParams, validator, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'report005-view';
        vm.viewName = 'Report005';

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

        vm.yearmodel2 = "";
        vm.periodmodel2 = "";
        vm.currencymodel2 = "";

        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';

        vm.reportId = $stateParams.ParameterKey;

        vm.reportSet005 = function () {
            vm.reportId = vm.reportId;
            vm.yearmodel2 = vm.yearmodel;
            vm.periodmodel2 = vm.periodmodel;
            vm.path = "Deposit Mobilization";
            vm.division = "division";
        }

    }
}());
