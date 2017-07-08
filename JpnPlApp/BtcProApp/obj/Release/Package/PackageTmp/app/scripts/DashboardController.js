var module = angular.module('app', []);
module.controller('Dashboard', function ($scope, $http, $location) {
    $scope.data = [];
    $scope.username = "";
    $http.get("/Home/UserWalletBalance?username=" + $scope.username).then(function (data) {
        debugger;
        $scope.DevelopmentBalance = data.data.Wallet1_Balance;
        $scope.ReferralBalance = data.data.Wallet2_Balance;
        $scope.RoyaltyBalance = data.data.Wallet3_Balance;
        $scope.DiamondRoyaltyBalance = data.data.Wallet4_Balance;
        $scope.JoiningBalance = data.data.Wallet5_Balance;
        $scope.WithdrawBalance = data.data.Wallet6_Balance;
    })
    $http.get("/Home/MemberDetail").then(function (data) {
        $scope.package = "Share";
        $scope.MobileNo = data.data.Member.Mobileno;
        $scope.Emailid = data.data.Member.Emailid;
        $scope.SignupDate = data.data.Date;
        $scope.Name = data.data.Member.Firstname;
        if (data.data.Member.Achievement1 == 1) {
            $scope.RoyaltyClub = "Royalty Club Achiever";
        } else {
            $scope.RoyaltyClub = "";
        }
        if (data.data.Member.Achievement2 == 1) {
            $scope.DiamondRoyaltyClub = "Diamond Royalty Club Achiever";
        } else {
            $scope.DiamondRoyaltyClub = "";
        }
    })

    //$scope.CashWallet = function () {
    //    var path = $location.path("/Home/CashWallet");
    //    var abspath = path.$$absUrl;
    //    var modifiedpath = abspath.replace("/Home/Index#", "");
    //    window.location = modifiedpath;
    //}
    //$scope.ReserveWallet = function () {
    //    var path = $location.path("/Home/ReserveWallet");
    //    var abspath = path.$$absUrl;
    //    var modifiedpath = abspath.replace("/Home/Index#", "");
    //    window.location = modifiedpath;
    //}
    //$scope.ReturnWallet = function () {
    //    var path = $location.path("/Home/ReturnWallet");
    //    var abspath = path.$$absUrl;
    //    var modifiedpath = abspath.replace("/Home/Index#", "");
    //    window.location = modifiedpath;
    //}
    $scope.TeamMembers = function () {
        var path = $location.path("/Home/TeamMembers");
        var abspath = path.$$absUrl;
        var modifiedpath = abspath.replace("/Home/Index#", "");
        window.location = modifiedpath;
    }
    $scope.MyPurchase = function () {
        var path = $location.path("/Home/MyPurchase");
        var abspath = path.$$absUrl;
        var modifiedpath = abspath.replace("/Home/Index#", "");
        window.location = modifiedpath;
    }
    $scope.MyRepurchase = function () {
        var path = $location.path("/Home/MyPurchase");
        var abspath = path.$$absUrl;
        var modifiedpath = abspath.replace("/Home/Index#", "");
        window.location = modifiedpath;
    }
})