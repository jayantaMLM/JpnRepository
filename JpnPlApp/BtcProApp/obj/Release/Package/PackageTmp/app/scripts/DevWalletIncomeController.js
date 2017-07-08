var module = angular.module('app', []);

module.controller('DevIncomeWallet', function ($scope, $http) {
    $scope.getCurrentDevWalletIncome = function () {
        $scope.totalDevWalletBalance = 0;

        $http.get('/Home/MyDevIncomeWallet').then(function (response) {
            $scope.DevWalletledger = response.data.DevWallet;

            if ($scope.DevWalletledger.length > 0) {
                angular.forEach($scope.DevWalletledger, function (value, index) {
                    value.Balance = $scope.totalDevWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalDevWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.DevWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentDevWalletIncome();
})