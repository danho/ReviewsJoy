
/// <reference path="../Scripts/jasmine.js" />
/// <reference path="../../scripts/angular.js" />
/// <reference path="../../scripts/controllers/homepagecontroller.js" />
/// <reference path="../../scripts/angular-mocks.js" />
/// <reference path="../../scripts/angular-route.js" />
/// <reference path="../../scripts/reviewsjoy.js" />
/// <reference path="../../scripts/loaddash.js" />
/// <reference path="../../scripts/angular-google-maps.js" />

//describe("tests", function () {
//    it("isDebug", function () {
//        expect(app.isDebug).toEqual(true);
//    });
//});

describe('HomePageController Tests', function () {
    beforeEach(module('ReviewsJoy'));

    var $controller;

    beforeEach(inject(function(_$controller_) {
        $controller = _$controller_;
    }));

    describe('$scope.showSearch', function () {
        it('is false on first load', function () {
            var $scope = {};
            var controller = $controller('HomePageController', { $scope: $scope });
            expect($scope.showSearch).toEqual(false);
        });

        it('is true when toggled', function () {
            var $scope = {};
            var controller = $controller('HomePageController', { $scope: $scope });
            $scope.toggle();
            expect($scope.showSearch).toEqual(true);
        });
    });

    describe('$scope.searchText', function () {
        it('is empty on first load', function () {
            var $scope = {};
            var controller = $controller('HomePageController', { $scope: $scope });
            expect($scope.searchText).toEqual('');
        });
    });
});