var app = angular.module('ReviewsJoy', ['ngRoute']);

app.controller('HomePageController', ['$scope', '$window', HomePageController]);
app.controller('BeginController', ['$scope', '$window', BeginController]);

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
            }
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