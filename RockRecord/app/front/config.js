var frontApp = angular.module('front', ['customService', 'ngRoute', 'ngAnimate','ngui.pager','chieffancypants.loadingBar']);

frontApp.config(function ($locationProvider, $routeProvider) {
    $locationProvider.html5Mode(true);

    $routeProvider
       .when('/Albums/:genreId', { templateUrl: '/app/front/views/albumList.html', controller: 'albumListController' })
       .otherwise({ redirectTo: '/' });
});