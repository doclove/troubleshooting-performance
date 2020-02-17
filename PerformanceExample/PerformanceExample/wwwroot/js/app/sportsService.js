(function () {
    'use strict';

    var serviceId = 'sportsService';

    angular.module('sports').factory(serviceId,
        [
            '$http',
            function ($http) {

                var service = {
                    getLatest: getLatest,
                    placeKCBet: placeKCBet,
                    placeSanFranBet: placeSanFranBet
                };

                return service;

                function getLatest() {
                    return $http
                        .get('/api/sports/getLatest')
                        .then(function (response) {
                            return response.data;
                        });
                }

                function placeKCBet(amount) {
                    return $http
                        .put('/api/sports/placeKCBet', amount)
                        .then(function (response) {
                            return response.data;
                        });
                }

                function placeSanFranBet(amount) {
                    return $http
                        .put('/api/sports/placeSanFranBet', amount)
                        .then(function (response) {
                            return response.data;
                        });
                }
            }
        ]
    );
})();