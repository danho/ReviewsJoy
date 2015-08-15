var HomePageController = function ($scope, $window) {
    $scope.toggle = function () {
        $window.location.href = '/#/Begin/';
    };
}

HomePageController.$inject = ['$scope'];