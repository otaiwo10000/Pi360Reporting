
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CarouselDemoCtrl",
        ['$scope', '$state', 'viewModelHelper', 'validator', 'Excel', '$timeout',
            CarouselDemoCtrl]);

    function CarouselDemoCtrl($scope, $state, viewModelHelper, validator, Excel, $timeout) {
       
        $scope.myInterval = 500;
        var slides = $scope.slides = [];
        $scope.addSlide = function() {
            var newWidth = 600;
            slides.push({
                image: 'http://placekitten.com/' + newWidth + '/300',
                
            });
        };
        for (var i = 0; i < 4; i++) {
            $scope.addSlide();
        }

    }
}());
