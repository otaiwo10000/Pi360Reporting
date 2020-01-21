
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SessionVariableController",
        ['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
            SessionVariableController]);

    function SessionVariableController($scope, $state, $rootScope, viewModelHelper, $modal, $modalInstance, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
       
        //vm.view = 'modalctrl-view';
        //vm.viewName = 'modal box ';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];


        //vm.sessindataObj = {};
        ////$scope.miscodeGlobalData = 'null';
     
        // $rootScope.sesssionVariableFunc = function () {
        //     $scope.miscodeGlobalData = '';
        //     vm.viewModelHelper.apiGet('api/sessiondata/sessionvariables', null,
        //            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
        //            function (res) {
        //                vm.sessindataObj = res.data;

        //                angular.forEach(vm.sessindataObj, function (a, b) {                           
        //                    $scope.miscodeGlobalData.push(a.MISCode);
        //                });
        //            },
        //            function (result) {
        //                //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //            }, null);               
        //}




    }
}());
