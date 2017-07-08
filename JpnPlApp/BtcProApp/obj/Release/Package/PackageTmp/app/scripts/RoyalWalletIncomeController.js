var module = angular.module('app', []);

module.controller('RoyalIncomeWallet', function ($scope, $http) {
    $scope.getCurrentRoyalWalletIncome = function () {
        $scope.totalRoyalWalletBalance = 0;

        $http.get('/Home/MyRoyalIncomeWallet').then(function (response) {
            $scope.RoyalWalletledger = response.data.RoyalWallet;

            if ($scope.RoyalWalletledger.length > 0) {
                angular.forEach($scope.RoyalWalletledger, function (value, index) {
                    value.Balance = $scope.totalRoyalWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalRoyalWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.RoyalWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentRoyalWalletIncome();
})