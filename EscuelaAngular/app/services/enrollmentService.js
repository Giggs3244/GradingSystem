'use strict';
app.factory('enrollmentService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://localhost:57778/';
    var enrollmentServiceFactory = {};

    var _getStudents = function () {

        var deferred = $q.defer();

        return $http.get(serviceBase + 'api/student/get').success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _getCourses = function () {

        var deferred = $q.defer();

        return $http.get(serviceBase + 'api/course/get').success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _getEnrolledStudentsInCourse = function (courseId) {

        var deferred = $q.defer();

        return $http.get(serviceBase + 'api/enrollment/get/' + courseId).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _postEnrollment = function (enrollment) {

        var deferred = $q.defer();

        var descripcion = enrollment.description || '';
        //var data = "description=" + descripcion;

        return $http.post(serviceBase + 'api/enrollment/post/' + enrollment.student.id + "/" + enrollment.course.id, descripcion, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    enrollmentServiceFactory.getStudents = _getStudents;
    enrollmentServiceFactory.getCourses = _getCourses;
    enrollmentServiceFactory.postEnrollment = _postEnrollment;
    enrollmentServiceFactory.getEnrolledStudentsInCourse = _getEnrolledStudentsInCourse;

    return enrollmentServiceFactory;

}]);