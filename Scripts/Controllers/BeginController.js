var BeginController = function ($scope, $window, $http) {
    $scope.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 };
    $scope.marker = {
        id: 0,
        coords: { }
    };
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
                    ac.push({ label: jsonResult.predictions[i].description, placeId: jsonResult.predictions[i].place_id });
                }
                $('#searchTxtBx').autocomplete({
                    source: ac,
                    select: function (event, ui) {
                        $http({
                            url: '/Reviews/GetLatAndLng',
                            method: 'POST',
                            data: {
                                'placeId': ui.item.placeId
                            }
                        }).success(function (data, status, headers, config) {
                            var jsonResult2 = JSON.parse(data);
                            var lat = jsonResult2.result.geometry.location.lat;
                            var lng = jsonResult2.result.geometry.location.lng;
                            $scope.map = { center: { latitude: lat, longitude: lng }, zoom: 17 };
                            $scope.marker = { id: 0, coords: { latitude: lat, longitude: lng } };
                        });
                    }
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