
var app = angular.module("employeeApp", ['ngMessages','ngRoute']);
//var app = angular.module("employeeApp", ['ngRoute']);

app.config(['$routeProvider', '$locationProvider',
  function ($routeProvider, $locationProvider) {
      $routeProvider 
        .when('/Employee/List',
        {
            templateUrl:  'Employee/Index',
            controller: 'employeeCtrl'
        })
        .when('/Employee/Create',
        {
            templateUrl:  'Employee/Create',
            controller: 'employeeAddUpdateCtrl'
        })
       .otherwise({
           redirectTo: 'Employee/Index'
       });

     $locationProvider.html5Mode(true);
   //$locationProvider.html5Mode(false).hashPrefix('!');
  }]);

app.factory('employeeFactory',function ($http) {
    var employeeFactory = {};
    employeeFactory.getEmployees = function () {
        return $http.get(getEmployeeListing);
    };

    // Simple POST request example (passing data) :
    employeeFactory.saveEmployee = function (employee) {
        return $http.post(saveEmployeeListing, employee);
    };

    employeeFactory.getStates = function () {
        return $http.get(getStates);
    };

    employeeFactory.getCountries = function () {
        return $http.get(getCountries);
    };

    employeeFactory.getEmployee = function (id) {        
        return $http.get(getEmployeeUrl + id);        
    };

    employeeFactory.deleteEmployee = function (id) {
        return $http.post(deleteEmployeeUrl + id);
    }
    return employeeFactory;
});
