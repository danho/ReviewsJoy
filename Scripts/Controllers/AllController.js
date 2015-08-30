var AllController = function ($scope, $window, $http) {
    $scope.GeneralReviews = '';
    $scope.CategorizedReviews = '';
    $scope.init = function(genRevs, catRevs)
    {
        $scope.GeneralReviews = JSON.parse(genRevs);
        $scope.CategorizedReviews = JSON.parse(catRevs);
    }
    $scope.isNoReviews = function()
    {
        return $scope.GeneralReviews.length + $scope.CategorizedReviews.length == 0 ? true : false;
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
    $scope.locationId = 0;
    $scope.nameTxtBx = '';
    $scope.reviewTxtArea = '';
    $scope.submitReview = function () {
        $http({
            url: '/Reviews/AddNewReview',
            method: 'POST',
            data: {
                'locationId': $scope.locationId,
                'name': $scope.nameTxtBx,
                'review': $scope.reviewTxtArea
            }
        }).success(function (data, status, headers, config) {
            var success = Boolean(data);
            if (success) {
                debugger;
                location.reload();
            }
        });
    };
}

AllController.$inject = ['$scope'];