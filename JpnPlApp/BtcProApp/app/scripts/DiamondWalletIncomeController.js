var module = angular.module('app', []);

module.controller('DiamondIncomeWallet', function ($scope, $http) {
    $scope.getCurrentDiamondWalletIncome = function () {
        $scope.totalDiamondWalletBalance = 0;

        $http.get('/Home/MyDiamondIncomeWallet').then(function (response) {
            $scope.DiamondWalletledger = response.data.DiamondWallet;

            if ($scope.DiamondWalletledger.length > 0) {
                angular.forEach($scope.DiamondWalletledger, function (value, index) {
                    value.Balance = $scope.totalDiamondWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalDiamondWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.DiamondWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentDiamondWalletIncome();
})