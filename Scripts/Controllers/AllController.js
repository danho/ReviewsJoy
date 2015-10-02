var AllController = function ($scope, $window, $http) {
    $scope.$on('$routeChangeSuccess', function () {
        $("body").css("background-image", "none");
    });
    $scope.Reviews = '';
    $scope.locationId = 0;
    $scope.init = function(reviews, locationId, placeId)
    {
        if (reviews != null && reviews != '')
            $scope.Reviews = JSON.parse(reviews);
        if (locationId != null && locationId != '')
            $scope.locationId = locationId;
        $scope.placeId = placeId;
    }
    $scope.isNoReviews = function()
    {
        return $scope.Reviews.length == 0 ? true : false;
    }
    $scope.hideGenForm = true;
    $scope.hideCatForm = true;
    $scope.hideReviews = false;
    $scope.toggleReviews = function () {
        if ($scope.hideCatForm && $scope.hideGenForm)
            $scope.hideReviews = false;
        else
            $scope.hideReviews = true;
    };
    $scope.toggleGenForm = function () {
        $scope.hideGenForm = !$scope.hideGenForm;
        $scope.hideCatForm = true;
        $scope.toggleReviews();
    };
    $scope.toggleCatForm = function () {
        $scope.hideCatForm = !$scope.hideCatForm;
        $scope.hideGenForm = true;
        $scope.toggleReviews();
    };
    $scope.categoryTxtBx = '';
    $scope.searchByCategoy = function () {
        $window.location.href = '/#/reviews/' + $scope.locationId + '/' + $scope.categoryTxtBx + '';
    };
    $scope.nameTxtBx = '';
    $scope.reviewTxtArea = '';
    $scope.submitGeneralReview = function () {
        $http({
            url: '/Reviews/AddNewReview',
            method: 'POST',
            data: {
                'locationId': $scope.locationId,
                'placeId': $scope.placeId,
                'name': $scope.nameTxtBx,
                'review': $scope.reviewTxtArea,
                'category': 'GENERAL'
            }
        }).success(function (data, status, headers, config) {
            var success = Boolean(data);
            if (success) {
                location.reload();
            }
        });
    };
    $scope.submitReview = function () {
        $http({
            url: '/Reviews/AddNewReview',
            method: 'POST',
            data: {
                'locationId': $scope.locationId,
                'placeId': $scope.placeId,
                'category': $scope.nameTxtBx,
                'review': $scope.reviewTxtArea
            }
        }).success(function (data, status, headers, config) {
            var success = Boolean(data);
            if (success) {
                location.reload();
            }
        });
    };
}

AllController.$inject = ['$scope'];