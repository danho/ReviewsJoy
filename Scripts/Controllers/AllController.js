var AllController = function ($scope, $window) {
    $scope.hideForm = false;
    $scope.showForm = function () {
        $scope.hideForm = true;
    };
    //$scope.categoryId = 0;
    $scope.locationId = 0;
    $scope.getlocationId = function () {
        var url = $window.location.href.split('/');
        $scope.locationId = url[url.length - 1];
    };
    $scope.nameTxtBx = '';
    $scope.reviewTxtArea = '';
    $scope.submitReview = function () {
        var uri = '/Reviews/AddNewReview';
        $scope.getlocationId();
        $.ajax({
            url: uri,
            type: 'POST',
            data: {
                'locationId': $scope.locationId,
                //'categoryId': $scope.categoryId,
                'name': $scope.nameTxtBx,
                'review': $scope.reviewTxtArea
            },
            success: function (result) {
                var success = Boolean(result);
                if (success)
                {
                    location.reload();
                }
            }
        });
    };
}

AllController.$inject = ['$scope'];