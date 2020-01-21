

(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportController001",
            ['$scope', '$state', 'viewModelHelper', '$stateParams', 'validator', 'yearsService', 'Excel', '$timeout', '$modal',
            ReportController001]);
    //.filter('trustAsResourceUrl', ['$sce', function ($sce) {
    //    return function (val) {
    //        return $sce.trustAsResourceUrl(val);
    //    };
    //}]);

    function ReportController001($scope, $state, viewModelHelper, $stateParams, validator, yearsService, Excel, $timeout, $modal, $modalInstance) {
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

        vm.scorecardtypeList = [
           { id: "monthly", name: "montly" },
           { id: "YTD", name: "Year to date" }
        ];


        vm.view = 'report001-view';
        vm.viewName = 'Report001';
        vm.menuObj = {};
        vm.yearperiodObj = {};  
        vm.tToggle = false;
        vm.toggleText = "collapse prompt";

        vm.reportId = $stateParams.ParameterKey;

       
        vm.yearmodel2 = 0;
        vm.periodmodel2 = 0;

        vm.periodmodel = "0";
        vm.yearmodel = "0";

        //vm.scorecardtypemodel = "monthly";

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];       
             
        //vm.init = false;
        //vm.showInstruction = false;
        //vm.instruction = '';        


        //$scope.reports = [          
        //    {
        //        id: 'Balancesheet',
        //        name: 'Balancesheet',
        //        isSelected: false
        //    },
        //    {
        //        id: 'BalanceSheetBreakDown',
        //        name: 'BalanceSheetBreakDown',
        //        isSelected: false
        //    },
        //];

        //vm.reportId = $scope.reports[0].id;  //initializing so as to have the value on pageload
        //$scope.reportId2 = $stateParams.ParameterKey;  //initializing so as to have the value on pageload
        

        //vm.path = "new description";
        //vm.type = "new bs";
        //vm.currency = "new currency";

         //$scope.openReport = function (reportId) {
         //    //$scope.reportId = reportId;
         //    vm.reportId = reportId;
        //}
      

        //vm.scorecardtypemodel2 = "monthly";
        //vm.scorecardtypemodel = "monthly";
        vm.segmentmodel2 = "all";
        vm.act = true;

       
       
        //var defaultreportSet001 = function () {
        //    vm.reportId = vm.reportId;            
        //    latestyearAndperiodFunc();            
        //    menuobjFunc();
        //    vm.scorecardtypemodel2 = "monthly";
        //    vm.scorecardtypemodel = "monthly";
        //    vm.segmentmodel2 = "all";
        //}

        var latestyearAndperiodFunc = function () {
            //vm.periodmodel = 0;
            //vm.yearmodel = 0;
            vm.viewModelHelper.apiGet('api/teamstructure/latestyearandperiod', null,
                function (result) {
                    vm.yearperiodObj = result.data;
                    ////initializing
                    //vm.yearmodel = vm.yearperiodObj.Year;
                    //vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel = vm.yearperiodObj.Year.toString();
                    vm.periodmodel = vm.yearperiodObj.Period.toString();

                    vm.yearmodel2 = vm.yearperiodObj.Year.toString();
                    vm.periodmodel2 = vm.yearperiodObj.Period.toString();
                    //vm.yearmodel2 = vm.yearmodel;
                    //vm.periodmodel2 = vm.periodmodel;
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

        //var yearFunc = function () {
        //    vm.viewModelHelper.apiGet('api/teamstructure/years', null,
        //        function (result) {
        //            vm.yearList = result.data;
        //        },
        //        function (result) {

        //        }, null);
        //};

        vm.currentStatus = null;
        //vm.currentPeriod = 0;
        vm.setPeriods = null;
        vm.lockPeriods = [];
        vm.currentYear = 0;
        vm.loggedindefcode = null;
        //vm.test = "10,3,5,12";
        
        var defaultreportSet001 = function () {

            vm.reportId = vm.reportId;
            //vm.currentStatus = '';
            latestyearAndperiodFunc();
            displaysingleORmultiSelectDDFunc();
          

            //displaysingleORmultiSelectDDFunc();
            //vm.viewModelHelper.apiGet('api/reportprocedure/reportstatus', null,
            vm.viewModelHelper.apiGet('api/reportprocedure/mprreportstatus', null,
                function (result) {
                    vm.reportStatusObj = result.data;

                    //vm.currentPeriod = vm.reportStatusObj.Period;
                    vm.lockPeriods = vm.reportStatusObj.Period.split(',');
                    //vm.lockPeriods = vm.test.split(',');
                    vm.currentYear = vm.reportStatusObj.Year;
                    vm.currentStatus = vm.reportStatusObj.ReportStatus;
                    vm.loggedindefcode = vm.reportStatusObj.loggedindefcode;

                    

                    //if (vm.currentStatus === "OFF" && (vm.loggedindefcode !== "BNK" && vm.loggedindefcode !== "SA")) {
                    if (vm.currentStatus === "OFF" && vm.loggedindefcode !== "SA") {
                       // ////alert("Report for the periods ( " + vm.reportStatusObj.Period  + " ) are being processed");
                     
                       periodFunc(vm.lockPeriods);
                        alert("Report for the periods ( " + vm.p.join('/') + " ) are being processed");
                    }
                    else {
                        ////vm.reportId = vm.reportId;
                        ////latestyearAndperiodFunc();
                        ////menuobjFunc();
                        ////vm.scorecardtypemodel2 = "monthly";
                        ////vm.scorecardtypemodel = "monthly";
                        ////latestyearAndperiodFunc(); 

                        vm.pathmodel2 = 'null';
                        vm.scorecardtypemodel2 = vm.scorecardtypemodel;
                        vm.segmentmodel2 = "all";
                    }

                },
                function (result) {

                }, null);
        };
        
        vm.periodmodel3 = 0;
        vm.reportSet001 = function () {

            //vm.reportId = vm.reportId;
            //vm.scorecardtypemodel2 = vm.scorecardtypemodel;
            //vm.segmentmodel2 = "all";
            //vm.yearmodel2 = vm.yearmodel;
            //vm.periodmodel2 = vm.periodmodel;
            //vm.pathmodel2 = 'null';
            //vm.periodmodel3 = vm.periodmodel3 + 1;
        
            //vm.viewModelHelper.apiGet('api/reportprocedure/reportstatus', null,
            vm.viewModelHelper.apiGet('api/reportprocedure/mprreportstatus', null,
                function (result) {
                    vm.reportStatusObj = result.data;

                    //vm.currentPeriod = vm.reportStatusObj.Period;
                    vm.lockPeriods = vm.reportStatusObj.Period.split(',');
                    vm.currentYear = vm.reportStatusObj.Year;
                    vm.currentStatus = vm.reportStatusObj.ReportStatus;
                    vm.loggedindefcode = vm.reportStatusObj.loggedindefcode;


                    //if (vm.currentStatus === "OFF" && vm.currentPeriod.toString() === vm.periodmodel.toString() && vm.currentYear.toString() === vm.yearmodel.toString() && vm.loggedindefcode !== 'SA') {
                    if (vm.currentStatus === "OFF" && (vm.lockPeriods.indexOf(vm.periodmodel.toString()) !== -1) && vm.currentYear.toString() === vm.yearmodel.toString() && vm.loggedindefcode !== 'SA') {
                        //alert("Report for the periods ( " + vm.reportStatusObj.Period + " ) are being processed");
                        periodFunc(vm.lockPeriods);
                        alert("Report for the periods ( " + vm.p.join('/') + " ) are being processed");
                    }
                    else {
                        //vm.reportId = $scope.reports[0].id;
                        vm.reportId = vm.reportId;
                        vm.scorecardtypemodel2 = vm.scorecardtypemodel;
                        vm.segmentmodel2 = "all";
                        vm.yearmodel2 = vm.yearmodel;
                        vm.periodmodel2 = vm.periodmodel;
                        vm.pathmodel2 = 'null';
                        vm.periodmodel3 = vm.periodmodel3 + 1;
                    }
                },
                function (result) {

                }, null);
        };


        vm.displaysingleSelectDD = false;
        vm.displaymultiSelectDD = false;

        var displaysingleORmultiSelectDDFunc = function () {
            if (vm.reportId === 'ScoreCard') { vm.displaysingleSelectDD = true; vm.scorecardtypemodel = 'monthly'; }
            else if (vm.reportId === 'ScoreCardYTD') { vm.displaymultiSelectDD = true; vm.scorecardtypemodel = 'YTD'; }
        };

        $scope.getSelectItems = function (data) {
            var outpNew = "";
            var n = 0;
            angular.forEach(data, function (value, key) {
                //outpNew += value.id + ",";
                n = n + 1;
                if (n < data.length) { outpNew += value.id + ","; }
                else { outpNew += value.id; }   
            });
            vm.periodmodel = outpNew;
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
            document.getElementById("report001").hidden = vm.tToggle;
            toggleTextFunc();
        };

        vm.showReportFunc = function () {
            setTimeout(function () {
                document.getElementById('r001').style.display = 'block';
            }, 10000);
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


        var showReptFunc = function () {
            displayReportService.reportFunc('r001')
                .then(function () {
                    //alert(vm.yearList);               
                }).catch(function (result) {
                    alert("Got error");
                });
        };


     



        yearFunc();
        
        defaultreportSet001();
        vm.showReportFunc();
        //showReptFunc();
        
        
       // vm.currentStatusFunc();     

         //$scope.para = { "pat": "John", "type": "Doe" , "cur": "ddd"};
         ////$http.post('<%= ResolveUrl("~/default.aspx/postjson") %>', $scope.user)
         //vm.viewModelHelper.apiPost('<%= ResolveUrl("~/Viewer.aspx/Page_Load") %>', $scope.para, null);
    }
}());
