
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("tasksCtrl",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout', '$modal', 
            tasksCtrl]);

    function tasksCtrl($scope, $state, viewModelHelper, validator, Excel, $timeout, $modal, $modalInstance) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;
       
        vm.view = 'dia-bx-view';
        vm.viewName = 'NN Page';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.mprBalanceSheets = [];
        vm.distinctRunDate = [];
        //vm.distinctToDate = [];

        vm.selectedsearchType = 'All';
        vm.searchValue = 'All';
        vm.number = 0;
        vm.RunDate = 'None'//new Date();
        //vm.ToDate = 'None'//new Date();

        var rootUrl = '';

        vm.tasks = [
            {
                "description": "Milk, Cheese, Pizza, Fruit, Tylenol",
                "done": false,
                "title": "Buy groceries",
                "uri": "http://localhost:5000/todo/api/v1.0/tasks/1"
            },
            {
                "description": "Need to find a good Python tutorial on the web",
                "done": false,
                "title": "Learn Python",
                "uri": "http://localhost:5000/todo/api/v1.0/tasks/2"
            }
        ];


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {

                //$http.get("data.json")   //file name
                //    .success(function (response) {
                //        $scope.tasks = response.tasks;
                //    });  

                $scope.tasks = vm.tasks;

                //open();
                vm.open = open();
            }
        }

        vm.beginEdit = function (task) {
            //alert(task.title);             
            $scope.selectedTask = task;
            $('#edit').modal('show');
        };

        vm.beginEdit2 = function () {
            //alert(task.title);             
            $('#edit').modal('show');
        };

        vm.editTask = function () {
            $('#edit').modal('show');
        };

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#dbx').length > 0) {
                    var exportTable = $('#dbx').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                        "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                        "t" +
                        "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }


        var open = function () {
            var modalInstance = $modal.open({
                //templateUrl: 'modalctrl-view.html',
                //templateUrl: '/app/assets/views/modalctrl-view.html',
                backdrop: true,
                templateUrl: rootUrl + 'app/views/modalctrl-view.html',
                controller: 'ModalCtrl as vm',
                windowClass: 'app-modal-window1'
            })
        };

        initialize();

    }
}());
