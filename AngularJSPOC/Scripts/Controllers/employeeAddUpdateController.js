app.controller("employeeAddUpdateCtrl", function ($scope, employeeFactory) {
    $scope.employee = {}
    
$scope.getEmployee = function (id) {
    $scope.loading = true;  // start spinng the image   
    if (id > 0) {
        $scope.insertMode = false;
    }
    else {
        $scope.insertMode = true;
    }
        employeeFactory.getEmployee(id)
        .success(function (response) {            
            $scope.loading = false;
            $scope.employee.id = response.EmployeeId;
            $scope.employee.Name = response.EmployeeName;
            $scope.employee.Age = response.EmployeeAge;
            $scope.employee.Email = response.EmployeeEmail;
            //$scope.employee.CountryId = response.CountryId;
            $scope.employee.StateId = response.StateId;
            //$scope.employee.CountryName = response.CountryName;
            $scope.employee.StateName = response.StateName;
        })
        .error(function (error) {
            $scope.status=error;
        });
}

$scope.saveEmployee = function (isValid) {
    $scope.submitted = true;
    if (isValid) {
        $scope.loading = true;  // start spinng the image    
        employeeFactory.saveEmployee($scope.employee)
        .success(function (response) {
            $scope.loading = false;
            $scope.submitted = false;
            if ($scope.employee.id > 0) {
                $scope.editModeSuccess = true;
                $scope.insertModeSuccess = false;
            }
            else {
                $scope.insertModeSuccess = true;
                $scope.editModeSuccess = false;
            }
            $scope.employee.Name = '';
            $scope.employee.Age = '';
            $scope.employee.Email = '';
            $scope.employee.StateId = 0;
            $scope.employee.CountryId = 0;
        })
        .error(function (error) {
            $scope.status = 'Unable to save employee data';
            $scope.loading = false;
            $scope.submitted = false;
        });
    }
}

$scope.countries = function getCountries() {
    employeeFactory.getCountries()
    .success(function (response) {
        $scope.Countries = response;
    })
    .error(function (error) {
        $scope.status = 'Unable to get countries : ' + error.message;
    });
}

$scope.states = function getStates() {    
    employeeFactory.getStates()
    .success(function (response) {
        $scope.States = response;
    })
    .error(function (error) {
        $scope.status = 'Unable to get states: ' + error;
    });
}

});