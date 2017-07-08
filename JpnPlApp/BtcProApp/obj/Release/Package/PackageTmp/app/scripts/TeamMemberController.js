var module = angular.module('app', []);

module.controller('Team', function ($scope, $http) {
    $scope.team = [];
    $scope.loading = true;
    $http.get("/Home/GetTeam?RegistrationId=null").then(function (teamdata) {
        $scope.team = teamdata.data.Members;
        $scope.loading = false;
    })
})