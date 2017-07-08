var module = angular.module('app', []);

module.controller('JoiningWallet', function ($scope, $http) {
    $scope.getCurrentJoiningWallet = function () {
        $scope.totalJoiningWalletBalance = 0;

        $http.get('/Home/MyJoiningAccountWallet').then(function (response) {
            debugger;
            $scope.JoiningWalletledger = response.data.JoiningWallet;

            if ($scope.JoiningWalletledger.length > 0) {
                angular.forEach($scope.JoiningWalletledger, function (value, index) {
                    value.Balance = $scope.totalJoiningWalletBalance + value.Deposit - value.Withdraw;
                    $scope.totalJoiningWalletBalance = value.Balance;
                })
            }
            $scope.recordCount = $scope.JoiningWalletledger.length;
            debugger;
        })
    }

    $scope.getCurrentJoiningWallet();
})