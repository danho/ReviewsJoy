var SearchController = function ($scope, $window) {
    $scope.searchText = '';
    $scope.search = function () {
        $window.location.href = '/#/search/' + $scope.searchText;
    }
}

SearchController.$inject = ['$scope'];