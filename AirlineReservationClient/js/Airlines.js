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
    $scope.divSchedule = false;
    $scope.divReservation = false;
    $scope.divSeats = false;
    $scope.inp_airCode = '';
    $scope.inp_airPE = '';
    $scope.inp_airPF = '';
    $scope.inp_airSE = '';
    $scope.inp_airSF = '';
    //schedule
    $scope.AirplaneID = '';
    $scope.inp_airDep = '';
    $scope.inp_airArr = '';
    $scope.inp_airDepDate = '';
    $scope.inp_airArrDate = '';

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

    //function to show schedule input and data
    $scope.showSchedule = function () {
        $scope.hideAll();

        $http.get(url + "api/Schedule/5")
        .success(function (response) {
            $scope.airCodes = response;
        }).error(
                    function (data, status, headers, config) {
                        toastr.error = "Error retrieving Airline data!";
                    }
                );

        $http.get(url + "api/Schedule")
        .success(function (response) {
            $scope.dataSchedule = response;
            //console.log(response);
            $scope.divSchedule = true;
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
        $scope.divSchedule = false;
    };

    //function to add schedule
    $scope.AddSchedule = function () {
        if (!$scope.AirplaneID.length
            || !$scope.inp_airDep.length
            || !$scope.inp_airArr.length
            || !$scope.inp_airDepDate.length
            || !$scope.inp_airArrDate.length
            ) {
            toastr.error = "Some fields are blank or have invalid values!";
        }
        else {
            postString = '{"AirplaneID":' + $scope.AirplaneID + ',';
            postString += '"Airport_Departure":"' + $scope.inp_airDep + '",';
            postString += '"Airport_Arrival":"' + $scope.inp_airArr + '",';
            postString += '"Time_Departure":"' + $scope.inp_airDepDate + '",';
            postString += '"Time_Arrival":"' + $scope.inp_airArrDate + '"}';
            //$scope.error = postString;
            $http({
                method: 'POST',
                url: url + 'api/Schedule',
                data: "=" + postString,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            }).success(
                    function (data, status, headers, config) {
                        $scope.AirplaneID = '';
                        $scope.inp_airDep = '';
                        $scope.inp_airArr = '';
                        $scope.inp_airDepDate = '';
                        $scope.inp_airArrDate = '';
                        $scope.error = '';
                        $scope.hideAll();
                        $scope.showSchedule();
                        toastr.success('Schedule added successfully!!')
                    }
                ).error(
                    function (data, status, headers, config) {
                        toastr.error = "Error saving schedule!";
                    }
                );
        }
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