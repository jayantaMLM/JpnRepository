var module = angular.module('app', []);

module.controller('WithdrawalAccountWallet', function ($scope, $http) {
    $scope.getCurrentWithdrawalWallet = function () {
        $scope.totalWithdrawalWalletBalance = 0;

        $http.get('/Home/MyWithdrawalAccountWallet').then(function (response) {
            $scope.WithdrawalWalletledger = response.data.WithdrawalWallet;

            if ($scope.WithdrawalWalletledger.length > 0) {
                angular.forEach($scope.WithdrawalWalletledger, function (value, index) {
                    value.Balance = $scope.totalWithdrawalWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalWithdrawalWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.WithdrawalWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentWithdrawalWallet();
})