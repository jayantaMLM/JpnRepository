var module = angular.module('app', []);

module.controller('MemberTransfer', function ($scope, $http, $location) {
    $scope.amount = 0;
    $scope.username = "";
    $scope.wallet = '5';
    $scope.walletBalance = 0;
    $scope.errormessage = "";

    $http.get("/Home/IsUserMember").then(function (response) {
        $scope.isExists = response.data.Found;
    })

    $scope.getbalance = function () {
        $http.get("/Home/MyWalletBalance?WalletId=5").then(function (data) {
            debugger;
            $scope.walletBalance = data.data.Balance;
        })
    }
    $scope.Transaction = function () {
        var ans=confirm("Are you sure?");
        if (ans) {
            $http.post("/Home/LedgerPostingMember?Username=" + $scope.username + "&WalletType=" + $scope.wallet + "&Amount=" + $scope.amount).then(function (response) {
                debugger;
                if (response.data.Success) {
                    $scope.username = "";
                    $scope.amount = 0;
                    $scope.getbalance();
                    $scope.GetHistory();
                }
            })
        }
        
    }

    $scope.getbalance();

    $scope.GetHistory = function () {
        $http.get("/Home/MemberTransferHistory").then(function (response) {
            $scope.transfers = response.data.Transfers;
            if ($scope.transfers.length > 0) {
                var calculatedAmt = 0;
                debugger;
                angular.forEach($scope.transfers, function (value, index) {
                    calculatedAmt = calculatedAmt + value.Deposit - value.Withdraw;
                    value.Amount = calculatedAmt;
                })
            }
        })
    }

    $scope.goToPurchase = function () {
        var path = $location.path("/Home/PackagesShop");
        var abspath = path.$$absUrl;
        var modifiedpath = abspath.replace("/Home/Transfers#!", "");
        window.location = modifiedpath;
    }
    
    $scope.GetHistory();

    $scope.checkUsername = function () {
        $http.get("/Home/IsUserNameExist1?UserName=" + $scope.username).then(function (data) {
            $scope.isUsernameFound1 = data.data.Found;
            if (!$scope.isUsernameFound1) {
                $scope.username = "";
                $scope.errormessage = "Username not found.";

            } else {
                $scope.errormessage = "";
            }
        })

    }
})