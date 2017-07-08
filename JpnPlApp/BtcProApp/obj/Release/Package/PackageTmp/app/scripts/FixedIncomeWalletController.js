var module = angular.module('app', []);

module.controller('FixedIncomeWallet', function ($scope, $http) {
    $scope.getCurrentFixedIncome = function () {
        $http.get('MyFixedIncomeWallet').then(function (response) {
            $scope.ledger = response.data.FixedIncomeArray;
            if ($scope.ledger.length > 0) {
                $scope.calculatedAmt = 0;
                $scope.paidamount = 0;
              
                angular.forEach($scope.ledger, function (value, index) {
                    $scope.calculatedAmt = $scope.calculatedAmt + value.Amount;
                    value.Total = $scope.calculatedAmt;
                })
            }
        })
    }
    $scope.getCurrentFixedIncome();
})