var module = angular.module('app', []);

module.controller('AdminTransfer', function ($scope, $http) {
    $scope.amount = 0;
    $scope.username = "";
    $scope.dr_or_cr = "D";
    $scope.wallet = '5';
    $scope.errormessage = "";
    $scope.name = "";
    $scope.email = "";
    $scope.countrycode = "";
    $scope.idfound = false;

    $http.get("/Home/AdminTransferLedger?Username=").then(function (response) {
        $scope.transfers = response.data.Ledger;
    })

    $scope.Transaction = function () {
        var ans = confirm("Are you sure?");
        if (ans) {
            $http.post("LedgerPosting?Username=" + $scope.username + "&DrCr=" + $scope.dr_or_cr + "&WalletType=" + $scope.wallet + "&Amount=" + $scope.amount).then(function (response) {
                $scope.username = "";
                $scope.amount = 0;
                $scope.idfound = false;
                $scope.name = "";
                $scope.email = "";
                $scope.countrycode = "";
                $http.get("/Home/AdminTransferLedger?Username=" + $scope.username).then(function (response) {
                    $scope.transfers = response.data.Ledger;
                })
                alert("Amount transferred successfully");
            })
        }
    }

    $scope.isNotExcessWithdraw = function () {
        if ($scope.dr_or_cr == "D") { return false; }
        if ($scope.dr_or_cr == "W") {
            if ($scope.wallet == "1") {
                if ($scope.DevelopmentBalance >= $scope.amount) { return false; } else { return true; }
            }
            if ($scope.wallet == "2") {
                if ($scope.ReferralBalance >= $scope.amount) { return false; } else { return true; }
            }
            if ($scope.wallet == "3") {
                if ($scope.RoyaltyBalance >= $scope.amount) { return false; } else { return true; }
            }
            if ($scope.wallet == "4") {
                if ($scope.DiamondRoyaltyBalance >= $scope.amount) { return false; } else { return true; }
            }
            if ($scope.wallet == "5") {
                if ($scope.JoiningBalance >= $scope.amount) { return false; } else { return true; }
            }
            if ($scope.wallet == "6") {
                if ($scope.WithdrawBalance >= $scope.amount) { return false; } else { return true; }
            }
        }
    }

    $scope.checkUsername = function () {
        $http.get("/Home/IsUserNameExist1?UserName=" + $scope.username).then(function (data) {
            debugger;
            $scope.isUsernameFound1 = data.data.Found;
            if (!$scope.isUsernameFound1) {
                $scope.username = "";
                $scope.errormessage = "Username not found.";
                $scope.idfound = false;
                $scope.name = "";
                $scope.email = "";
                $scope.countrycode = "";
                $scope.transfers = [];

            } else {
                $scope.errormessage = "";
                $scope.idfound = true;
                $scope.name = data.data.Name;
                $scope.email = data.data.Email;
                $scope.countrycode = data.data.Countrycode;
                $http.get("/Home/AdminTransferLedger?Username=" + $scope.username).then(function (response) {
                    $scope.transfers = response.data.Ledger;
                })
                $http.get("/Home/UserWalletBalance?username=" + $scope.username).then(function (data) {
                    debugger;
                    $scope.DevelopmentBalance = data.data.Wallet1_Balance;
                    $scope.ReferralBalance = data.data.Wallet2_Balance;
                    $scope.RoyaltyBalance = data.data.Wallet3_Balance;
                    $scope.DiamondRoyaltyBalance = data.data.Wallet4_Balance;
                    $scope.JoiningBalance = data.data.Wallet5_Balance;
                    $scope.WithdrawBalance = data.data.Wallet6_Balance;
                })
            }
        })
    }
})