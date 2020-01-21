
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ModalCtrl",
        //['$scope', '$state', 'viewModelHelper', '$modal', '$modalInstance', '$rootScope', 'validator', 'Excel', '$timeout',
        ['$rootScope', '$scope', '$state', 'viewModelHelper', '$stateParams', '$modal', '$modalInstance', 'validator', 'Excel', '$timeout',
            ModalCtrl]);



    //function ModalCtrl($scope, $state, viewModelHelper, $modal, $modalInstance, $rootScope, validator, Excel, $timeout) {
    function ModalCtrl($rootScope, $scope, $state, viewModelHelper, $stateParams, $modal, $modalInstance, validator, Excel, $timeout) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.view = 'modalctrl-view';
        vm.viewName = 'modal box ';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        //$scope.finallySelectedLevelCodeDropDown = null;

        vm.teamstructuredata = [];
        vm.yearmodel = "2018";
        vm.ts = [];

        vm.DirectorateList = [];
        vm.RegionList = [];
        vm.DivisionList = [];
        vm.BranchList = [];
        vm.AccountOfficerList = [];

        //vm.totalbank = 'NULL'; vm.directorate = 'NULL'; vm.region = 'NULL'; vm.division = 'NULL'; vm.branch = 'NULL'; vm.acctofficer = 'NULL';


        vm.directorate = 'NULL'; vm.region = 'NULL'; vm.division = 'NULL'; vm.branch = 'NULL'; vm.acctofficer = 'NULL';
        vm.searchval = 'NULL';

        vm.selectioncontrollevel = '';
        vm.searchvalue = 'NULL';
        vm.selectedmiscode = 'NULL';
        vm.selectedmisname = 'NULL';
        vm.selectedyear = '2018';
        vm.selectedlevel = 'NULL';
        $scope.finallyselectedmiscode = 'NULL';


        //vm.dir = "4"; vm.reg = "R1016"; vm.div = ""; vm.brh = ""; vm.acct = ""; N
        //vm.dir = '4'; vm.reg = 'R1016'; vm.div = ''; vm.brh = ''; vm.acct = '';



        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        //vm.totalbankDropDown = [{ Code: "bnk", Name: "totalbank" }];
        vm.totalbankDropDown = [{ Code: "select", Name: "select" }, { Code: "bnk", Name: "totalbank" }];

        vm.totalbank = "bnk";

        vm.year = [
                  { value: "2018", name: "2018" },
                  { value: "2017", name: "2017" },
                  { value: "2016", name: "2016" },
                  { value: "2015", name: "2015" },
                  { value: "2014", name: "2014" }
        ];


        //=========================== dropdownlist for pageload starts ==================================================================

        //vm.totalbankdisplay = false;
        vm.totalbank = "";

        vm.TotalbankList = [];
        vm.DirectorateList_page = [];
        vm.RegionList_page = [];
        vm.DivisionList_page = [];
        vm.BranchList_page = [];
        vm.AccountOfficerList_page = [];

        vm.tsOnPageLoadFunc = function () {

            latestyearAndperiodFunc();
            vm.DirectorateList = [];
            vm.RegionList = [];
            vm.DivisionList = [];
            vm.BranchList = [];
            vm.AccountOfficerList = [];
            vm.TotalbankList = [];
            //vm.totalbank = "bnk";
            ////vm.totalbankdisplay = true;

            vm.directorate = null; vm.region = null; vm.division = null; vm.branch = null; vm.acctoffice = null;

            //var bnkvar = $scope.miscodeGlobalData;
            ////vm.showtotalbank = 'false';
            ////if (bnkvar.toLowerCase() === 'bnk' || bnkvar.toLowerCase() === 'totalbank')
            //if (bnkvar !== 'bnk' || bnkvar !== 'totalbank')
            //{
            //    vm.totalbank = "";
            //    vm.totalbankDropDown = [];
            //}


            vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebymiscodelevelyear', null,
                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                function (res) {
                    vm.teamstructuredata = res.data;

                    angular.forEach(vm.teamstructuredata, function (value, key) {
                        //vm.newArray.push({
                        //    DirectorateList: value.DirectorateList,
                        //    RegionList: value.RegionList,
                        //    DivisionList: value.DivisionList,
                        //    BranchList: value.BranchList,
                        //    AccountOfficerList: value.AccountOfficerList
                        //});

                        angular.forEach(value.TotalbankList, function (a, b) {
                            vm.TotalbankList.push({ Code: a.Code, Name: a.Name });
                        });

                        angular.forEach(value.DirectorateList, function (a, b) {
                            vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                        });

                        angular.forEach(value.RegionList, function (c, d) {
                            vm.RegionList.push({ Code: c.Code, Name: c.Name });
                        });

                        angular.forEach(value.DivisionList, function (e, f) {
                            vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                        });

                        angular.forEach(value.BranchList, function (i, j) {
                            vm.BranchList.push({ Code: i.Code, Name: i.Name });
                        });

                        angular.forEach(value.AccountOfficerList, function (i, j) {
                            vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                        });

                    });

                    //vm.DirectorateList_page = vm.DirectorateList;
                    //vm.RegionList_page = vm.RegionList;
                    //vm.DivisionList_page = vm.DivisionList;
                    //vm.BranchList_page = vm.BranchList;
                    //vm.AccountOfficerList_page = vm.AccountOfficerList;

                    ////toastr.success('BalanceSheets data loaded.', 'Fintrak');
                    ////InitialView();
                    ////vm.init === true;
                },
                function (result) {
                    //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);

            // }
        };


        //=========================== dropdownlist for pageload ends ==================================================================

        //=========================== at an instant dropdown selection event, effect on every other dropdownlist starts (B) ==================================================================

        vm.tsOnSelectFunc = function (miscode, level, misname) {
            vm.selectedmiscode = miscode;
            vm.selectedlevel = level;
            vm.selectedmisname = misname;

            if (vm.selectedlevel === '0') {
                $scope.finallySelectedLevelCodeDropDown = 'BNK';

                vm.DirectorateList.length = 0;
                //    vm.DirectorateList = [];				 
                //    vm.RegionList = [];
                //    vm.DivisionList = [];
                //    vm.BranchList = [];
                //    vm.AccountOfficerList = [];

                //vm.DirectorateList = vm.DirectorateList_page

                ////$scope.finallyselectedmiscode = vm.selectedmiscode;


                //// if (vm.init === false) {

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + vm.selectedmisname, null,
                    //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                    function (res) {
                        vm.teamstructuredata = res.data;

                        //vm.TotalbankList.length = 0;
                        vm.DirectorateList.length = 0;
                        vm.RegionList.length = 0;
                        vm.DivisionList.length = 0;
                        vm.BranchList.length = 0;
                        vm.AccountOfficerList.length = 0;

                        angular.forEach(vm.teamstructuredata, function (value, key) {

                            angular.forEach(value.TotalbankList, function (c, d) {
                                vm.TotalbankList.push({ Code: c.Code, Name: c.Name });
                            });

                            angular.forEach(value.DirectorateList, function (a, b) {
                                //  if (vm.DirectorateList.indexOf(a.Code) == -1) {  //get unique /distict values of a.period
                                //    vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                                //}
                                vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                            });

                            angular.forEach(value.RegionList, function (c, d) {
                                vm.RegionList.push({ Code: c.Code, Name: c.Name });
                            });

                            angular.forEach(value.DivisionList, function (e, f) {
                                vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                            });

                            angular.forEach(value.BranchList, function (i, j) {
                                vm.BranchList.push({ Code: i.Code, Name: i.Name });
                            });

                            angular.forEach(value.AccountOfficerList, function (i, j) {
                                vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                            });

                        });

                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);

            }

            else if (vm.selectedlevel === '5') {
                $scope.finallySelectedLevelCodeDropDown = 'DIR';
                vm.RegionList = [];
                vm.DivisionList = [];
                vm.BranchList = [];
                vm.AccountOfficerList = [];
                vm.totalbank = "";
                //$scope.finallyselectedmiscode = vm.selectedmiscode;

                // if (vm.init === false) {

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + vm.selectedmisname, null,
                    //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                    function (res) {
                        vm.teamstructuredata = res.data;

                        angular.forEach(vm.teamstructuredata, function (value, key) {

                            //vm.DirectorateList.push({ Code: vm.selectedmiscode, Name: vm.selectedmisname });

                            angular.forEach(value.RegionList, function (c, d) {
                                vm.RegionList.push({ Code: c.Code, Name: c.Name });
                            });

                            angular.forEach(value.DivisionList, function (e, f) {
                                vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                            });

                            angular.forEach(value.BranchList, function (i, j) {
                                vm.BranchList.push({ Code: i.Code, Name: i.Name });
                            });

                            angular.forEach(value.AccountOfficerList, function (i, j) {
                                vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                            });

                        });

                        //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                        //InitialView();
                        //vm.init === true;
                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);
            }

            else if (vm.selectedlevel === '4') {
                $scope.finallySelectedLevelCodeDropDown = 'REG';
                vm.DivisionList = [];
                vm.BranchList = [];
                vm.AccountOfficerList = [];
                vm.totalbank = "";
                //$scope.finallyselectedmiscode = vm.selectedmiscode;

                // if (vm.init === false) {

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + encodeURIComponent(vm.selectedmisname), null,
                    //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                    function (res) {
                        vm.teamstructuredata = res.data;

                        angular.forEach(vm.teamstructuredata, function (value, key) {

                            angular.forEach(value.DivisionList, function (e, f) {
                                vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                            });

                            angular.forEach(value.BranchList, function (i, j) {
                                vm.BranchList.push({ Code: i.Code, Name: i.Name });
                            });

                            angular.forEach(value.AccountOfficerList, function (i, j) {
                                vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                            });

                        });

                        //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                        //InitialView();
                        //vm.init === true;
                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);
            }

            else if (vm.selectedlevel === '6') {
                $scope.finallySelectedLevelCodeDropDown = 'DIV';
                vm.BranchList = [];
                vm.AccountOfficerList = [];
                vm.totalbank = "";
                //$scope.finallyselectedmiscode = vm.selectedmiscode;

                // if (vm.init === false) {

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + vm.selectedmisname, null,
                    //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                    function (res) {
                        vm.teamstructuredata = res.data;

                        angular.forEach(vm.teamstructuredata, function (value, key) {

                            angular.forEach(value.BranchList, function (i, j) {
                                vm.BranchList.push({ Code: i.Code, Name: i.Name });
                            });

                            angular.forEach(value.AccountOfficerList, function (i, j) {
                                vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                            });

                        });

                        //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                        //InitialView();
                        //vm.init === true;
                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);
            }

            else if (vm.selectedlevel === '2') {
                $scope.finallySelectedLevelCodeDropDown = 'BRH';
                vm.AccountOfficerList = [];
                vm.totalbank = "";
                //$scope.finallyselectedmiscode = vm.selectedmiscode;

                // if (vm.init === false) {

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + vm.selectedmisname, null,
                    //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                    function (res) {
                        vm.teamstructuredata = res.data;

                        angular.forEach(vm.teamstructuredata, function (value, key) {

                            angular.forEach(value.AccountOfficerList, function (i, j) {
                                vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                            });

                        });

                        //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                        //InitialView();
                        //vm.init === true;
                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);
            }

            else if (vm.selectedlevel === '1') {
                $scope.finallySelectedLevelCodeDropDown = 'ACCT';
                vm.totalbank = "";
                //vm.AccountOfficerList = [];
                ////$scope.finallyselectedmiscode = vm.selectedmiscode;

                vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel + '/' + vm.selectedmisname, null,
                    function (res) {
                        //vm.teamstructuredata = res.data;

                        //angular.forEach(vm.teamstructuredata, function (value, key) {

                        //    angular.forEach(value.AccountOfficerList, function (i, j) {
                        //        vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                        //    });

                        //});
                    },
                    function (result) {
                        //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                    }, null);
            }

        };

        //=========================== at an instant dropdown selection event, effect on every other dropdownlist ends ==================================================================



        //=========================== at an instant dropdown selection event, effect on every other dropdownlist starts (A) ==================================================================

        vm.tsOnSelectFunc_2 = function (miscode, level) {
            vm.selectedmiscode = miscode;
            vm.selectedlevel = level;

            vm.DirectorateList = [];
            vm.RegionList = [];
            vm.DivisionList = [];
            vm.BranchList = [];
            vm.AccountOfficerList = [];
            //$scope.finallyselectedmiscode = vm.selectedmiscode;

            // if (vm.init === false) {

            vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.selectedmiscode + '/' + vm.selectedyear + '/' + vm.selectedlevel, null,
                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                function (res) {
                    vm.teamstructuredata = res.data;

                    angular.forEach(vm.teamstructuredata, function (value, key) {

                        angular.forEach(value.DirectorateList, function (a, b) {
                            vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                        });

                        angular.forEach(value.RegionList, function (c, d) {
                            vm.RegionList.push({ Code: c.Code, Name: c.Name });
                        });

                        angular.forEach(value.DivisionList, function (e, f) {
                            vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                        });

                        angular.forEach(value.BranchList, function (i, j) {
                            vm.BranchList.push({ Code: i.Code, Name: i.Name });
                        });

                        angular.forEach(value.AccountOfficerList, function (i, j) {
                            vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                        });

                    });

                    //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                    //InitialView();
                    //vm.init === true;
                },
                function (result) {
                    //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);

            // }
        };

        //=========================== at an instant dropdown selection event, effect on every other dropdownlist ends ==================================================================



        //=================== teamstructure by miscode starts ==================================================================
        vm.misSelectionBysearchvalue = function (mcode) {

            // if (vm.init === false) {

            vm.DirectorateList = [];
            vm.RegionList = [];
            vm.DivisionList = [];
            vm.BranchList = [];
            vm.AccountOfficerList = [];

            vm.searchvalue = mcode;


            vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.searchvalue, null,
                //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
                function (res) {
                    vm.teamstructuredata = res.data;

                    angular.forEach(vm.teamstructuredata, function (value, key) {

                        angular.forEach(value.DirectorateList, function (a, b) {
                            vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                        });

                        angular.forEach(value.RegionList, function (c, d) {
                            vm.RegionList.push({ Code: c.Code, Name: c.Name });
                        });

                        angular.forEach(value.DivisionList, function (e, f) {
                            vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                        });

                        angular.forEach(value.BranchList, function (i, j) {
                            vm.BranchList.push({ Code: i.Code, Name: i.Name });
                        });

                        angular.forEach(value.AccountOfficerList, function (i, j) {
                            vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                        });

                    });

                    //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                    //InitialView();
                    //vm.init === true;
                },
                function (result) {
                    //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);

            // }
        };

        //=================== teamstructure by miscode ends ==================================================================


        //=================== non-initialize function starts ============================================================

        vm.getteamselectionFunc = function () {

            vm.DirectorateList = [];
            vm.RegionList = [];
            vm.DivisionList = [];
            vm.BranchList = [];
            vm.AccountOfficerList = [];


            //var dir = '4'; var reg = 'R1016'; var div = ''; var brh = ''; var acct = '';
            // if (vm.init === false) {

            //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebymiscodelevelyear', null,
            //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
            vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.directorate + '/' + vm.region + '/' + vm.division + '/' + vm.branch + '/' + vm.acctofficer, null,
                //vm.viewModelHelper.apiGet('api/teamstructure/teamstructurebyselection/' + vm.dir + '/' + vm.reg + '/' + vm.div+ '/' + vm.brh + '/' + vm.acct, null,

                function (res) {
                    vm.teamstructuredata = res.data;


                    angular.forEach(vm.teamstructuredata, function (value, key) {
                        //vm.newArray.push({
                        //    DirectorateList: value.DirectorateList,
                        //    RegionList: value.RegionList,
                        //    DivisionList: value.DivisionList,
                        //    BranchList: value.BranchList,
                        //    AccountOfficerList: value.AccountOfficerList
                        //});

                        angular.forEach(value.DirectorateList, function (a, b) {
                            vm.DirectorateList.push({ Code: a.Code, Name: a.Name });
                        });

                        angular.forEach(value.RegionList, function (c, d) {
                            vm.RegionList.push({ Code: c.Code, Name: c.Name });
                        });

                        angular.forEach(value.DivisionList, function (e, f) {
                            vm.DivisionList.push({ Code: e.Code, Name: e.Name });
                        });

                        angular.forEach(value.BranchList, function (i, j) {
                            vm.BranchList.push({ Code: i.Code, Name: i.Name });
                        });

                        angular.forEach(value.AccountOfficerList, function (i, j) {
                            vm.AccountOfficerList.push({ Code: i.Code, Name: i.Name });
                        });

                    });

                    //toastr.success('BalanceSheets data loaded.', 'Fintrak');
                    //InitialView();
                    //vm.init === true;
                },
                function (result) {
                    //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
                }, null);

            // }
        };




        //=========================== dropdownlist methods starts ==================================================================

        //vm.getSegment_New = function (Code) {

        //    var LevelId = Code;

        //    vm.viewModelHelper.apiGet('api/usermis/getusermisdetail/' + $scope.usermisId + '/' + LevelId, null,
        //        function (result) {
        //            //vm.userMIS = result.data.UserMIS;
        //            vm.userClassifications = result.data.Classifications;
        //        },
        //        function (result) {
        //            toastr.error(result.data, 'Fintrak');
        //        }, null);
        //}

        vm.regFunc = function (svalue) {

            vm.searchval = '';  //NOTE: clear the contents of vm.searchvalue first bcos it is used by each dropdown at a time.
            vm.searchval = svalue;
            vm.RegionList = '';

            vm.viewModelHelper.apiGet('api/teamstructure/regionbydirectorate/' + vm.searchval, null,
                function (result) {
                    vm.RegionList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.divFunc = function (svalue) {

            vm.searchval = '';  //NOTE: clear the contents of vm.searchvalue first bcos it is used by each dropdown at a time.
            vm.searchval = svalue;
            vm.DivisionList = '';

            vm.viewModelHelper.apiGet('api/teamstructure/divisionbyregion/' + vm.searchval, null,
                function (result) {
                    vm.DivisionList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.brhFunc = function (svalue) {

            vm.searchval = '';  //NOTE: clear the contents of vm.searchvalue first bcos it is used by each dropdown at a time.
            vm.searchval = svalue;
            vm.BranchList = '';

            vm.viewModelHelper.apiGet('api/teamstructure/branchbydivision/' + vm.searchval, null,
                function (result) {
                    vm.BranchList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        vm.acctFunc = function (svalue) {

            vm.searchval = '';  //NOTE: clear the contents of vm.searchvalue first bcos it is used by each dropdown at a time.
            vm.searchval = svalue;
            vm.AccountOfficerList = '';

            vm.viewModelHelper.apiGet('api/teamstructure/acctbybrh/' + vm.searchval, null,
                function (result) {
                    vm.AccountOfficerList = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

        //=========================== dropdownlist methods ends ==================================================================

        //=================== non-initialize function ends ==================================================================

        $scope.dashboarddepositGlobalFunc_childmethod = function () {
            $rootScope.$emit("dashboarddepositGlobalFunc_ParentMethod", {});
        };

        $scope.dashboardexpenseGlobalFunc_childmethod = function () {
            $rootScope.$emit("dashboardexpenseGlobalFunc_ParentMethod", {});
        };

        $scope.dashboardratiosGlobalFunc_childmethod = function () {
            $rootScope.$emit("dashboardratiosGlobalFunc_ParentMethod", {});
        };

        $scope.dashboardaccountGlobalFunc_childmethod = function () {
            $rootScope.$emit("dashboardaccountGlobalFunc_ParentMethod", {});
        };

        vm.uirouteparam = $stateParams.urldashboardparam;

        $rootScope.okGlobal = function () {

            // $scope.finallySelectedLevelCodeDropDown = $scope.finallySelectedLevelCodeDropDown;
            $rootScope.updateMenu($scope.finallySelectedLevelCodeDropDown);

            switch (vm.uirouteparam) {
                case 'deposits':
                    $scope.dashboarddepositGlobalFunc_childmethod();
                    break;

                case 'expenses':
                    $scope.dashboardexpenseGlobalFunc_childmethod();
                    break;

                case 'ratios':
                    $scope.dashboardratiosGlobalFunc_childmethod();
                    break;

                case 'accounts':
                    $scope.dashboardaccountGlobalFunc_childmethod();
            }


            //$scope.dashboarddepositGlobalFunc_childmethod();
            //$scope.dashboardexpenseGlobalFunc_childmethod();
            //$scope.dashboardratiosGlobalFunc_childmethod();


            $modalInstance.close();
        };

        vm.ok = function () {
            $modalInstance.close();
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        vm.delete = function () {
            $modalInstance.dismiss('cancel');
        };

        var latestyearAndperiodFunc = function () {
            //vm.periodmodel = 0;
            //vm.yearmodel = 0;
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;

                    vm.selectedyear = vm.yearperiodObj.Year.toString();
                    //vm.periodmodel = vm.yearperiodObj.Period.toString();
                },
                function (result) {

                }, null);
        };


        //var selectioncontrollevelFunc = function () {

        //    // if (vm.init === false) {

        //    vm.viewModelHelper.apiGet('api/home/levelcode', null,

        //        function (res) {

        //            vm.selectioncontrollevel = res.data;

        //            if (vm.selectioncontrollevel == "ACCT")
        //                vm.selectioncontrollevel = 1
        //            else if (vm.selectioncontrollevel == "BRH")
        //                vm.selectioncontrollevel = 2
        //            else if (vm.selectioncontrollevel == "DIV")
        //                vm.selectioncontrollevel = 6
        //            else if (vm.selectioncontrollevel == "REG")
        //                vm.selectioncontrollevel = 2
        //            else if (vm.selectioncontrollevel == "DIR")
        //                vm.selectioncontrollevel = 1
        //            else if (vm.selectioncontrollevel == "BNK")
        //                vm.selectioncontrollevel = 0

        //        },
        //        function (result) {
        //            //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //        }, null);

        //    // }
        //};

        //=================== non-initialize function ends ==================================================================

        //vm.sessindataObj = {};
        //vm.miscode = 'null';
        ////$scope.miscodeGlobalData = vm.miscode;
        //$scope.miscodeGlobalData = vm.sessindataObj;


        //$rootScope.sesssionVariableFunc = function () {
        //    $scope.miscodeGlobalData = '';
        //    vm.viewModelHelper.apiGet('api/sessiondata/sessionvariables', null,
        //           //vm.viewModelHelper.apiGet('api/mprbalancesheet/availablemprbalancesheet/' + vm.number + '/' + vm.RunDate.toDateString() + '/' + vm.ToDate.toDateString(), null,
        //           function (res) {
        //               vm.sessindataObj = res.data;

        //               //angular.forEach(vm.sessindataObj, function (a, b) {
        //               //    vm.miscode.push(a.MISCode);
        //               //});
        //           },
        //           function (result) {
        //               //toastr.error('Fail to load BalanceSheets data.', 'Fintrak');
        //           }, null);
        //}


        ////$rootScope.finallySelectedLevelCodeDropDownFunc = function () {
        ////    $scope.menus = [];
        ////    $rootScope.okGlobal();  //calling from "app/controllers/modalctrl-controllers.js".
        ////};


        ////selectioncontrollevelFunc();
        ////initialize();
        ////getteamselectionFunc();

        vm.tsOnPageLoadFunc();

    }
}());
