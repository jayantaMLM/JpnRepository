var module = angular.module('app', []);

module.controller('BusinessReport', function ($scope, $http) {
    $scope.calculatedAmt = 0;
    $scope.sponsorTotalIncome = 0;
    $scope.generationTotalIncome = 0;

    $scope.getCurrentBinaryIncome = function () {
        $http.get('/Home/GetMyCurrentBinaryIncome').then(function (response) {
            $scope.binaryledger = response.data.BIincomeArray;
            $scope.ledgertotals = response.data.TotalsArray;
        })
    }
    $scope.getCurrentBinaryIncome();

    $scope.getCurrentSponsorIncome = function () {
        $http.get('/Home/GetMyCurrentSponsorIncome').then(function (response) {
            debugger;
            $scope.sponsorledger = response.data.SpIincomeArray;
            $scope.sponsorTotalIncome = 0;
            angular.forEach($scope.sponsorledger,function(value,index){
                $scope.sponsorTotalIncome = $scope.sponsorTotalIncome + value.WalletAmount;
            })
        })
    }
    $scope.getCurrentSponsorIncome();

    
})