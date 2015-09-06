var SearchController = function ($scope, $window) {
    $scope.init = function (model) {
        if (model == '') {
            $scope.noReviewsAvail = true;
        } else {
            $scope.model = JSON.parse(model);
        }
    }
    $scope.searchText = '';
    $scope.search = function () {
        $window.location.href = '/#/search/' + $scope.searchText;
    }
}

SearchController.$inject = ['$scope'];