﻿var module = angular.module('app', ['ngMaterial', 'ngMessages']);

module.controller('EditProfile', function ($scope, $http) {

    $http.get("MemberDetail").then(function (response) {
        $scope.member = response.data.Member;
        $scope.member.Preferredlanguage = "English(US)";
        $scope.member.Doj = new Date(parseInt($scope.member.Doj.replace('/Date(', '')));
        $scope.kyc_status = "";
        $scope.kyc_documents = "";
        if ($scope.Kycstatus) {
            $scope.kyc_status = "APPROVED";
        } else {
            $scope.kyc_status = "APPROVAL PENDING";
        }
        if ($scope.kyc_documents == "") {
            $scope.kyc_documents = "DOCUMENTS SUBMISSION PENDING";
        }
        else {
            $scope.kyc_documents = $scope.member.Kycdocuments;
        }
    })

    $scope.buttonclick0 = function () {
        debugger;
        $http.post("/Home/UpdateMember/" + $scope.member.Id, $scope.member).then(function (response) {
            if (response.data.Success) {
                alert('Saved successfully');
            } else {
                alert('Save not successful !!!');
            }
           
        })
    }

    $scope.buttonclick = function (ev) {
        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        $mdDialog.show(
          $mdDialog.alert()
            .parent(angular.element(document.querySelector('#popupContainer')))
            .clickOutsideToClose(true)
            .title('This is an alert title')
            .textContent('You can specify some description text in here.')
            .ariaLabel('Alert Dialog Demo')
            .ok('Got it!')
            .targetEvent(ev)
        );
    };

})