var app = angular.module('ReviewsJoy', ['ngRoute', 'uiGmapgoogle-maps']);

app.controller('HomePageController', ['$scope', '$window', HomePageController]);
app.controller('BeginController', ['$scope', '$window', '$http', BeginController]);
app.controller('SearchController', ['$scope', '$window', SearchController]);
app.controller('AllController', ['$scope', '$window', '$http', AllController]);
app.controller('ReviewsByCategoryNameController', ['$scope', '$window', ReviewsByCategoryNameController])

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
                return '/Location/GetLocationsByAddressWebService?address=' + params.address;
            },
            controller: 'SearchController'
        })
        .when('/reviews/:id', {
            templateUrl: function (params) {
                return '/Reviews/All?id=' + params.id;
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