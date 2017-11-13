'use strict';
app.controller('studentsController', ['$scope', 'studentsService', function ($scope, studentsService) {

    $scope.message = "";
    $scope.load = true;
    $scope.isRegister = false;
    $scope.isEdition = false;
    $scope.showDetail = false;
    $scope.studentDetail = {};
    $scope.students = [];
    $scope.student = {};
    $scope.studentIndexEdition = null;

    getStudents();
    function getStudents()
    {
        studentsService.getStudents().then(function (response) {
            $scope.students = response.data;
            $scope.load = false;
        },
         function (err) {
             $scope.message = err.data;
             $scope.load = false;
        });
    };

    $scope.register = function () {
        $scope.isRegister = true;
        $scope.student = {};
        $scope.message = "";
    };
    
    $scope.submit = function (form) {
        if (form.$valid)
        {
            $scope.load = true;
            $scope.isRegister = false;
            studentsService.postStudent($scope.student).then(function (response) {
                $scope.students.push(response.data);
                $scope.load = false;
                $scope.cancel(form);
            },
            function (err) {
                 $scope.message = err.data;
                 $scope.load = false;
                 $scope.cancel(form);
            });
        }
        else
        {
            form.$setSubmitted(true);
            return false;
        }
    };

    $scope.reload = function () {
        location.reload(true);
    };

    $scope.update = function (studentId, index) {
        $scope.isEdition = true;
        $scope.student.email = $scope.students[index].email;
        $scope.student.firstName = $scope.students[index].firstName;
        $scope.student.lastName = $scope.students[index].lastName;
        $scope.student.id = studentId;
        $scope.studentIndexEdition = index;
    };

    $scope.details = function (index) {
        $scope.showDetail = true;
        $scope.studentDetail.email = $scope.students[index].email;
        $scope.studentDetail.firstName = $scope.students[index].firstName;
        $scope.studentDetail.lastName = $scope.students[index].lastName;
    };

    $scope.delete = function (studentId, index) {
        $scope.load = true;
        studentsService.deleteStudent(studentId).then(function (response) {
            $scope.students.splice(index, 1);
            $scope.load = false;
        },
        function (err) {
            $scope.message = err.data;
            $scope.load = false;
        });
    };

    $scope.cancel = function (form) {
        $scope.isRegister = false;
        $scope.isEdition = false;
        $scope.student = {};
        form.$setPristine();
    };

    $scope.goStudents = function () {
        $scope.showDetail = false;
        $scope.studentDetail = {};
    };

    $scope.submitUpdate = function (form) {
        if (form.$valid) {
            $scope.load = true;
            $scope.isEdition = false;
            studentsService.putStudent($scope.student).then(function (response) {
                $scope.students[$scope.studentIndexEdition] = response.data;
                $scope.load = false;
                $scope.studentIndexEdition = null;
                $scope.cancel(form);
            },
            function (err) {
                $scope.message = err.data;
                $scope.load = false;
                $scope.cancel(form);
            });
        }
        else
        {
            form.$setSubmitted(true);
            return false;
        }
    };
}]);