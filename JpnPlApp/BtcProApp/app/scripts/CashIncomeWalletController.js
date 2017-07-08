var module = angular.module('app', []);

module.controller('CashIncomeWallet', function ($scope, $http) {

    $scope.getCurrentCashWalletIncome = function () {
        $scope.totalBinaryIncome = 0;
        $scope.totalSponsorIncome = 0;
        $scope.totalGenerationIncome = 0;

        $http.get('/Home/MyCashIncomeWallet').then(function (response) {
            $scope.Binaryledger = response.data.BinaryIncomeArray;
            $scope.Sponsorledger = response.data.SponsorIncomeArray;
            $scope.Generationledger = response.data.GenerationIncomeArray;

            if ($scope.Binaryledger.length > 0) {
                angular.forEach($scope.Binaryledger, function (value, index) {
                    $scope.totalBinaryIncome = $scope.totalBinaryIncome + value.WalletAmount;
                })
            }
            if ($scope.Sponsorledger.length > 0) {
                angular.forEach($scope.Sponsorledger, function (value, index) {
                    $scope.totalSponsorIncome = $scope.totalSponsorIncome + value.WalletAmount;
                })
            }
            if ($scope.Generationledger.length > 0) {
                angular.forEach($scope.Generationledger, function (value, index) {
                    $scope.totalGenerationIncome = $scope.totalGenerationIncome + value.WalletAmount;
                })
            }
        })
    }

    $scope.getCurrentCashWalletIncome();
})