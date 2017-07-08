var module = angular.module('app', []);

module.controller('RefIncomeWallet', function ($scope, $http) {
    $scope.getCurrentRefWalletIncome = function () {
        $scope.totalRefWalletBalance = 0;

        $http.get('/Home/MyRefIncomeWallet').then(function (response) {
            $scope.RefWalletledger = response.data.RefWallet;

            if ($scope.RefWalletledger.length > 0) {
                angular.forEach($scope.RefWalletledger, function (value, index) {
                    value.Balance = $scope.totalRefWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalRefWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.RefWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentRefWalletIncome();
})