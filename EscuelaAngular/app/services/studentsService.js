'use strict';
app.factory('studentsService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://localhost:57778/';
    var studentsServiceFactory = {};

    var _getStudents = function () {

        var deferred = $q.defer();

        return $http.get(serviceBase + 'api/student/get').success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _postStudent = function (student) {

        var deferred = $q.defer();

        var data = "email=" + student.email + "&firstName=" + student.firstName + "&lastName=" + student.lastName;

        return $http.post(serviceBase + 'api/student/post', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _putStudent = function (student) {

        var deferred = $q.defer();

        var data = "email=" + student.email + "&firstName=" + student.firstName + "&lastName=" + student.lastName;

        return $http.put(serviceBase + 'api/student/put/' + student.id, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _deleteStudent = function (studentId) {

        var deferred = $q.defer();

        return $http.delete(serviceBase + 'api/student/delete/' + studentId).success(function (response) {
            deferred.resolve(response);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    studentsServiceFactory.getStudents = _getStudents;
    studentsServiceFactory.postStudent = _postStudent;
    studentsServiceFactory.putStudent = _putStudent;
    studentsServiceFactory.deleteStudent = _deleteStudent;

    return studentsServiceFactory;

}]);