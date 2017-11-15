'use strict';
app.controller('enrollmentController', ['$scope', 'enrollmentService', function ($scope, enrollmentService) {

    $scope.messageCourse = "";
    $scope.messageStudent = "";
    $scope.message = "";
    $scope.messageGood = "";
    $scope.courses = [];
    $scope.students = [];
    $scope.courseLoad = true;
    $scope.studentLoad = true;
    $scope.load = true;
    $scope.enrollment = {};

    $scope.reload = function () {
        location.reload(true);
    };

    getStudents();
    function getStudents() {
        enrollmentService.getStudents().then(function (response) {
            $scope.students = response.data;
            $scope.studentLoad = false;
            $scope.load = false;
        },
         function (err) {
             $scope.messageStudent = err.data;
             $scope.studentLoad = false;
             $scope.load = false;
         });
    };

    getCourses();
    function getCourses() {
        enrollmentService.getCourses().then(function (response) {
            $scope.courses = response.data;
            $scope.courseLoad = false;
            $scope.load = false;
        },
         function (err) {
             $scope.messageCourse = err.data;
             $scope.courseLoad = false;
             $scope.load = false;
         });
    };

    $scope.getEnrolledStudentsInCourse = function (form) {
        if (form.$valid) {
            $scope.messageGood = '';
            $scope.showEnrolledStudents = false;      
            enrollmentService.getEnrolledStudentsInCourse($scope.enrollment.course.id).then(function (response) {
                if (angular.equals([], response.data)) {
                    $scope.messageGood = 'The ' + $scope.enrollment.course.name + ' course do no contain enrolled students';
                } else {
                    $scope.detailCourse = $scope.enrollment.course;
                    $scope.enrolledStudents = response.data;
                    $scope.showEnrolledStudents = true;
                }                
            },
            function (err) {
                $scope.messageGood = err.data;
            });
        }
        else
        {
            form.$setSubmitted(true);
            return false;
        }
    };

    $scope.cancelEnrolledStudents = function () {
        $scope.messageGood = '';
        $scope.showEnrolledStudents = false;
    };

    $scope.submit = function (form) {
        $scope.messageGood = "";
        if (form.$valid) {
            $scope.load = true;
            enrollmentService.postEnrollment($scope.enrollment).then(function (response) {
                $scope.messageGood = "Enrollment is created.";
                $scope.load = false;
                $scope.enrollment = {};
                form.$setPristine();
            },
            function (err) {
                $scope.message = err.data.message;
                $scope.load = false;
                $scope.enrollment = {};
                form.$setPristine();
            });
        }
        else
        {
            form.$setSubmitted(true);
            return false;
        }
    };

}]);