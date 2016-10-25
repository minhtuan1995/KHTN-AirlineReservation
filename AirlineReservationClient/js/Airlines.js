var url = "http://localhost:26930/";

$(function () {
    $('#datetimepicker2').datetimepicker({
        language: 'en',
        pick12HourFormat: true
    });
});

var app = angular.module('angularApp', []);
app.controller('airlineController', function ($scope, $http) {
    //Default Values
    $scope.divHome = true;

    $scope.currentResID = 0;

    //show home
    $scope.showHome = function () {
        $scope.divHome = true;
    };

});