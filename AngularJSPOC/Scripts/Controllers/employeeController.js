
app.controller("employeeCtrl", function ($scope, employeeFactory) {
    
    $scope.employees = function getEmployees() {        
        employeeFactory.getEmployees()
        .success(function (response) {
            $scope.employees = response;
        })
        .error(function (error) {
            $scope.status = 'Unable to load employee data: ' + error.message;
        });
    }

    

    function getEmployees() {     
        employeeFactory.getEmployees()
        .success(function (response) {
            $scope.employees = response;
        })
        .error(function (error) {
            $scope.status = 'Unable to load employee data: ' + error.message;
        });
    }

    $scope.deleteEmployee = function deleteEmployee(id) {
        if (confirm('Are you sure you want to delete this?')) {
            employeeFactory.deleteEmployee(id)
            .success(function (response) {
                getEmployees();
            })
            .error(function (error) {
                alert(error);
            });
        }
    }

    $scope.getEmployee = function getEmployee(id) {        
        employeeFactory.getEmployee(id);
    }
});


