var module = angular.module('app', []);

module.controller('TrnPasswordChange', function ($scope, $http) {
    $scope.isExists = true;
    $scope.oldpassword = "";
    $scope.newpassword = "";
    $scope.confirmpassword = "";
    $scope.oldpwdErrMsg = "";
    $scope.newpwdErrMsg = "";
    $scope.confirmpwdErrMsg = "";

    $http.get("TransactionPasswordExists").then(function (response) {
        $scope.isExists = response.data.isEXISTS;
    });

    $scope.checkOldPwd = function () {
        $http.get("TransactionPasswordMatch?OldPassword=" + $scope.oldpassword).then(function (response) {
            $scope.isOK = response.data.isOK;
            if (!$scope.isOK) { $scope.oldpwdErrMsg = "Old Transaction password is incorrect."; }
            else { $scope.oldpwdErrMsg = "";}
        });
    }

    $scope.checkNewPwd = function () {
        if ($scope.oldpassword == $scope.newpassword) { $scope.newpwdErrMsg = "Old and New Transaction password cannot be same."; }
        else { $scope.newpwdErrMsg = ""; }
    }
    
    $scope.checkConfirmPwd = function () {
        if ($scope.newpassword != $scope.confirmpassword) { $scope.confirmpwdErrMsg = "New password and it's confirmation password must be same."; }
        else { $scope.confirmpwdErrMsg = ""; }
    }

    $scope.submit = function () {
        if ($scope.oldpwdErrMsg == "" && $scope.newpwdErrMsg == "" && $scope.confirmpwdErrMsg=="") {
            $http.get("MemberDetail").then(function (response) {
                $scope.member = response.data.Member;
                $scope.member.Transactionpassword = $scope.newpassword;
                $http.put("/api/Members/" + $scope.member.Id, $scope.member).then(function () {
                    alert("Password saved successfully.");
                })
            })
        }
    }
})