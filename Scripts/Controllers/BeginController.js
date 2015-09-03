var BeginController = function ($scope, $window, $http) {
    $scope.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 };
    $scope.searchText = '';
    $scope.autoComplete = function () {
        if ($scope.searchText.length > 3) {
            $http({
                url: '/Reviews/AutoCompleteSearch',
                method: 'POST',
                data: {
                    'searchText': $scope.searchText
                }
            }).success(function (data, status, headers, config) {
                var jsonResult = JSON.parse(data);
                var ac = [];
                for (var i = 0; i < jsonResult.predictions.length; i++) {
                    ac.push(jsonResult.predictions[i].description);
                }
                $('#searchTxtBx').autocomplete({
                    source: ac
                });
            });
        }
    };
    $scope.search = function () {
        //$http({
        //    url: '/Reviews/AutoCompleteSearch',
        //    method: 'POST',
        //    data: {
        //        'searchText': $scope.searchText
        //    }
        //}).success(function (data, status, headers, config) {
        //    var jsonResult = JSON.parse(data);
        //});
        $window.location.href = '/#/search/' + $scope.searchText;
    }
}

BeginController.$inject = ['$scope'];