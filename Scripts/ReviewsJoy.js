﻿var app = angular.module('ReviewsJoy', ['ngRoute', 'vcRecaptcha']);
app.filter('range', function () {
    return function (input, total) {
        total = parseInt(total);
        for (var i = 0; i < total; i++) {
            input.push(i);
        }
        return input;
    };
});

app.controller('HomePageController', ['$scope', '$window', '$http', HomePageController]);
app.controller('BeginController', ['$scope', '$window', '$http', BeginController]);
app.controller('SearchController', ['$scope', '$window', SearchController]);
app.controller('AllController', ['$scope', '$window', '$http', 'vcRecaptchaService', AllController]);
app.controller('ReviewsByCategoryNameController', ['$scope', '$window', ReviewsByCategoryNameController]);

var configFunction = function ($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: 'Home/LandingPage',
            controller: 'HomePageController'
        })
        .when('/Landing', {
            templateUrl: 'Home/LandingPage',
            controller: 'HomePageController'
        })
        .when('/Begin', {
            templateUrl: 'Home/Begin',
            controller: 'BeginController'
        })
        .when('/search/:address', {
            templateUrl: function (params) {
                return '/Location/GetLocationByPlaceId?placeId=' + params.address;
            },
            controller: 'SearchController'
        })
        .when('/reviews/:placeId', {
            templateUrl: function (params) {
                return '/Reviews/All?placeId=' + params.placeId;
            },
            controller: 'AllController'
        })
        .when('/reviews/:id/:category', {
            templateUrl: function (params) {
                return '/Reviews/ReviewsByCategoryName?id=' + params.id + '&categoryName=' + params.category;
            },
            controller: 'ReviewsByCategoryNameController'
        })
        .otherwise({
            redirectTo: '/Landing'
        });
};

configFunction.$inject = ['$routeProvider'];
app.config(configFunction);
