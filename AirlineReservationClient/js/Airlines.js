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
    $scope.divAirlines = false;

    $scope.inp_airCode = '';
    $scope.inp_airPE = '';
    $scope.inp_airPF = '';
    $scope.inp_airSE = '';
    $scope.inp_airSF = '';

    //show home
    $scope.showHome = function () {
        $scope.hideAll();
        $scope.divHome = true;
    };

    //methods for showing/hiding divs
    $scope.showAirlines = function () {
        $scope.hideAll();
        $http.get(url + "api/Airplane")
        .success(function (response) {
            $scope.dataAirlines = response;
            //console.log(response);
            $scope.divAirlines = true;
        }).error(
                    function (data, status, headers, config) {
                        toastr.error = "Error retrieving data!";
                    }
                );
    };

    //hide all divs
    $scope.hideAll = function () {
        $scope.divHome = false;
        $scope.divAirlines = false;
    };

        //function to add airline
    $scope.AddAirline = function () {
        //$.ajax({
        //    type: "POST",
        //    url: url + "api/Airplane",
        //    data: { "": "check" },
        //    success: function (exists) {
        //        if (exists) {
        //            fileExists = true;
        //        } else {
        //            fileExists = false;
        //        }
        //    }
        //});

        if (!$scope.inp_airCode.length
            || !angular.isNumber($scope.inp_airPE)
            || !angular.isNumber($scope.inp_airPF)
            || !angular.isNumber($scope.inp_airSE)
            || !angular.isNumber($scope.inp_airSF)
            ) {
            toastr.error = "Some fields are blank or have invalid values!";
        }
        else {
            postString = '{"AirplaneCode":"' + $scope.inp_airCode + '",';
            postString += '"Price_Economy":' + $scope.inp_airPE + ',';
            postString += '"Price_FirstClass":' + $scope.inp_airPF + ',';
            postString += '"Seats_Economy":' + $scope.inp_airSE + ',';
            postString += '"Seats_FirstClass":' + $scope.inp_airSF + '}';

            $http({
                method: 'POST',
                url: url + 'api/Airplane',
                data: "=" + postString,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(
                    function (data, status, headers, config) {
                        $scope.inp_airCode = '';
                        $scope.inp_airPE = '';
                        $scope.inp_airPF = '';
                        $scope.inp_airSE = '';
                        $scope.inp_airSF = '';
                        $scope.error = '';
                        $scope.hideAll();
                        $scope.showAirlines();
                        toastr.success('New Airline added successfully!!')
                    }
                ).error(
                    function (data, status, headers, config) {
                        toastr.error = "Error saving new Airline!";
                    }
                );
        }
    };
});