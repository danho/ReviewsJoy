var ReviewsByCategoryNameController = function ($scope, $window) {
    $scope.model = '';
    $scope.init = function (value)
    {
        $scope.model = value;
    }
    $scope.alert = function()
    {
        debugger;
        var a = JSON.parse($scope.model);
        alert($scope.model);
    }
}

ReviewsByCategoryNameController.$inject = ['$scope'];