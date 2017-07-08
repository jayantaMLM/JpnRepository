var module = angular.module('app', []);
module.filter('myLimitTo', function () {
    return function (input, limit, begin) {
        if (!input) {
            return null;
        } else {
            return input.slice(begin, begin + limit);
        }
    };
});
module.controller('TransferWithdraw', function ($scope, $http, $location) {
    // Pagination control related
    $scope.recordCount = 0;
    $scope.currentPage = 1;
    $scope.startindex = 0;
    $scope.pageBoundary = 1;
    $scope.pageChanged = function (pageno) {
        $scope.startindex = (pageno * $scope.maxSize) - $scope.maxSize;
    };
    $scope.maxSize = 5;
    $scope.bigTotalItems = 10000;
    $scope.bigCurrentPage = 1;

    $scope.$watch('maxSize', function () {
        $scope.currentPage = 1;
        var pagecount = ($scope.recordCount / $scope.maxSize);
        var decimalpart = pagecount % 1;
        $scope.pageBoundary = Math.floor(pagecount);  //integer part from pagecount
        if (decimalpart > 0) {
            $scope.pageBoundary = $scope.pageBoundary + 1;
        }
        if ($scope.pageBoundary == 0) { $scope.pageBoundary = $scope.pageBoundary + 1; }
        $scope.startindex = ($scope.currentPage * $scope.maxSize) - $scope.maxSize;
    })

    $scope.changeBoundary = function () {
        $scope.currentPage = 1;
        var pagecount = ($scope.recordCount / $scope.maxSize);
        var decimalpart = pagecount % 1;
        $scope.pageBoundary = Math.floor(pagecount);  //integer part from pagecount
        if (decimalpart > 0) {
            $scope.pageBoundary = $scope.pageBoundary + 1;
        }
        if ($scope.pageBoundary == 0) { $scope.pageBoundary = $scope.pageBoundary + 1; }
        $scope.startindex = ($scope.currentPage * $scope.maxSize) - $scope.maxSize;
    }
    //
    $scope.calculateTotals = function (arr) {
        debugger;
        $scope.total_Amount = 0;
        $scope.total_Payable = 0;
        $scope.total_AdministrativeChg = 0;

        angular.forEach(arr, function (value, index) {
            $scope.total_Amount = $scope.total_Amount + value.Amount;
            $scope.total_Payable = $scope.total_Payable + value.Payable;
            $scope.total_AdministrativeChg = $scope.total_AdministrativeChg + value.AdministrativeChg;
        })
    }
    $scope.filterChanged = function (arr) {
        debugger;
        if (arr) {
            $scope.Arr = arr;
            if ($scope.searchText == '') { $scope.Arr = $scope.transfers; }
            $scope.recordCount = $scope.Arr.length;
            $scope.changeBoundary();
            $scope.calculateTotals($scope.Arr);
        }
    }
    //
    $scope.amount = 0;
    $scope.cashwalletBalance = 0;
    $scope.fixedwalletBalance = 0;
    $scope.wallet = "1";
    $scope.walletTo = "6";
    $scope.errormessage = "";
    $scope.totalrequested = 0;
    $scope.totalpayable = 0;
    $scope.adminchange = 0;

    $scope.getbalance = function () {
        $http.get("/Home/MyWalletBalance?WalletId=1").then(function (data) {
            $scope.wallet1Balance = data.data.Balance;
        })
        $http.get("/Home/MyWalletBalance?WalletId=2").then(function (data) {
            $scope.wallet2Balance = data.data.Balance;
        })
        $http.get("/Home/MyWalletBalance?WalletId=3").then(function (data) {
            $scope.wallet3Balance = data.data.Balance;
        })
        $http.get("/Home/MyWalletBalance?WalletId=4").then(function (data) {
            $scope.wallet4Balance = data.data.Balance;
        })
    }
    $scope.getbalance();

    $scope.amountisValid = function () {
        debugger;
        if ($scope.wallet == "1") {
            if ($scope.amount > $scope.wallet1Balance) {
                return true;
            } else {
                return false;
            }
        }
        if ($scope.wallet == "2") {
            if ($scope.amount > $scope.wallet2Balance) {
                return true;
            } else {
                return false;
            }
        }
        if ($scope.wallet == "3") {
            if ($scope.amount > $scope.wallet3Balance) {
                return true;
            } else {
                return false;
            }
        }
        if ($scope.wallet == "4") {
            if ($scope.amount > $scope.wallet4Balance) {
                return true;
            } else {
                return false;
            }
        }
    }

    $scope.Transaction = function () {
        var ans = confirm("Are you sure?");
        if (ans) {
            $http.post("/Home/TransferLedgerPosting?ToWalletType=" + $scope.walletTo + "&WalletType=" + $scope.wallet + "&Amount=" + $scope.amount).then(function (response) {
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
    $scope.GetHistory = function () {
        $http.get("/Home/MemberSelfTransferHistory").then(function (response) {
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
    $scope.GetHistory();
})