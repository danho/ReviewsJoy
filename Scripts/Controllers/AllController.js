var AllController = function ($scope, $window) {
    $scope.hideForm = false;
    $scope.showForm = function () {
        $scope.hideForm = true;
    }
}

AllController.$inject = ['$scope'];