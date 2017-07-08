var module = angular.module('app',[]);

module.controller('ProcessPayout', function ($scope, $http, $location) {
    $scope.processing = false;
    $scope.processedtext = "";

    $scope.pre = function () {
        $http.get("/Home/PreCalculatePayout").then(function (response) {
            $scope.preInfo = response.data.PayoutInfo;
        })
    }
    $scope.pre();

    $scope.Process = function () {
        var ans = confirm("Sure you want to proceed ?");
        if (ans) {
            $scope.processing = true;
            $http.post("/Home/CalculatePayout").then(function (response) {
                if (response.data.Success) {
                    $scope.pre();
                    $scope.processing = false;
                    alert("Process completed successsfully!!!");
                }
            })
        }
    }
})
