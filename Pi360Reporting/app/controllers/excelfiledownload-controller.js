
//correct order of dependencies: ['$rootScope', '$scope', '$http', '$location', '$localStorage', 'loginService'

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExcelFileDownLoadController",
        //['$scope', '$state', '$rootScope', 'viewModelHelper', 'validator', '$modal', 'Excel', '$timeout',
        ['$scope', '$state', 'viewModelHelper', 'validator', '$rootScope', 'Excel', '$timeout', '$stateParams',
            ExcelFileDownLoadController]);
    

    function ExcelFileDownLoadController($scope, $state, viewModelHelper, validator, $rootScope, $routeParams, $location, $http, Excel, $timeout, $stateParams) {

    
       

    }
}());
