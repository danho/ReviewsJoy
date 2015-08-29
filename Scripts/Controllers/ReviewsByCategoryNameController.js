var ReviewsByCategoryNameController = function ($scope, $window) {
    $scope.model = '';
    $scope.init = function (model)
    {
        $scope.model = JSON.parse(model);
    }
}

ReviewsByCategoryNameController.$inject = ['$scope'];