
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("Carousel2Controller",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout',
            Carousel2Controller]);

    function Carousel2Controller($scope, $state, viewModelHelper, validator, Excel, $timeout) {
       
        $scope.myInterval = 500;
        
        $scope.images = [
   "http://placehold.it/500/e499e4/fff&amp;text=1",
   "http://placehold.it/500/e499e4/fff&amp;text=2",
   "http://placehold.it/500/e499e4/fff&amp;text=3",
   "http://placehold.it/500/e499e4/fff&amp;text=4",
   "http://placehold.it/500/e499e4/fff&amp;text=5",
   "http://placehold.it/500/e499e4/fff&amp;text=6",
   "http://placehold.it/500/e499e4/fff&amp;text=7",
   "http://placehold.it/500/e499e4/fff&amp;text=8",
   "http://placehold.it/500/e499e4/fff&amp;text=9",
        ];

    }
}());
