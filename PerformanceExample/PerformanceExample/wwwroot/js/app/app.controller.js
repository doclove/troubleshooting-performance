(function () {
    'use strict';

    var controllerId = 'AppCtrl';

    angular.module('sports').controller(controllerId,
        [
            '$scope', 'sportsService',
            function ($scope,sportsService) {
                var vm = this;

                vm.amountBet = '';
                vm.betKC = betKC;
                vm.betSF = betSF;
                vm.amountKansas = 0;
                vm.amountSanFran = 0;
                vm.kansasOdds = '';
                vm.sanFranOdds = '';
                vm.bets = [];

                activate();

                function activate() {
                    sportsService.getLatest().then(function (data) {
                        vm.amountBet = data.amountBet;
                        vm.kansasOdds = data.kansasOdds;
                        vm.sanFranOdds = data.sanFranOdds;
                    });

                    getBets();
                }

                function betKC() {
                    sportsService.placeKCBet(vm.amountKansas).then(function(data) {
                        vm.amountBet = data.amountBet;
                        vm.kansasOdds = data.kansasOdds;
                        vm.sanFranOdds = data.sanFranOdds;
                        getBets();
                    });
                }

                function betSF() {
                    sportsService.placeSanFranBet(vm.amountSanFran).then(function (data) {
                        vm.amountBet = data.amountBet;
                        vm.kansasOdds = data.kansasOdds;
                        vm.sanFranOdds = data.sanFranOdds;
                        getBets();
                    });
                }

                function getBets() {
                    sportsService.getBets().then(function (data) {
                        vm.bets = data.slice(0, 10);
                    });
                }
            }

        ]
    );
})();