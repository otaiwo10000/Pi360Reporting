"use strict";

var commonModule = angular.module('fintrakCommon', ['ngRoute', 'ui.bootstrap']);

var App = angular.module('fintrak', ['ngRoute', 'ui.bootstrap', 'ui.router', 'oc.lazyLoad', 'ngResource',
    'fintrakCommon', 'colorpicker.module', 'ngCsvImport', 'ngLoadingSpinner', 'ks.ngScrollRepeat', 'treeGrid',
    'ngIdleTimer', 'ngSanitize', 'ngCsv', 'ngJsonExportExcel', 'ngTable', 'ngInputDate']);
//, 'moment-picker', 'textAngular'
App.config(function ($provide) {
    $provide.decorator("$exceptionHandler",
        ["$delegate",
            function ($delegate) {
                return function (exception, cause) {
                    exception.message = "Please contact the Help Desk! \n Message: " +
                                                            exception.message;
                    $delegate(exception, cause);
                    alert(exception.message);
                };
            }]);
});

//Http Intercpetor to check auth failures for xhr requests
App.config(['$httpProvider',
  function ($httpProvider) {
      $httpProvider.interceptors.push('httpErrorResponseInterceptor');
  }
]);
App.config(function ($stateProvider, $urlRouterProvider, $locationProvider ) {
    //
    // For any unmatched url, redirect to /state1
    //var rootUrl = '/ClientPortal/';
    var rootUrl = '';


    //$urlRouterProvider.otherwise("/core-userprofile-list");
    $urlRouterProvider.otherwise("/landingpage");
    //$urlRouterProvider.otherwise("account/login");

    //
    // Now set up the states
    $stateProvider

       .state('landingpage', {
           url: "/landingpage",
           templateUrl: rootUrl + '/app/views/landingpage-view.html',
           controller: 'LandingPageController as vm'

       }).state('dashboarddeposit', {
           //url: '/report002/:ParameterKey',
           url: "/dashboarddeposit/:urldashboardparam",
           templateUrl: rootUrl + '/app/views/dashboarddeposit-view.html',
           controller: 'DashboardDepositController as vm'

       }).state('dashboardratios', {
           //url: "/dashboardratios",
           url: "/dashboardratios/:urldashboardparam",
           templateUrl: rootUrl + '/app/views/dashboardratios-view.html',
           controller: 'DashboardRatiosController as vm'

       }).state('dashboardexpense', {
           //url: "/dashboardexpense",
           url: "/dashboardexpense/:urldashboardparam",
           templateUrl: rootUrl + '/app/views/dashboardexpense-view.html',
           controller: 'DashboardExpenseController as vm'

       }).state('dashboardaccount', {
           url: "/dashboardaccount/:urldashboardparam",
           templateUrl: rootUrl + '/app/views/dashboardaccount-view.html',
           controller: 'DashboardAccountController as vm'

        }).state('dashboardbrs', {
            url: "/dashboardbrs/:urldashboardparam",
            templateUrl: rootUrl + '/app/views/dashboardbrowser-view.html',
            controller: 'DashboardBrowserController as vm'

        }).state('dashboardriskasset', {
            url: "/dashboardriskasset/:urldashboardparam",
            templateUrl: rootUrl + '/app/views/dashboardriskasset-view.html',
            controller: 'DashboardRiskAssetController as vm'

        }).state('dashboardincome', {
            url: "/dashboardincome/:urldashboardparam",
            templateUrl: rootUrl + '/app/views/dashboardincome-view.html',
            controller: 'DashboardIncomeController as vm'

        }).state('dashboardcustomercrm', {
            url: "/dashboardcustomercrm/:urldashboardparam",
            templateUrl: rootUrl + '/app/views/dashboardcustomercrm-view.html',
            controller: 'DashboardCustomerCRMController as vm'

       }).state('report001', {
           url: '/report001/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report001-view.html',
           controller: 'ReportController001 as vm'

       }).state('report002', {
           url: '/report002/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report002-view.html',
           controller: 'ReportController002 as vm'

       }).state('report003', {
           url: '/report003/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report003-view.html',
           controller: 'ReportController003 as vm'

       }).state('report004', {
           url: '/report004/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report004-view.html',
           controller: 'ReportController004 as vm'

       }).state('report005', {
           url: '/report005/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report005-view.html',
           controller: 'ReportController005 as vm'

       }).state('report006', {
           url: '/report006/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report006-view.html',
           controller: 'ReportController006 as vm'

       }).state('report007', {
           url: '/report007/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report007-view.html',
           controller: 'ReportController007 as vm'

       }).state('report008', {
           url: '/report008/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report008-view.html',
           controller: 'ReportController008 as vm'

       }).state('report009', {
           url: '/report009/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report009-view.html',
           controller: 'ReportController009 as vm'

       }).state('report010', {
           url: '/report010/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report010-view.html',
           controller: 'ReportController010 as vm'

       }).state('report011', {
           url: '/report011/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report011-view.html',
           controller: 'ReportController011 as vm'

       }).state('report012', {
           url: '/report012/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report012-view.html',
           controller: 'ReportController012 as vm'

       }).state('report014', {
           url: '/report014/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report014-view.html',
           controller: 'ReportController014 as vm'

       }).state('report015', {
           url: '/report015/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report015-view.html',
           controller: 'ReportController015 as vm'

       }).state('report016', {
           url: '/report016/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report016-view.html',
           controller: 'ReportController016 as vm'

       }).state('report017', {
           url: '/report017/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report017-view.html',
           controller: 'ReportController017 as vm'

       }).state('report018', {
           url: '/report018/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report018-view.html',
           controller: 'ReportController018 as vm'

       }).state('report019', {
           url: '/report019/:ParameterKey',
           templateUrl: rootUrl + '/app/views/report019-view.html',
           controller: 'ReportController019 as vm'

        }).state('report020', {
            url: '/report020/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report020-view.html',
            controller: 'ReportController020 as vm'

        }).state('report021', {
            url: '/report021/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report021-view.html',
            controller: 'ReportController021 as vm'

        }).state('report022', {
            url: '/report022/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report022-view.html',
            controller: 'ReportController022 as vm'

        }).state('report023', {
            url: '/report023/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report023-view.html',
            controller: 'ReportController023 as vm'

        }).state('report024', {
            url: '/report024/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report024-view.html',
            controller: 'ReportController024 as vm'

        }).state('report025', {
            url: '/report025/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report025-view.html',
            controller: 'ReportController025 as vm'

        }).state('report026', {
            url: '/report026/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report026-view.html',
            controller: 'ReportController026 as vm'

        }).state('report027', {
            url: '/report027/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report027-view.html',
            controller: 'ReportController027 as vm'

        }).state('report028', {
            url: '/report028/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report028-view.html',
            controller: 'ReportController028 as vm'

        }).state('report029', {
            url: '/report029/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report029-view.html',
            controller: 'ReportController029 as vm'

        }).state('report030', {
            url: '/report030/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report030-view.html',
            controller: 'ReportController030 as vm'

        }).state('report031', {
            url: '/report031/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report031-view.html',
            controller: 'ReportController031 as vm'

        }).state('report032', {
            url: '/report032/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report032-view.html',
            controller: 'ReportController032 as vm'

        }).state('report033', {
            url: '/report033/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report033-view.html',
            controller: 'ReportController033 as vm'

        }).state('report034', {
            url: '/report034/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report034-view.html',
            controller: 'ReportController034 as vm'

        }).state('report035', {
            url: '/report035/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report035-view.html',
            controller: 'ReportController035 as vm'

        }).state('report036', {
            url: '/report036/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report036-view.html',
            controller: 'ReportController036 as vm'

 }).state('report037', {
            url: '/report037/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report037-view.html',
            controller: 'ReportController037 as vm'

 }).state('report038', {
            url: '/report038/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report038-view.html',
            controller: 'ReportController038 as vm'

        }).state('report101', {
            url: '/report101/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report101-view.html',
            controller: 'ReportController101 as vm'

        }).state('report102', {
            url: '/report102/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report102-view.html',
            controller: 'ReportController102 as vm'

        }).state('report103', {
            url: '/report103/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report103-view.html',
            controller: 'ReportController103 as vm'

        }).state('report104', {
            url: '/report104/:ParameterKey',
            templateUrl: rootUrl + '/app/views/report104-view.html',
            controller: 'ReportController104 as vm'

        }).state('excelfiledownload', {
            url: '/excelfiledownload',
            templateUrl: rootUrl + '/app/views/excelfiledownload-view.html',
            controller: 'ExcelFileDownLoadController as vm'

       }).state('carousel', {
           url: '/carousel',
           templateUrl: rootUrl + '/app/views/carousel-view.html',
           controller: 'CarouselDemoCtrl as vm'

       }).state('modalctrl', {
           url: "/modalctrl",
           templateUrl: rootUrl + '/app/views/modalctrl-view.html',
           controller: 'ModalCtrl as vm'

        }).state('modalctrlwmb', {
            url: "/modalctrlwmb",
            templateUrl: rootUrl + '/app/views/modalctrlWMB-view.html',
            controller: 'ModalCtrlWMB as vm'

        }).state('modalctrlabp', {
            url: "/modalctrlabp",
            templateUrl: rootUrl + '/app/views/modalctrlABP-view.html',
            controller: 'ModalCtrlABP as vm'

        }).state('modalctrllbic', {
            url: "/modalctrllbic",
            templateUrl: rootUrl + '/app/views/modalctrlLBIC-view.html',
            controller: 'ModalCtrlLBIC as vm'
        });
    //$locationProvider.html5Mode(true);
});

App.controller('AppController', function ($scope, $rootScope, $routeParams, $location, $http, viewModelHelper) {

    $scope.userProfile = {};

    $scope.showMessage = function (title, message, type, includeDialog) {
        if (type === 'success') {
            toastr.success(message, title);
        } else if (type === 'error') {
            toastr.error(message, title);
        } else if (type === 'warning') {
            toastr.warning(message, title);
        } else if (type === 'info') {
            toastr.info(message, title);
        }

        if (includeDialog)
            alert(message);
    };

    var loadProfile = function () {
        //viewModelHelper.apiGet('api/account/getuserprofile', null,
        //          function (result) {
        //              $scope.userProfile = result.data;
        //          },
        //           function (result) {
        //               toastr.error('Fail to load user data', 'Fintrak');
        //           }, null);
    };

    loadProfile();
});






//============ The menu controller filter =================================================================

//App.controller('MenuController', function ($scope, $rootScope, $routeParams, $location, $http, viewModelHelper) {

//    //var employees = [
//    //         { name: "ben", gender: "Male" },
//    //         { name: "ben2", gender: "Male" },
//    //         { name: "ben3", gender: "Female" },
//    //         { name: "ben4", gender: "Male" }
//    //];
//    //$scope.employees = employees;

//    $scope.reports = [
//      {
//          id: 'Balancesheet',
//          name: 'Balancesheet',
//          isSelected: true
//      },
//        {
//            id: 'BalanceSheetBreakDown',
//            name: 'BalanceSheetBreakDown',
//            isSelected: false
//        },
//    ];

//    $scope.reportId = $scope.reports[0].id;

//    $scope.openReport = function (reportId) {
//        $scope.reportId = reportId;

//    }

//})
//.filter('trustAsResourceUrl', ['$sce', function ($sce) {
//    return function (val) {
//        return $sce.trustAsResourceUrl(val);
//    };
//}]);

//=========================================================

//App.controller('MenusController', function ($scope, $rootScope, $routeParams, $modal, $location, $http, viewModelHelper) {
//    var vm = this;
//    vm.viewModelHelper = viewModelHelper;
//    vm.parentController = $scope.$parent;

//    $scope.menus = [];

//    var menuFunc = function () {
//        //if (vm.init === false) {
//        vm.viewModelHelper.apiGet('api/menu/menusubmenu', null,
//                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
//                function (result) {
//                    $scope.menus = result.data
//                },
//                function (result) {
//                    // toastr.error('Fail to load data.', 'Fintrak');
//                }, null);
//    }

    
//    $rootScope.menuFunc2 = function () {
//        $scope.menus = [];
//        menuFunc();
//        //$modalInstance.close();
//        $rootScope.okGlobal();
//    };


//    menuFunc();


//});

//App.controller('test1controller', function ($scope, $rootScope, $routeParams, yearsfactory, $modal, $location, $http, viewModelHelper) {
  
//    yearsfactory.yearsFunc('/api/teamstructure/years').
//        then(function (data) {
//            //alert(data);
//            vm.yearList2 = data;
//            alert(vm.yearList2);
//        }).
//        catch(function (result) {
//            alert("Got error");
//        });
//});




// Services attached to the commonModule will be available to all other Angular modules.
commonModule.factory('viewModelHelper', function ($http, $q) {
    return Fintrak.viewModelHelper($http, $q);
});

commonModule.factory('validator', function () {
    return valJs.validator();
});

(function (se) {
    var viewModelHelper = function ($http, $q) {

        var self = this;

        self.modelIsValid = true;
        self.modelErrors = [];
        self.isLoading = false;

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.get(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        };

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading = true;
            self.modelIsValid = true;
            $http.post(Fintrak.rootPath + uri, data)
                .then(function (result) {
                    success(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                }, function (result) {
                    if (failure === null) {
                        if (result.status !== 400)
                            self.modelErrors = [result.status + ':' + result.statusText + ' - ' + result.data.Message];
                        else
                            self.modelErrors = [result.data.Message];
                        self.modelIsValid = false;
                    }
                    else
                        failure(result);
                    if (always !== null)
                        always();
                    self.isLoading = false;
                });
        };

        return this;
    };
    se.viewModelHelper = viewModelHelper;
}(window.Fintrak));

(function (se) {
    var mustEqual = function (value, other) {
        return value === other;
    };
    se.mustEqual = mustEqual;
}(window.Fintrak));

// ***************** validation *****************

window.valJs = {};

(function (val) {
    var validator = function () {

        var self = this;

        self.PropertyRule = function (propertyName, rules) {
            var self = this;
            self.PropertyName = propertyName;
            self.Rules = rules;
        };

        self.ValidateModel = function (model, allPropertyRules) {
            var errors = [];
            var props = Object.keys(model);
            for (var i = 0; i < props.length; i++) {
                var prop = props[i];
                for (var j = 0; j < allPropertyRules.length; j++) {
                    var propertyRule = allPropertyRules[j];
                    if (prop === propertyRule.PropertyName) {
                        var propertyRules = propertyRule.Rules;

                        var propertyRuleProps = Object.keys(propertyRules);
                        for (var k = 0; k < propertyRuleProps.length; k++) {
                            var propertyRuleProp = propertyRuleProps[k];
                            if (propertyRuleProp !== 'custom') {
                                var rule = rules[propertyRuleProp];
                                var params = null;
                                if (propertyRules[propertyRuleProp].hasOwnProperty('params'))
                                    params = propertyRules[propertyRuleProp].params;
                                var validationResult = rule.validator(model[prop], params);
                                if (!validationResult) {
                                    errors.push(getMessage(prop, propertyRuleProp, rule.message));
                                }
                            }
                            else {
                                var validator = propertyRules.custom.validator;
                                var value = null;
                                if (propertyRules.custom.hasOwnProperty('params')) {
                                    value = propertyRules.custom.params;
                                }
                                var result = validator(model[prop], value());
                                if (result !== true) {
                                    errors.push(getMessage(prop, propertyRules.custom, 'Invalid value.'));
                                }
                            }
                        }
                    }
                }
            }

            model['errors'] = errors;
            model['isValid'] = (errors.length === 0);
        };

        var getMessage = function (prop, rule, defaultMessage) {
            var message = '';
            if (rule.hasOwnProperty('message'))
                message = rule.message;
            else
                message = prop + ': ' + defaultMessage;
            return message;
        };

        var rules = [];

        var setupRules = function () {

            rules['required'] = {
                validator: function (value, params) {
                    return !(value.toString().trim() === '');
                },
                message: 'Value is required 2.'
            };
            rules['notZero'] = {
                validator: function (value, params) {
                    return !(value === 0);
                },
                message: 'Value is must be greater than zero.'
            };
            rules['mostBePercentage'] = {
                validator: function (value, params) {
                    return !(value < 0);
                },
                message: 'Value must be greater than or equal zero.'
            };
            rules['mustBeDate'] = {
                validator: function (value, params) {
                    return (isDate(value));
                },
                message: 'Value is must be a valid date.'
            };
            rules['mustBeNumeric'] = {
                validator: function (value, params) {
                    return (isNumber(value));
                },
                message: 'Value is must be a valid number.'
            };
            rules['minLength'] = {
                validator: function (value, params) {
                    return !(value.toString().trim().length < params);
                },
                message: 'Value does not meet minimum length.'
            };
            rules['pattern'] = {
                validator: function (value, params) {
                    var regExp = new RegExp(params);
                    return !(regExp.exec(value.toString().trim()) === null);
                },
                message: 'Value must match regular expression.'
            };
        };

        function isDate(sDate) {
            if (sDate === null)
                return false;

            var scratch = new Date(sDate);
            if (scratch.toString() === 'NaN' || scratch.toString() === 'Invalid Date') {
                return false;
            } else {
                return true;
            }
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        setupRules();

        return this;
    }
    val.validator = validator;
}(window.valJs));

App.controller('NoneController', function ($scope, $routeParams) {

});

App.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

App.directive('kpiFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateKPIFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    };
}]);

App.directive('scoreFormular', ['$rootScope', function ($rootScope) {
    return {
        link: function (scope, element, attrs) {
            $rootScope.$on('updateScoreFormular', function (e, val) {
                var domElement = element[0];

                if (document.selection) {
                    domElement.focus();
                    var sel = document.selection.createRange();
                    sel.text = val;
                    domElement.focus();
                } else if (domElement.selectionStart || domElement.selectionStart === 0) {
                    var startPos = domElement.selectionStart;
                    var endPos = domElement.selectionEnd;
                    var scrollTop = domElement.scrollTop;
                    domElement.value = domElement.value.substring(0, startPos) + val + domElement.value.substring(endPos, domElement.value.length);
                    domElement.focus();
                    domElement.selectionStart = startPos + val.length;
                    domElement.selectionEnd = startPos + val.length;
                    domElement.scrollTop = scrollTop;
                } else {
                    domElement.value += val;
                    domElement.focus();
                }

            });
        }
    };
}]);

App.factory('httpErrorResponseInterceptor', ['$q', '$location',
  function ($q, $location) {
      return {
          response: function (responseData) {
              return responseData;
          },
          responseError: function error(response) {
              switch (response.status) {
                  case 401:
                      $location.path('/login');
                      break;
                  case 404:
                      $location.path('/404');
                      break;
                  default:
                      alert(response.data);
                      //$location.path('/error');
              }

              return $q.reject(response);
          }
      };
  }
]);



App.factory('Excel', function ($window) {
    var uri = 'data:application/vnd.ms-excel;base64,',
        template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
        base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
        format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
    return {
        tableToExcel: function (tableId, worksheetName) {
            var table = $(tableId),
                ctx = { worksheet: worksheetName, table: table.html() },
                href = uri + base64(format(template, ctx));
            return href;
        }
    };
});


//App.service('yearsService', function ($http, $rootScope) {
App.service('yearsService', function ($http, $rootScope) {
    return {
        yearsFunc: function () {
            $rootScope.loading = true;
            var req = {
                method: 'GET',
                //url: url
                url: '/api/teamstructure/years'
            };

            return $http(req).then(function (result) {
                return result.data;
            });
        }
    };
});


App.service('displayReportService', function ($http, $rootScope) {
        return {
            reportFunc: function (elementName) {
               
                return setTimeout(function () {
                    document.getElementById(elementName).style.display = 'block';
                }, 10000);

            }
        };
    });



//App.service('periodService', function ($rootScope) {
//    return {
//        periodFunc: function (peri) {
//            $rootScope.loading = true;

//            var vm = this;
//            vm.p = [];
//            angular.forEach(peri, function (value, key) {

//                switch (value) {
//                    case 1:
//                        vm.p.push("Jan");
//                        break;
//                    case 2:
//                        vm.p.push("Feb");
//                        break;
//                    case 3:
//                        vm.p.push("Mar");
//                        break;
//                    case 4:
//                        vm.p.push("Apr");
//                        break;
//                    case 5:
//                        vm.p.push("May");
//                        break;
//                    case 6:
//                        vm.p.push("Jun");
//                        break;
//                    case 7:
//                        vm.p.push("Jul");
//                        break;
//                    case 8:
//                        vm.p.push("Aug");
//                        break;
//                    case 9:
//                        vm.p.push("Sep");
//                        break;
//                    case 10:
//                        vm.p.push("Oct");
//                        break;
//                    case 11:
//                        vm.p.push("Nov");
//                        break;
//                    case 12:
//                        vm.p.push("Dec");
//                }
//                return vm.p;
//            });
//        }
//    };
//});


//App.service('yearsfactory4', function ($http, $q) {
//    this.yearsFunc = function () {
//        var deferred = $q.defer();
//        $http({
//            method: 'GET',
//            url: '/api/teamstructure/years'
//        }).
//            success(function (data, status, headers, config) {
//                //deferred.resolve(data);
//                deferred.resolve(data);
//            }).
//            error(function (data, status) {
//                //deferred.reject(data);
//                deferred.reject(data);
//            });

//        return deferred.promise;
//    }
//});




