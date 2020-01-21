

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CEO2DashboardController",
        ['$scope', '$state', 'viewModelHelper', '$modal', 'validator', 'Excel', '$timeout',
            CEO2DashboardController]);

    function CEO2DashboardController($scope, $state, viewModelHelper, $modal, $modalInstance, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'ceo2dashboard-view';
        vm.viewName = 'Dashboard';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
       


        vm.outputT1 = [];
        vm.outputD1 = [];

        vm.D1Subcaptions = [];
        vm.D1ASubcaptions = [];
        vm.D1BSubcaptions = [];

        vm.D1AAmounts = [];

        vm.D1Data = [];
        vm.D1Name = [];

        vm.D1ListItems = [];

        vm.D1ListMainCaption = [];
        vm.D1Listsubobj = [];

        var dFunc = function () {
            vm.viewModelHelper.apiGet('api/landingpage/mix', null,
                function (res) {
                    vm.outputT1 = res.data;
                    //vm.outputD1 = res;
                    //vm.D1Data = vm.outputT1.data;
                   // vm.D1SubCaptions = vm.D1Data.SubCaption;
                    //vm.D1ListItems = vm.outputT1.D1List;

                    //vm.D1ListMainCaption = vm.D1ListItems.MainCaption;
                    //vm.D1Listsubobj = vm.D1ListItems.subobj;

                    angular.forEach(vm.outputT1, function (a, b) {
                        angular.forEach(a.D1List, function (a2, b2) {
                            vm.D1ListMainCaption.push(a2.MainCaption);
                            vm.D1Listsubobj.push(a2.subobj);
                            //vm.D1Listsubobj.push({ subobj: a2.subobj });

                            angular.forEach(a2.subobj, function (c, d) {
                                vm.D1ASubcaptions.push(c.SubCaption);
                                vm.D1AAmounts.push(c.Amount);
                            });
                        });
                    });

                    totalAccountMixFunc();

                },
                function (result) {
                    // toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);
        }


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

       
        var totalAccountMixFunc = function () {
            //dFunc();

            Highcharts.chart('totalaccountmix', {
                chart: {
                    type: 'column',
                    //backgroundColor: 'dodgerblue',
                    options2d: {
                        enabled: true,
                        alpha: 10,
                        beta: 25,
                        depth: 70
                    }
                },
                title: {
                    text: 'Total Account Mix'
                },
                subtitle: {
                    //text: 'Notice the difference between a 0 value and a null point'
                },
                plotOptions: {
                    column: {
                        depth: 25
                    }
                },
                xAxis: {
                    //categories: Highcharts.getOptions().lang.shortMonths,
                    //categories: ['ACTIVE ACCOUNTS', 'DORMANT ACCOUNTS', 'INACTIVE ACCOUNTS'],
                    categories: vm.D1ASubcaptions,
                    labels: {
                        skew3d: true,
                        style: {
                            fontSize: '16px'
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: 'Amount'
                    }
                },
                //series: [{
                //    //name: 'Sales',
                //    //data: [560720.000000000000, 356525.000000000000, 1659202.000000000000]
                //}]

                series: [{
                    //name: 'Sales',
                    data: vm.D1AAmounts
                }]
                
            });
        };


        
        dFunc();
        //totalAccountMixFunc();
        
    }
}());
