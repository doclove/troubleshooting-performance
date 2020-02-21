(function () {
    'use strict';

    angular.module('sports')
        .config(['$httpProvider', '$locationProvider', function ($httpProvider, $locationProvider) {
            $httpProvider.interceptors.push('loadingInterceptor');

            $locationProvider.html5Mode({
                enabled: true,
                requireBase: false,
                rewriteLinks: false
            });
        }]);
})();