

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MenusController2",
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', '$modal', 'Excel', '$timeout',
            MenusController2]);

    function MenusController2($scope, $state, viewModelHelper, validator, $rootScope, $modal, $modalInstance, $routeParams, $location, $http, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'menus-view';
        vm.viewName = 'Menus';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        $scope.menus = [];
        vm.finallySelectedLevelCodeDropDown = null;
        $scope.finallySelectedLevelCodeDropDownInMenu = null;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var menuFunc = function () {

            //if (vm.init === false) {
            vm.viewModelHelper.apiGet('api/menu/menusubmenu', null,
                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                function (result) {
                    $scope.menus = result.data;
                    //customSesssionVariableFunc();
                },
                function (result) {
                    // toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };


        $rootScope.finallevelmenuFunc = function () {
            //$scope.menus = [];


            $rootScope.okGlobal();  //calling from "app/controllers/modalctrl-controllers.js".

            //$scope.miscodeGlobalValue = customSesssionVariableFactory.miscodeGlobalDataFunc();

            //if (vm.init === false) {
            //vm.viewModelHelper.apiGet('api/menu/menusubmenubylevelcode/' + 'DIV', null,
            vm.viewModelHelper.apiGet('api/menu/menusubmenubylevelcode/' + vm.finallySelectedLevelCodeDropDown, null,
                function (result) {
                    //$rootScope.okGlobal();  //calling from "app/controllers/modalctrl-controllers.js".
                    //$scope.menus = result.data;

                },
                function (result) {
                    // toastr.error('Fail to load data.', 'Fintrak');
                }, null);
            $scope.customSesssionVariableFunc_childmethod();
        };

        $rootScope.menuFunc2 = function () {
            $scope.menus = [];   
           // menuFunc();      // calling from this page.
            //$modalInstance.close();
            $rootScope.okGlobal();  //calling from "app/controllers/modalctrl-controllers.js".
        };

        $rootScope.updateMenu = function (fdd) {
            vm.finallySelectedLevelCodeDropDown = fdd;
        };

        var rootUrl = '';
        //vm.open = function () {
        $scope.globalopencall = function () {
           
            var modalInstance = $modal.open({
                backdrop: true,

                templateUrl: rootUrl + '/app/views/modalctrl-view.html',
                controller: 'ModalCtrl as vm',

                //templateUrl: rootUrl + '/app/views/modalctrlWMB-view.html',
                //controller: 'ModalCtrlWMB as vm',

                //templateUrl: rootUrl + '/app/views/modalctrlABP-view.html',
                //controller: 'ModalCtrlABP as vm'

                //templateUrl: rootUrl + '/app/views/modalctrlLBIC-view.html',
                //controller: 'ModalCtrlLBIC as vm'

                //windowClass: 'app-modal-window1'
            });
        };


        //$scope.sessiondataGlobalObj = {};
        ////$scope.test100 = 'test1';
        //$scope.miscodeGlobalData = '';
        //$scope.misnameGlobalData = '';
        $scope.customSesssionVariableFunc = function () {

            vm.viewModelHelper.apiGet('api/sessiondata/sessionvariables', null,
                function (result) {
                    var sessiondataGlobalObj = result.data;

                    //$scope.miscodeGlobalData = sessiondataGlobalObj.MISCode;
                    //$scope.misnameGlobalData = sessiondataGlobalObj.ReportUser;
                    //$scope.loggedinUserFullNameGlobal = sessiondataGlobalObj.LogOnUserFullName;

                    $scope.miscodeGlobalData = sessiondataGlobalObj.MISCode;                   
                    $scope.loggedinUserFullNameGlobal = sessiondataGlobalObj.LogOnUserFullName;
                    if (sessiondataGlobalObj.Level === 4) { $scope.misnameGlobalData = sessiondataGlobalObj.ReportUser + ' ' + 'Region'; }
                    else { $scope.misnameGlobalData = sessiondataGlobalObj.ReportUser;}                    
                    
                    ////$scope.levelcodeGlobalData = sessiondataGlobalObj.Levelcode;  //it's constant but it should not. it doesn't change but it shouls as new miscode is selected from the dropdown. THIS ONE NEEDS UPDATE, hence, it is commented
                },
                function (result) {
                    //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        };

        $rootScope.$on("customSesssionVariableFunc_ParentMethod", function () {
            $scope.customSesssionVariableFunc();
        });

        $scope.customSesssionVariableFunc_childmethod = function () {
            $rootScope.$emit("customSesssionVariableFunc_ParentMethod", {});
        };

        var mobilityMenuFunc = function () {

            vm.viewModelHelper.apiGet('api/menu/mobility', null,
                function (result) {
                    $scope.mobilityGlobalMenuList = result.data;
                },
                function (result) {
                    // toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };

        var echannelMenuFunc = function () {

            vm.viewModelHelper.apiGet('api/menu/echannel', null,
                function (result) {
                    $scope.echannelGlobalMenuList = result.data;
                },
                function (result) {
                    // toastr.error('Fail to load data.', 'Fintrak');
                }, null);
        };




        mobilityMenuFunc();
        echannelMenuFunc();

        $scope.customSesssionVariableFunc_childmethod();

        menuFunc();
      

    }
}());
