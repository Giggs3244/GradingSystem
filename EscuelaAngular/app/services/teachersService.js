'use strict';
app.factory('teachersService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:57778/';
    var teachersServiceFactory = {};

    var _getTeachers = function () {

        return $http.get(serviceBase + 'api/teachers').then(function (results) {
            return results;
        });
    };

    teachersServiceFactory.getTeachers = _getTeachers;

    return teachersServiceFactory;

}]);