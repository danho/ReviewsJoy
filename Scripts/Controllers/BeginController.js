var BeginController = function ($scope, $window) {
    $scope.searchText = '';
    $scope.search = function () {
        $window.location.href = '/#/search/' + $scope.searchText;
    }
}

BeginController.$inject = ['$scope'];