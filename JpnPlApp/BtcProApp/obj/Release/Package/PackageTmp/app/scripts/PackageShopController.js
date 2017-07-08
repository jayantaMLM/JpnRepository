var module = angular.module('app', []);
module.controller('PackageShop', function ($scope, $http) {
    $scope.isProductSelected = false;
    $scope.selectedProduct = "";
    $scope.minPay = 0;
    $scope.maxPay = 0;
    $scope.priceToPay = "";
    $scope.payamount = 0;
    $scope.errmsg = "";
    $scope.errmsg1 = "";
    $scope.proceed = false;
    $scope.walletBalance = 0;
    $scope.packageId = 0;
    $scope.loader = false;
    $scope.showInvoice = false;
    $scope.address = {};
    $scope.today = new Date();
    $scope.hidePurchaseBtn = true;

    $scope.getbalance = function () {
        $http.get("MyWalletBalance?WalletId=2").then(function (data) {
            $scope.walletBalance = data.data.Balance;
            $scope.CheckBalance();
        })
        $http.get("MyPurchases").then(function (purdata) {
            $scope.purchases = purdata.data.Purchases;
            if ($scope.purchases.length == 0) { $scope.hidePurchaseBtn = false; }
        })
    }
    
    $scope.getbalance();

    $scope.payamount_validate = function () {
        debugger;
        if ($scope.payamount < $scope.minPay || $scope.payamount > $scope.maxPay) {
            $scope.errmsg = "Amount must be between " + $scope.minPay + "$ - " + $scope.maxPay + "$";
        }
        else {
            $scope.errmsg = "";
        }
    }

    $scope.CheckBalance = function () {
        if ($scope.walletBalance < $scope.payamount) {
            $scope.errmsg1 = " Insufficient wallet balance to pay.";
        } else {
            $scope.errmsg1 = "";
        }
    }

    $scope.newPurchase = function () {
        $scope.loader = true;
        $http.post("MyNewPurchase?packageId=" + $scope.packageId + "&investmentAmt=" + $scope.payamount).then(function (data) {
            debugger;
            if (data.data.Success == "TRUE") {
                $http.get("MyAddress").then(function (retdata) {
                    $scope.address = retdata.data.Address;
                    $scope.loader = false;
                })
               
            }
        })
    }
})