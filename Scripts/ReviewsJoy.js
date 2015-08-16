var app = angular.module('ReviewsJoy', ['ngRoute']);

app.controller('HomePageController', ['$scope', '$window', HomePageController]);
app.controller('BeginController', ['$scope', '$window', BeginController]);
app.controller('SearchController', ['$scope', '$window', SearchController]);
app.controller('AllController', ['$scope', '$window', AllController]);

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
        .otherwise({
            redirectTo: '/Landing'
        });
};

configFunction.$inject = ['$routeProvider'];
app.config(configFunction);

//app.config(['$routeProvider',
//    function($routeProvider) {
//        $routeProvider.
//            when('/all', {
//                templateUrl: 'reviews/all/1'
//            });
//    }]);;