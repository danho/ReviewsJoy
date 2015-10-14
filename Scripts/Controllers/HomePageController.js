var HomePageController = function ($scope, $window, $http) {
    //$scope.$on('$routeChangeSuccess', function () {
    //    $("body").css("background-image", "url('/Content/photo-1428908728789-d2de25dbd4e2.jpg')");
    //    $http({
    //        url: '/Home/WarmUp',
    //        method: 'POST',
    //        data: {}
    //    });
    //});
    $scope.init = function () {
        $("body").css("background-image", "url('/Content/photo-1428908728789-d2de25dbd4e2.jpg')");
        $http({
            url: '/Home/WarmUp',
            method: 'POST',
            data: {}
        });
        $('#intro').css({ 'height': ($(window).height()) + 'px' });
        $(window).resize(function () {
            $('#intro').css({ 'height': ($(window).height()) + 'px' });
        });
    };
    $scope.showSearch = false;
    $scope.toggle = function () {
        $scope.showSearch = true;
    };
    $scope.searchText = '';
    $scope.searchBtnDisabled = true;
    $scope.autoComplete = function () {
        $scope.searchBtnDisabled = true;
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
                            $scope.searchBtnDisabled = false;
                            $scope.placeId = ui.item.placeId;
                        });
                    }
                });
            });
        }
    };
    $scope.search = function () {
        $window.location.href = '/#/reviews/' + $scope.placeId;
    }
}

HomePageController.$inject = ['$scope'];