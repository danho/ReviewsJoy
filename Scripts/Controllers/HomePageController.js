var HomePageController = function ($scope, $window) {
    $scope.$on('$routeChangeSuccess', function () {
        $("body").css("background-image", "url('/Content/photo-1428908728789-d2de25dbd4e2.jpg')");
    });
    $scope.toggle = function () {
        $window.location.href = '/#/Begin/';
    };
}

HomePageController.$inject = ['$scope'];