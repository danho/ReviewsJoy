var HomePageController = function ($scope, $window, $http) {
    $scope.init = function () {
        $("body").css("background-image", "url('/Content/sky.jpg')");
        angular.element(document).ready(function () {
            $(".reviewContainer").click(function () {
                $window.location.href = '/#/reviews/' + $(this).attr('value');
            });
            $("#searchTxtBx").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '/Reviews/AutoCompleteSearch',
                        method: 'POST',
                        dataType: 'json',
                        data: {
                            'searchText': request.term
                        },
                        success: function (data) {
                            var jsonResult = JSON.parse(data);
                            var ac = [];
                            for (var i = 0; i < jsonResult.predictions.length; i++) {
                                ac.push({ label: jsonResult.predictions[i].description, placeId: jsonResult.predictions[i].place_id });
                            }
                            response(ac);
                        }
                    });
                },
                minLength: 3,
                select: function (event, ui) {
                    $scope.searchBtnDisabled = false;
                    $scope.placeId = ui.item.placeId;
                    $scope.$apply();
                },
            });
        });
        $http({
            url: '/Home/GetLatestReviews',
            method: 'POST',
            data: {}
        }).then(function successCallback(response) {
            $scope.showSpinner = false;
            $.each(response.data, function (key, value) {
                $("#latestReviews").append("<div class='reviewContainer clickable col-xs-12 col-md-6' value='" + value.Location.placeId + "'><div class='review'>" + value.ReviewText + "<br/><div class='text-right'>" + value.Location.Name + "</div></div></div>")
                $(".reviewContainer").click(function () {
                    window.location.href = '/#/reviews/' + $(this).attr('value');
                });
            });
          }, function errorCallback(response) {
          });
        $('#intro').css({ 'height': ($(window).height()) + 'px' });
        $(window).resize(function () {
            $('#intro').css({ 'height': ($(window).height()) + 'px' });
        });
    };
    $scope.showSpinner = true;
    $scope.showSearch = false;
    $scope.toggle = function () {
        $scope.showSearch = true;
    };
    $scope.searchText = '';
    $scope.searchBtnDisabled = true;
    $scope.search = function () {
        $window.location.href = '/#/reviews/' + $scope.placeId;
    }
}

HomePageController.$inject = ['$scope'];