var AllController = function ($scope, $window, $http) {
    $scope.$on('$routeChangeSuccess', function () {
        $("body").css("background-image", "none");
    });
    $scope.Reviews = '';
    $scope.locationId = 0;
    $scope.init = function (reviews, locationId, placeId, locationName)
    {
        if (reviews != null && reviews != '')
            $scope.Reviews = JSON.parse(reviews);
        if (locationId != null && locationId != '')
            $scope.locationId = locationId;
        $scope.placeId = placeId;
        $scope.locationName = locationName;
        angular.element(document).ready(function () {
            $("#star1").click(function () {
                $scope.stars = 1;
                $(this).removeClass("unclicked-star");
                $(this).addClass("clicked-star");
                $("#star2").removeClass("clicked-star");
                $("#star2").addClass("unclicked-star");
                $("#star3").removeClass("clicked-star");
                $("#star3").addClass("unclicked-star");
                $("#star4").removeClass("clicked-star");
                $("#star4").addClass("unclicked-star");
                $("#star5").removeClass("clicked-star");
                $("#star5").addClass("unclicked-star");
            });
            $("#star2").click(function () {
                $scope.stars = 2;
                $(this).removeClass("unclicked-star");
                $(this).addClass("clicked-star");
                $("#star1").removeClass("unclicked-star");
                $("#star1").addClass("clicked-star");
                $("#star3").removeClass("clicked-star");
                $("#star3").addClass("unclicked-star");
                $("#star4").removeClass("clicked-star");
                $("#star4").addClass("unclicked-star");
                $("#star5").removeClass("clicked-star");
                $("#star5").addClass("unclicked-star");
            });
            $("#star3").click(function () {
                $scope.stars = 3;
                $(this).removeClass("unclicked-star");
                $(this).addClass("clicked-star");
                $("#star1").removeClass("unclicked-star");
                $("#star1").addClass("clicked-star");
                $("#star2").removeClass("unclicked-star");
                $("#star2").addClass("clicked-star");
                $("#star4").removeClass("clicked-star");
                $("#star4").addClass("unclicked-star");
                $("#star5").removeClass("clicked-star");
                $("#star5").addClass("unclicked-star");
            });
            $("#star4").click(function () {
                $scope.stars = 4;
                $(this).removeClass("unclicked-star");
                $(this).addClass("clicked-star");
                $("#star1").removeClass("unclicked-star");
                $("#star1").addClass("clicked-star");
                $("#star2").removeClass("unclicked-star");
                $("#star2").addClass("clicked-star");
                $("#star3").removeClass("unclicked-star");
                $("#star3").addClass("clicked-star");
                $("#star5").removeClass("clicked-star");
                $("#star5").addClass("unclicked-star");
            });
            $("#star5").click(function () {
                $scope.stars = 5;
                $(this).removeClass("unclicked-star");
                $(this).addClass("clicked-star");
                $("#star1").removeClass("unclicked-star");
                $("#star1").addClass("clicked-star");
                $("#star2").removeClass("unclicked-star");
                $("#star2").addClass("clicked-star");
                $("#star3").removeClass("unclicked-star");
                $("#star3").addClass("clicked-star");
                $("#star4").removeClass("unclicked-star");
                $("#star4").addClass("clicked-star");
            });
        });
    }
    $scope.isNoReviews = function()
    {
        return $scope.Reviews.length == 0 ? true : false;
    }
    $scope.showCategoryTxtBx = false;
    $scope.hideGenForm = true;
    $scope.hideCatForm = true;
    $scope.hideReviews = false;
    $scope.toggleReviews = function () {
        if ($scope.hideCatForm && $scope.hideGenForm)
            $scope.hideReviews = false;
        else
            $scope.hideReviews = true;
    };
    $scope.toggleCategoryTxtBx = function () {
        $scope.showCategoryTxtBx = !$scope.showCategoryTxtBx;
    }
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
    $scope.category = '';
    $scope.stars = 0;
    $scope.submitReview = function () {
        $http({
            url: '/Reviews/AddNewReview',
            method: 'POST',
            data: {
                'locationId': $scope.locationId,
                'locationName': $scope.locationName,
                'placeId': $scope.placeId,
                'name': $scope.nameTxtBx,
                'review': $scope.reviewTxtArea,
                'category': $scope.category,
                'stars': $scope.stars
            }
        }).success(function (data, status, headers, config) {
            var success = Boolean(data);
            if (success) {
                location.reload();
            }
        });
    };
    //$scope.submitReview = function () {
    //    $http({
    //        url: '/Reviews/AddNewReview',
    //        method: 'POST',
    //        data: {
    //            'locationId': $scope.locationId,
    //            'locationName': $scope.locationName,
    //            'placeId': $scope.placeId,
    //            'category': $scope.nameTxtBx,
    //            'review': $scope.reviewTxtArea
    //        }
    //    }).success(function (data, status, headers, config) {
    //        var success = Boolean(data);
    //        if (success) {
    //            location.reload();
    //        }
    //    });
    //};
    $scope.clearModal = function () {
        $scope.nameTxtBx = null;
        $scope.category = null;
        $scope.reviewTxtArea = null;
        $("#star1").removeClass("clicked-star");
        $("#star1").addClass("unclicked-star");
        $("#star2").removeClass("clicked-star");
        $("#star2").addClass("unclicked-star");
        $("#star3").removeClass("clicked-star");
        $("#star3").addClass("unclicked-star");
        $("#star4").removeClass("clicked-star");
        $("#star4").addClass("unclicked-star");
        $("#star5").removeClass("clicked-star");
        $("#star5").addClass("unclicked-star");
        $scope.myForm.$setPristine();
    }
    $scope.filterByCategory = function() {
        $http({
            url: '/Reviews/FilterByCategory',
            method: 'POST',
            data: {
                'locationId': $scope.locationId,
                'category': $scope.categoryTxtBx
            }
        }).success(function (data, status, headers, config) {
            //if (data.count > 0) {
            $scope.Reviews = data;
            //}
        });
    }
}

AllController.$inject = ['$scope'];
