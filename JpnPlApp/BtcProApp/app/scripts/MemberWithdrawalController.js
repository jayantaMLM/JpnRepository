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
module.controller('MemberWithdraw', function ($scope, $http, $location) {
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
    $scope.wallet = "6";
    $scope.errormessage = "";
    $scope.totalrequested = 0;
    $scope.totalpayable = 0;
    $scope.adminchange = 0;
    $scope.account = " ";
    $scope.isExists1 = true;
    $scope.isExists2 = true;
    $scope.BikasAcNo = "";
    $scope.BankAcNo = "";
    $scope.selectedAccount = "";

    $http.get("/Home/IsUserMember").then(function (response) {
        $scope.isExists = response.data.Found;
    })
    $http.get("/Home/IsMemberACNOpresent").then(function (response) {
        debugger;
        $scope.isExists1 = response.data.Found1;
        $scope.isExists2 = response.data.Found2;
        $scope.BikasAcNo = response.data.BikasAcNo;
        $scope.BankAcNo = response.data.BankAcNo;
    })

    $scope.getbalance = function () {
        $http.get("/Home/MyWalletBalance?WalletId=6").then(function (data) {
            $scope.wallet1Balance = data.data.Balance;
        })
    }

    $scope.amountisValid = function () {
        debugger;
        if ($scope.wallet == "1") {
            if ($scope.amount > $scope.wallet1Balance) {
                return true;
            } else {
                return false;
            }
        }
    }

    $scope.Transaction = function () {
        if ($scope.account == "BIKAS") {
            $scope.selectedAccount = $scope.BikasAcNo;
        }
        if ($scope.account == "BANK") {
            $scope.selectedAccount = $scope.BankAcNo;
        }
        debugger;
        var ans = confirm("Are you sure?");
        if (ans) {
            $http.post("/Home/WithdrawPostingMember?WalletType=" + $scope.wallet + "&Amount=" + $scope.amount + "&PayAccount=" + $scope.selectedAccount ).then(function (response) {
                debugger;
                if (response.data.Success) {
                    $scope.getbalance();
                    $scope.GetHistory();
                    $http.post("/Home/SendMyWithdrawalRequestemail?Username=&amount=" + $scope.amount + "&status=").then(function () {
                        $scope.amount = 0;
                    })
                }
            })
        }
    }

    $scope.getbalance();

    $scope.GetHistory = function () {
        $scope.totalrequested = 0;
        $scope.totalpayable = 0;
        $scope.adminchange = 0;
        $http.get("/Home/MemberWithdrawalHistory").then(function (response) {
            $scope.transfers = response.data.Transfers;
            $scope.recordCount = $scope.transfers.length;
            $scope.changeBoundary();
            $scope.calculateTotals($scope.transfers);
        })
    }

    $scope.GetHistory();

    $scope.cancelOrder = function (Id) {
        var ans = confirm("Sure you want to cancel it?");
        if (ans) {
            $http.get("/Home/CancelOrder?Id=" + Id + "&remarks=Cancelled by Member" + "&comment=").then(function (response) {
                if (response.data.Success) {
                    $scope.getbalance();
                    $scope.GetHistory();
                    alert("Request successfully cancelled.");
                } else {
                    alert("Request cancellation failure!!!")
                }
            })
        }
    }
})